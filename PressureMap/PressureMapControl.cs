namespace PressureMap
{
    using DevExpress.Utils;
    using DevExpress.XtraCharts;
    using DevExpress.XtraCharts.Heatmap;
    using DevExpress.XtraEditors.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using Excel = Microsoft.Office.Interop.Excel;

    public partial class PressureMapControl : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable _wellsDataTable;
        private DataTable _injectionDataTable;
        private Series _wellSeries;
        private Series _injectionSeries;
        private List<Well> _wellList;
        private const int _xCount = 1000;
        private const int _yCount = 1000;

        private List<double[,]> _pressures;

        #region график расхода Test
        
        private Series _testSeries;

        private void ChartInitTest()
        {
            _testSeries = new Series("Данные", ViewType.Line);
            _testSeries.View.Color = Color.Black;
            chartControl1.Series.Add(_testSeries);
        }

        private void ChartUpdate(double n)
        {
            int num = 100;
            var data = new (double x, double y)[num];

            for (int i = 0; i < num; i++)
            {
                data[i].x = i;
                data[i].y = Math.Pow(i, n);
            }

            _testSeries.Points.Clear();
            foreach (var value in data)
            {
                _testSeries.Points.Add(new SeriesPoint(value.x, value.y));
            }
        }

        #endregion

        public PressureMapControl()
        {
            InitializeComponent();
            InitializeWellTable();
            InitializeWellChartControl();
            InitializeInjectionDataTable();
            InitializeInjectionChartControl();
            ChartInitTest();
            ChartUpdate(0);
            UpdateTrackBar();

            _wellList = new List<Well>();
            _pressures = new List<double[,]>();

            AddNewInjectionData(new List<(double t, double Q)>()
            {
                (0, 0),
                (1, 2),
                (4, 3),
            });
        }

        private void UpdateTrackBar()
        {
            int days = GetDaysCount();
            int step = (int)stepNumericUpDown.Value;

            timesTrackBarControl.Properties.Minimum = 0;
            timesTrackBarControl.Properties.Maximum = days;

            var date = startDateEdit.DateTime;
            timesTrackBarControl.Properties.Labels.Clear();
            var labels = new TrackBarLabel[days / step + 1];
            for (int i = 0; i <= days; i += step)
            {
                labels[i / step] = new TrackBarLabel(date.ToString("dd.MM.yyyy"), i);
                date = date.AddDays(step);
            }
            timesTrackBarControl.Properties.Labels.AddRange(labels);
        }

        private int GetDaysCount()
        {
            var start = startDateEdit.DateTime;
            var end = endDateEdit.DateTime;
            return (end - start).Days;
        }

        private void InitializeInjectionDataTable()
        {
            _injectionDataTable = new DataTable();
            _injectionDataTable.Columns.Add("t");
            _injectionDataTable.Columns.Add("Q");
            injectionDataGridControl.DataSource = _injectionDataTable;
        }

        private void InitializeWellTable()
        {
            _wellsDataTable = new DataTable();
            _wellsDataTable.Columns.Add("Номер");
            _wellsDataTable.Columns.Add("X");
            _wellsDataTable.Columns.Add("Y");
            wellGridControl.DataSource = _wellsDataTable;
        }

        private void InitializeInjectionChartControl()
        {
            _injectionSeries = new Series("Данные закачки", ViewType.Line);
            _injectionSeries.View.Color = Color.Blue;

            injectionDataChartControl.BeginInit();
            injectionDataChartControl.Series.Add(_injectionSeries);
            var diagram = new XYDiagram();
            diagram.AxisX.Title.Text = "Время";
            diagram.AxisX.Title.Visibility = DefaultBoolean.True;
            diagram.AxisY.Title.Text = "Расход";
            diagram.AxisY.Title.Visibility = DefaultBoolean.True;
            injectionDataChartControl.Diagram = diagram;
            injectionDataChartControl.EndInit();
        }

        private void InitializeWellChartControl()
        {
            _wellSeries = new Series("Скважины", ViewType.Point);
            _wellSeries.View.Color = Color.Red;
            wellMapChartControl.Series.Add(_wellSeries);

            wellMapChartControl.BeginInit();

            XYDiagram diagram = new XYDiagram();
            
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;
            diagram.EnableAxisYScrolling = true;
            diagram.EnableAxisYZooming = true;
            
            diagram.AxisX.GridLines.Visible = true;
            diagram.AxisX.GridLines.MinorVisible = false;
            diagram.AxisX.GridLines.Color = Color.LightGray;
            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.MinorVisible = false;
            diagram.AxisY.GridLines.Color = Color.LightGray;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            
            wellMapChartControl.Diagram = diagram;

            wellMapChartControl.EndInit();
        }

        private void DeleteWell()
        {
            var focusedRowHandle = wellGridView.FocusedRowHandle;
            if (focusedRowHandle >= 0)
            {
                var dataTable = (DataTable)wellGridControl.DataSource;
                dataTable.Rows.RemoveAt(focusedRowHandle);
                _wellSeries.Points.RemoveAt(focusedRowHandle);
                wellMapChartControl.RefreshData();
            }
        }

        private bool IsDoubleValueValid(string str, out double value)
        {
            str = str.Replace(',', '.');
            if (!double.TryParse(str, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out double parsed))
            {
                value = double.NaN;
                return false;
            }
            value = parsed;
            return true;
        }

        private void AddNewWells(IEnumerable<Well> wells)
        {
            foreach (var well in wells)
            {
                _wellsDataTable.Rows.Add(new object[] { well.Number, well.X, well.Y });
                var point = new SeriesPoint(well.X, well.Y);
                _wellSeries.Points.Add(point);
            }
            wellMapChartControl.RefreshData();
            _wellList.AddRange(wells);
        }

        private void AddNewInjectionData(IEnumerable<(double t, double Q)> injectionData)
        {
            foreach (var data in injectionData)
            {
                _injectionDataTable.Rows.Add(new object[] { data.t, data.Q });
                var point = new SeriesPoint(data.t, data.Q);
               _injectionSeries.Points.Add(point);
            }
            injectionDataChartControl.RefreshData();
        }

        private void addWellButton_Click(object sender, System.EventArgs e)
        {
            string xStr = xTextEdit.Text;
            string yStr = yTextEdit.Text;
            string numStr = wellNumberTextEdit.Text;
            

            bool isCorrect = IsDoubleValueValid(xStr, out double x);
            if (!IsDoubleValueValid(yStr, out double y))
            {
                isCorrect = false;
            }

            if (!int.TryParse(numStr, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out int number))
            {
                isCorrect = false;
            }

            if (!isCorrect)
            {
                MessageBox.Show("Некорректно указаны координаты скважины или её номер");
                return;
            }

            var well = new Well(number, x, y);

            AddNewWells(new Well[] { well });

            xTextEdit.Text = string.Empty;
            yTextEdit.Text = string.Empty;
        }

        private void deleteWellButton_Click(object sender, System.EventArgs e)
        {
            DeleteWell();
        }
        
        private void wellGridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void wellGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteWell();
            }
        }

        private void loadFromExcelButton_Click(object sender, System.EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var coordinates = new List<(double X, double Y)>();
                var errorMessages = new List<string>();

                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Open(filePath);
                var worksheet = workbook.Sheets[1];
                var usedRange = worksheet.UsedRange;
                int rowCount = usedRange.Rows.Count;
                
                for (int row = 2; row <= rowCount; row++)
                {
                    Excel.Range cellX = (Excel.Range)worksheet.Cells[row, 1];
                    Excel.Range cellY = (Excel.Range)worksheet.Cells[row, 2];

                    if (cellX.Value2 != null && cellY.Value2 != null)
                    {
                        if (IsDoubleValueValid(cellX.Value2.ToString(), out double xValue))
                        {
                            if (IsDoubleValueValid(cellY.Value2.ToString(), out double yValue))
                            {
                                // Добавляем координаты в список
                                coordinates.Add((xValue, yValue));
                            }
                        }
                        else
                        {
                            errorMessages.Add(
                                $"Невозможно преобразовать значения в строке {row} в числа.");
                        }
                    }
                    else
                    {
                        errorMessages.Add($"Пустая ячейка в строке {row}.");
                    }
                }
                
                workbook.Close(false);
                excelApp.Quit();
                
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                //AddNewWells(coordinates);

                // show errors
                if (errorMessages.Count != 0)
                {
                    string errorMessage = string.Empty;
                    foreach (var message in errorMessages)
                    {
                        errorMessage += message + "\n";
                    }
                    MessageBox.Show(errorMessage);
                }
            }
        }
        
        private void UpdateHeatMap(double[,] pressure, int xCount, int yCount)
        {
            var dataAdapter = new HeatmapMatrixAdapter();
            dataAdapter.XArguments = GenerateAxisLabels(xCount);
            dataAdapter.YArguments = GenerateAxisLabels(yCount);
            dataAdapter.Values = pressure;
            pressureHeatmapControl.DataAdapter = dataAdapter;

            var palette = new Palette("Custom") {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Cyan,
                Color.Blue,
                Color.DarkBlue,
                Color.DarkMagenta
            };

            var colorProvider = new HeatmapRangeColorProvider
            {
                Palette = palette,
                ApproximateColors = true,
            };

            for (double i = 0.0; i < 1.0; i+=0.125)
            {
                colorProvider.RangeStops.Add(
                    new HeatmapRangeStop(i, HeatmapRangeStopType.Percentage));
            }
            
            pressureHeatmapControl.ColorProvider = colorProvider;
        }

        private string[] GenerateAxisLabels(int count)
        {
            string[] labels = new string[count];
            for (int i = 0; i < count; i++)
            {
                labels[i] = $"{i + 1}";
            }
            return labels;
        }

        private void runSimpleButton_Click(object sender, EventArgs e)
        {
            double p0 = (double)p0SpinEdit.Value;
            double mu = (double)muSpinEdit.Value;
            double Q = (double)qSpinEdit.Value;
            double k = (double)kSpinEdit.Value;
            double H = (double)hSpinEdit.Value;
            double phi0 = (double)phi0SpinEdit.Value;
            double ct = (double)ctSpinEdit.Value;
            double D = k / (mu * phi0 * ct);

            var calculator = new PressureCalculator(p0, mu, Q, k, H, D);

            double[] times = GetTimes();
            
            double[] x = Linspace(-5, 100, _xCount);
            double[] y = Linspace(-5, 100, _yCount);

            double[][] coords = new[]
            {
                new double[] { 10.0, 20.0 } ,
                new double[] { 30.0, 40.0 } ,
                new double[] { 50.0, 60.0 } ,
            };

            _pressures.Clear();
            for (int i = 0; i < times.Length; i++)
            {
                double time = times[i];
                double[,] pressure = calculator.ComputatePressureConst(x, y, time, coords);
                _pressures.Add(pressure);
            }
        }

        public static double[] Linspace(double start, double stop, int num)
        {
            double step = (stop - start) / (num - 1);
            return Enumerable.Range(0, num)
                .Select(i => start + i * step)
                .ToArray();
        }

        private void GetWellsMinMax(
            out double minX, out double minY, 
            out double maxX, out double maxY)
        {
            minX = _wellList.Min(w => w.X);
            minY = _wellList.Min(w => w.Y);
            maxX = _wellList.Max(w => w.X);
            maxY = _wellList.Max(w => w.Y);
        }

        private double[] GetTimes()
        {
            int days = GetDaysCount();
            int step = (int)stepNumericUpDown.Value;
            days /= step;
            double secondsInDay = 86400.0;
            double[] times = new double[days + 1];
            for (int i = 0; i <= days; i++)
            {
                times[i] = (secondsInDay * i);
            }
            return times;
        }

        private void timesTrackBarControl_Scroll(object sender, EventArgs e)
        {

        }

        private void stepNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrackBar();
        }

        private void endDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            UpdateTrackBar();
        }

        private void startDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            UpdateTrackBar();
        }
    }
}
