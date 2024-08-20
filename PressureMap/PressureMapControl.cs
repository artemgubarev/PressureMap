namespace PressureMap
{
    using DevExpress.XtraCharts;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public partial class PressureMapControl : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable _wellsDataTable;
        private Series _series;

        public PressureMapControl()
        {
            InitializeComponent();
            InitializeWellTable();
            InitializeWellChartControl();
        }
        
        private void InitializeWellTable()
        {
            _wellsDataTable = new DataTable();
            _wellsDataTable.Columns.Add("X");
            _wellsDataTable.Columns.Add("Y");
            wellGridControl.DataSource = _wellsDataTable;
        }

        private void InitializeWellChartControl()
        {
            _series = new Series("Скважины", ViewType.Point);
            wellMapChartControl.Series.Add(_series);

            wellMapChartControl.BeginInit();

            XYDiagram diagram = new XYDiagram();

            // Настройка свойств диаграммы (если необходимо)
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;
            diagram.EnableAxisYScrolling = true;
            diagram.EnableAxisYZooming = true;

            // Настройка сетки для оси X
            diagram.AxisX.GridLines.Visible = true;
            diagram.AxisX.GridLines.MinorVisible = false;
            diagram.AxisX.GridLines.Color = Color.LightGray;
            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Dash;

            // Настройка сетки для оси Y
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.MinorVisible = false;
            diagram.AxisY.GridLines.Color = Color.LightGray;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;

            // Назначение созданной диаграммы для ChartControl
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
                _series.Points.RemoveAt(focusedRowHandle);
                wellMapChartControl.RefreshData();
            }
        }

        private void addWellButton_Click(object sender, System.EventArgs e)
        {
            string xStr = xTextEdit.Text;
            string yStr = yTextEdit.Text;

            bool isCorrect = double.TryParse(
                xStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double x);

            if (!double.TryParse(
                    yStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
            {
                isCorrect = false;
            }

            if (!isCorrect)
            {
                MessageBox.Show("Некорректные значения координат скважины");
                return;
            }
            
            _wellsDataTable.Rows.Add(new string[] { xStr, yStr });
            
            var point = new SeriesPoint(x,y);
            _series.Points.Add(point);
            wellMapChartControl.RefreshData();

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
    }
}
