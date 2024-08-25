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
        private const int _xCount = 500;
        private const int _yCount = 500;
        private List<double[,]> _pressures;
        

        public PressureMapControl()
        {
            InitializeComponent();
            InitializeWellTable();
            InitializeWellChartControl();
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
                var well = _wellList[focusedRowHandle];
                var dataTable = (DataTable)wellGridControl.DataSource;
                dataTable.Rows.RemoveAt(focusedRowHandle);
                _wellSeries.Points.RemoveAt(focusedRowHandle);
                _wellList.RemoveAt(focusedRowHandle);
                wellMapChartControl.RefreshData();
                wellComboBox.Properties.Items.Remove(well.Number);
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
            _wellList.AddRange(wells);
            foreach (var well in wells)
            {
                _wellsDataTable.Rows.Add(new object[] { well.Number, well.X, well.Y });
                var point = new SeriesPoint(well.X, well.Y);
                point.Tag = well.Number;
                _wellSeries.Points.Add(point);
                wellComboBox.Properties.Items.Add(well.Number);
                if (wellComboBox.Properties.Items.Count == 1)
                {
                    wellComboBox.SelectedIndex = 0;
                }
            }
            wellMapChartControl.RefreshData();
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
            var dateTimes = new List<DateTime>();
            var fluidFlows = new List<(int Num, double[] Values)>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                
                Excel.Worksheet worksheetX = workbook.Sheets[3];
                Excel.Worksheet worksheetY = workbook.Sheets[4];
                Excel.Worksheet worksheetQ = workbook.Sheets[1];
                
                int numRow = 1; 
                int valueRow = 2;

                ParseWellCoords(worksheetX, numRow, valueRow, xCoords);
                ParseWellCoords(worksheetY, numRow, valueRow, yCoords);
                ReadDateTimeFromWorksheet(worksheetQ, dateTimes);
                ReadExcelValues(worksheetQ, fluidFlows);

                var combined = from x in xCoords  
                               join y in yCoords on x.Num equals y.Num
                               select (x.Num, x.X, y.Y);

                var wells = new List<Well>();
                foreach (var value in combined)
                {
                    var qValues = 
                        fluidFlows.FirstOrDefault(item => item.Num == value.Num);
                    var dateAndQValues =
                        CombineArrays(dateTimes.ToArray(), qValues.Values);
                    wells.Add(new Well(value.Num, value.X, value.Y, dateAndQValues));
                }
                AddNewWells(wells);

                workbook.Close(false);
                excelApp.Quit();
                
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheetX);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private (DateTime Date, double Q)[] CombineArrays(DateTime[] dates, double[] values)
        {
            if (dates.Length != values.Length)
            {
                throw new ArgumentException("Arrays must have the same length.");
            }
            var combinedArray = dates.Zip(values, (date, value) 
                => (Date: date, Q: value)).ToArray();
            return combinedArray;
        }

        private void ReadExcelValues(
            Excel.Worksheet worksheet, List<(int Num, double[] Values)> dataList)
        {
            int lastRow = worksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
            int lastCol = worksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column;
            
            var excelApp = (Excel.Application)worksheet.Application;
            bool originalScreenUpdating = excelApp.ScreenUpdating;
            bool originalCalculation = excelApp.Calculation == Excel.XlCalculation.xlCalculationAutomatic;

            excelApp.ScreenUpdating = false;
            excelApp.Calculation = Excel.XlCalculation.xlCalculationManual;

            try
            {
                var range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[lastRow, lastCol]];
                object[,] values = (object[,])range.Value2;
                
                for (int col = 2; col <= lastCol; col++)
                {
                    var num = values[1, col];
                    if (num != null)
                    {
                        int numValue = Convert.ToInt32(num);

                        List<double> valuesList = new List<double>();
                        
                        for (int row = 2; row <= lastRow; row++)
                        {
                            var cellValue = values[row, col];
                            if (cellValue != null)
                            {
                                valuesList.Add(Convert.ToDouble(cellValue));
                            }
                        }
                        dataList.Add((numValue, valuesList.ToArray()));
                    }
                }
            }
            finally
            {
                excelApp.ScreenUpdating = originalScreenUpdating;
                excelApp.Calculation = originalCalculation ? Excel.XlCalculation.xlCalculationAutomatic : Excel.XlCalculation.xlCalculationManual;
            }
        }

        private void ReadDateTimeFromWorksheet(
            Excel.Worksheet worksheet, List<DateTime> dateTimeValues)
        {
            int row = 2;
            object cellValue;

            // Assuming dateTimeValues is already declared and initialized
            while ((cellValue = worksheet.Cells[row++, 1].Value) != null)
            {
                if (DateTime.TryParse(cellValue.ToString(), out DateTime dateValue))
                {
                    dateTimeValues.Add(dateValue);
                }
            }
        }

        private void ParseWellCoords(Excel.Worksheet worksheet, int numRow, int valueRow,
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
                var palette = new Palette("Custom Gradient",
                    new PaletteEntry[] {
                        new PaletteEntry(Color.FromArgb(255, 0, 0)),   // Red (красный)
                        new PaletteEntry(Color.FromArgb(255, 50, 0)),
                        new PaletteEntry(Color.FromArgb(255, 100, 0)),
                        new PaletteEntry(Color.FromArgb(255, 127, 0)),
                        new PaletteEntry(Color.FromArgb(255, 191, 0)),
                        new PaletteEntry(Color.FromArgb(255, 255, 0)), // Yellow
                        new PaletteEntry(Color.FromArgb(191, 255, 0)),
                        new PaletteEntry(Color.FromArgb(127, 255, 0)),
                        new PaletteEntry(Color.FromArgb(0, 255, 0)),   // Green
                        new PaletteEntry(Color.FromArgb(0, 255, 127)),
                        new PaletteEntry(Color.FromArgb(0, 255, 255)),
                        new PaletteEntry(Color.FromArgb(0, 191, 255)),
                        new PaletteEntry(Color.FromArgb(0, 100, 255)),
                        new PaletteEntry(Color.FromArgb(0, 0, 255)),
                        new PaletteEntry(Color.FromArgb(75, 0, 130)),
                        new PaletteEntry(Color.FromArgb(139, 0, 255)),
                        new PaletteEntry(Color.FromArgb(128, 0, 128)), // Dark Purple (темно-фиолетовый)
                    });

                var colorProvider = new HeatmapRangeColorProvider
                {
                    Palette = palette,
                    ApproximateColors = true,
                };

                double increment = 1.0 / palette.Count;
                for (double i = 0.0; i <= 1.0; i += increment)
                {
                    colorProvider.RangeStops.Add(
                        new HeatmapRangeStop(i, HeatmapRangeStopType.Percentage));
                }
                
                pressureHeatmapControl.ColorProvider = colorProvider;

                //pressureHeatmapControl.PaletteRepository.Add("RainbowPalette", palette);
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

            double[] x = Linspace(minX, maxX,_xCount);
            double[] y = Linspace(minY, maxY, _yCount);
            
            _pressures.Clear();
            for (int i = 0; i < times.Length; i++)
            {
                double time = times[i];
                double[,] pressure = calculator.ComputatePressure(x, y, time, coords);
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

        private void wellComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = wellComboBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedValue = wellComboBox.Properties.Items[selectedIndex].ToString();
                int number = int.Parse(selectedValue);
                DrawQ(number);
            }
            else
            {
                return;
            }
        }

        private void DrawQ(int wellNumber)
        {
            var well = _wellList.FirstOrDefault(w => w.Number == wellNumber);
            if (well.Q == null)
            {
                return;
            }
            Series series = new Series("Series1", ViewType.StepLine);

            foreach (var point in well.Q)
            {
                series.Points.Add(new SeriesPoint(point.Date, point.value));
                series.Points.Add(new SeriesPoint(point.Date.AddDays(1), point.value));
            }

            var diagram = new XYDiagram();
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
            diagram.AxisX.Label.TextPattern = "{A:MMM yyyy}";

            chartControl1.BeginInit();
            chartControl1.Diagram = diagram;
            chartControl1.Series.Clear();
            chartControl1.Series.Add(series);
            chartControl1.EndInit();
        }
    }
}