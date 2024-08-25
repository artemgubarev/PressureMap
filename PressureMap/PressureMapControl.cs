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
            ChartInitTest();
            ChartUpdate(0);
            UpdateTrackBar();

            _wellList = new List<Well>();
            _pressures = new List<double[,]>();

            wellMapChartControl.ToolTipController = new ToolTipController();
            wellMapChartControl.ToolTipController.ShowBeak = true;
        }

        private void UpdateTrackBar()
        {
            int days = GetDaysCount();
            int step = (int)stepNumericUpDown.Value;

            timesTrackBarControl.Properties.Minimum = 0;
            timesTrackBarControl.Properties.Maximum = days / step;

            var date = startDateEdit.DateTime;
            timesTrackBarControl.Properties.Labels.Clear();
            var labels = new TrackBarLabel[days / step + 2];

            for (int i = 0; i <= (days / step) + 1; i ++)
            {
                labels[i] =
                    new TrackBarLabel(date.ToString("dd.MM.yyyy"), i);
                date = date.AddDays(step);
            }
           
            timesTrackBarControl.Properties.TickFrequency = 1;
            timesTrackBarControl.Properties.Labels.AddRange(labels);
            int num = timesTrackBarControl.Value;
            currentTimeTextEdit.Text = labels[num].Label;
        }

        private int GetDaysCount()
        {
            var start = startDateEdit.DateTime;
            var end = endDateEdit.DateTime;
            return (end - start).Days;
        }

        private void InitializeWellTable()
        {
            _wellsDataTable = new DataTable();
            _wellsDataTable.Columns.Add("Номер");
            _wellsDataTable.Columns.Add("X");
            _wellsDataTable.Columns.Add("Y");
            wellGridControl.DataSource = _wellsDataTable;
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
            diagram.AxisX.GridLines.Color = Color.DimGray;
            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.MinorVisible = false;
            diagram.AxisY.GridLines.Color = Color.DimGray;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;

            diagram.DefaultPane.BackColor = Color.LightBlue;

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
                point.Tag = well.Number;
                _wellSeries.Points.Add(point);
            }
            wellMapChartControl.RefreshData();
            _wellList.AddRange(wells);
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
            wellNumberTextEdit.Text = string.Empty;
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
            var xCoords = new List<(int Num, double X)>();
            var yCoords = new List<(int Num, double Y)>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                
                Excel.Worksheet worksheetX = workbook.Sheets[3];
                Excel.Worksheet worksheetY = workbook.Sheets[4];
                
                int numRow = 1; 
                int valueRow = 2;

                ParseSheetData(worksheetX, numRow, valueRow, xCoords);
                ParseSheetData(worksheetY, numRow, valueRow, yCoords);

                var combined = from x in xCoords  
                               join y in yCoords on x.Num equals y.Num
                               select (x.Num, x.X, y.Y);

                var wells = new List<Well>();
                foreach (var value in combined)
                {
                    wells.Add(new Well(value.Num, value.X, value.Y));
                }
                AddNewWells(wells);

                workbook.Close(false);
                excelApp.Quit();
                
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheetX);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void ParseSheetData(Excel.Worksheet worksheet, int numRow, int valueRow,
            List<(int Number, double Value)> coordsList)
        {
            for (int col = 1; col <= worksheet.UsedRange.Columns.Count; col++)
            {
                Excel.Range cellNum = worksheet.Cells[numRow, col];
                string cellValueNumStr = cellNum.Value?.ToString() ?? string.Empty;

                Excel.Range valueCell = worksheet.Cells[valueRow, col];
                string valueCellStr = valueCell.Value?.ToString() ?? string.Empty;

                if (!IsDoubleValueValid(valueCellStr, out double value))
                {
                    continue;
                }

                if (!int.TryParse(cellValueNumStr, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out int number))
                {
                    continue;
                }

                coordsList.Add((number, value));
            }
        }

        private void UpdateHeatMap(double[,] pressure)
        {
            var dataAdapter = new HeatmapMatrixAdapter();
            dataAdapter.XArguments = GenerateAxisLabels(_xCount);
            dataAdapter.YArguments = GenerateAxisLabels(_yCount);
            dataAdapter.Values = pressure;
            pressureHeatmapControl.DataAdapter = dataAdapter;
            
            double minValue = pressure.Cast<double>().Min();
            double maxValue = pressure.Cast<double>().Max();

            if (minValue == maxValue)
            {
                var palette = new Palette("SolidColor")
                {
                    Color.Blue
                };

                var colorProvider = new HeatmapRangeColorProvider();
                colorProvider.RangeStops.Add(new HeatmapRangeStop(0, HeatmapRangeStopType.Absolute));
                colorProvider.RangeStops.Add(new HeatmapRangeStop(100, HeatmapRangeStopType.Absolute));


                pressureHeatmapControl.ColorProvider = colorProvider;
                pressureHeatmapControl.Refresh();
            }
            else
            {
                var palette = new Palette("Custom") {
                    Color.Red,
                    Color.Orange,
                    Color.Yellow,
                    Color.Green,
                    Color.Cyan,
                    Color.Blue,
                    Color.DarkBlue,
                    Color.DarkMagenta,
                };

                var colorProvider = new HeatmapRangeColorProvider
                {
                    Palette = palette,
                    ApproximateColors = true,
                };

                for (double i = 0.0; i <= 1.0; i += 0.125)
                {
                    colorProvider.RangeStops.Add(
                        new HeatmapRangeStop(i, HeatmapRangeStopType.Percentage));
                }

                pressureHeatmapControl.ColorProvider = colorProvider;
            }
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
            if (_wellList.Count == 0)
            {
                return;
            }

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
            double[][] coords = new double[_wellList.Count][];

            for (int i = 0; i < _wellList.Count; i++)
            {
                coords[i] = new double[] { _wellList[i].X, _wellList[i].Y };
            }
            GetWellsMinMax(
                out double minX, out double minY,
                out double maxX, out double maxY);

            double xRange;
            double yRange;
            if (_wellList.Count == 1)
            {
                xRange = 10.0; 
                yRange = 10.0;
            }
            else
            {
                xRange = Math.Abs(minX - maxX); 
                yRange = Math.Abs(minY - maxY);
            }

            minX -= xRange / 4.0;
            minY -= yRange / 4.0;
            maxX += xRange / 4.0;
            maxY += yRange / 4.0;

            double[] x = Linspace(minX, maxX,_xCount);
            double[] y = Linspace(minY, maxY, _yCount);
            
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
            int num = timesTrackBarControl.Value;
            int step = (int)stepNumericUpDown.Value;
            var days = num * step;
            var start = startDateEdit.DateTime;
            var date = start.AddDays(days);
            currentTimeTextEdit.Text = date.ToString("dd.MM.yyyy");
            if (_pressures.Count == 0)
            {
                return;
            }
            UpdateHeatMap(_pressures[num]);
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
