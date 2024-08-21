namespace PressureMap
{
    partial class PressureMapControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wellMapChartControl = new DevExpress.XtraCharts.ChartControl();
            this.addWellButton = new DevExpress.XtraEditors.SimpleButton();
            this.wellGroupControl = new DevExpress.XtraEditors.GroupControl();
            this.loadFromExcelButton = new DevExpress.XtraEditors.SimpleButton();
            this.xLabel = new DevExpress.XtraEditors.LabelControl();
            this.yLabel = new DevExpress.XtraEditors.LabelControl();
            this.deleteWellButton = new DevExpress.XtraEditors.SimpleButton();
            this.yTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.xTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.wellGridControl = new DevExpress.XtraGrid.GridControl();
            this.wellGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.injectionDataChartControl = new DevExpress.XtraCharts.ChartControl();
            this.injectionDataGridControl = new DevExpress.XtraGrid.GridControl();
            this.injectionDataGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.wellMapChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGroupControl)).BeginInit();
            this.wellGroupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataGridView)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wellMapChartControl
            // 
            this.wellMapChartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wellMapChartControl.CrosshairOptions.ArgumentLineColor = System.Drawing.Color.Black;
            this.wellMapChartControl.CrosshairOptions.CrosshairLabelBackColor = System.Drawing.Color.White;
            this.wellMapChartControl.CrosshairOptions.CrosshairLabelTextOptions.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.wellMapChartControl.CrosshairOptions.ShowArgumentLabels = true;
            this.wellMapChartControl.CrosshairOptions.ShowValueLabels = true;
            this.wellMapChartControl.CrosshairOptions.ShowValueLine = true;
            this.wellMapChartControl.CrosshairOptions.ValueLineColor = System.Drawing.Color.Black;
            this.wellMapChartControl.Location = new System.Drawing.Point(3, 3);
            this.wellMapChartControl.Name = "wellMapChartControl";
            this.wellMapChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.wellMapChartControl.Size = new System.Drawing.Size(481, 724);
            this.wellMapChartControl.TabIndex = 0;
            // 
            // addWellButton
            // 
            this.addWellButton.AllowFocus = false;
            this.addWellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addWellButton.Location = new System.Drawing.Point(8, 695);
            this.addWellButton.Name = "addWellButton";
            this.addWellButton.Size = new System.Drawing.Size(116, 23);
            this.addWellButton.TabIndex = 1;
            this.addWellButton.Text = "Добавить";
            this.addWellButton.Click += new System.EventHandler(this.addWellButton_Click);
            // 
            // wellGroupControl
            // 
            this.wellGroupControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wellGroupControl.Controls.Add(this.loadFromExcelButton);
            this.wellGroupControl.Controls.Add(this.xLabel);
            this.wellGroupControl.Controls.Add(this.yLabel);
            this.wellGroupControl.Controls.Add(this.deleteWellButton);
            this.wellGroupControl.Controls.Add(this.yTextEdit);
            this.wellGroupControl.Controls.Add(this.xTextEdit);
            this.wellGroupControl.Controls.Add(this.wellGridControl);
            this.wellGroupControl.Controls.Add(this.addWellButton);
            this.wellGroupControl.Location = new System.Drawing.Point(490, 3);
            this.wellGroupControl.Name = "wellGroupControl";
            this.wellGroupControl.Size = new System.Drawing.Size(284, 723);
            this.wellGroupControl.TabIndex = 2;
            this.wellGroupControl.Text = "Скважины";
            // 
            // loadFromExcelButton
            // 
            this.loadFromExcelButton.AllowFocus = false;
            this.loadFromExcelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadFromExcelButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(255)))), ((int)(((byte)(222)))));
            this.loadFromExcelButton.Appearance.Options.UseBackColor = true;
            this.loadFromExcelButton.Location = new System.Drawing.Point(163, 638);
            this.loadFromExcelButton.Name = "loadFromExcelButton";
            this.loadFromExcelButton.Size = new System.Drawing.Size(116, 23);
            this.loadFromExcelButton.TabIndex = 9;
            this.loadFromExcelButton.Text = "Загрузить из Excel";
            this.loadFromExcelButton.Click += new System.EventHandler(this.loadFromExcelButton_Click);
            // 
            // xLabel
            // 
            this.xLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xLabel.Location = new System.Drawing.Point(8, 641);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(10, 13);
            this.xLabel.TabIndex = 8;
            this.xLabel.Text = "X:";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yLabel.Location = new System.Drawing.Point(8, 671);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(10, 13);
            this.yLabel.TabIndex = 7;
            this.yLabel.Text = "Y:";
            // 
            // deleteWellButton
            // 
            this.deleteWellButton.AllowFocus = false;
            this.deleteWellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteWellButton.Location = new System.Drawing.Point(165, 695);
            this.deleteWellButton.Name = "deleteWellButton";
            this.deleteWellButton.Size = new System.Drawing.Size(114, 23);
            this.deleteWellButton.TabIndex = 3;
            this.deleteWellButton.Text = "Удалить";
            this.deleteWellButton.Click += new System.EventHandler(this.deleteWellButton_Click);
            // 
            // yTextEdit
            // 
            this.yTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yTextEdit.Location = new System.Drawing.Point(24, 668);
            this.yTextEdit.Name = "yTextEdit";
            this.yTextEdit.Size = new System.Drawing.Size(100, 20);
            this.yTextEdit.TabIndex = 6;
            // 
            // xTextEdit
            // 
            this.xTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xTextEdit.Location = new System.Drawing.Point(24, 638);
            this.xTextEdit.Name = "xTextEdit";
            this.xTextEdit.Size = new System.Drawing.Size(100, 20);
            this.xTextEdit.TabIndex = 5;
            // 
            // wellGridControl
            // 
            this.wellGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wellGridControl.Location = new System.Drawing.Point(5, 26);
            this.wellGridControl.MainView = this.wellGridView;
            this.wellGridControl.Name = "wellGridControl";
            this.wellGridControl.Size = new System.Drawing.Size(274, 606);
            this.wellGridControl.TabIndex = 4;
            this.wellGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.wellGridView});
            // 
            // wellGridView
            // 
            this.wellGridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.wellGridView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.wellGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.wellGridView.GridControl = this.wellGridControl;
            this.wellGridView.Name = "wellGridView";
            this.wellGridView.OptionsBehavior.ReadOnly = true;
            this.wellGridView.OptionsCustomization.AllowSort = false;
            this.wellGridView.OptionsFilter.AllowFilterEditor = false;
            this.wellGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.wellGridView.OptionsView.ShowGroupPanel = false;
            this.wellGridView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.wellGridView_ShowingEditor);
            this.wellGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wellGridView_KeyDown);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(886, 732);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.wellMapChartControl);
            this.xtraTabPage1.Controls.Add(this.wellGroupControl);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(777, 730);
            this.xtraTabPage1.Text = "Загрузка скважин";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.injectionDataChartControl);
            this.xtraTabPage2.Controls.Add(this.injectionDataGridControl);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(777, 730);
            this.xtraTabPage2.Text = "Данные закачки";
            // 
            // injectionDataChartControl
            // 
            this.injectionDataChartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.injectionDataChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.injectionDataChartControl.Location = new System.Drawing.Point(3, 3);
            this.injectionDataChartControl.Name = "injectionDataChartControl";
            this.injectionDataChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.injectionDataChartControl.Size = new System.Drawing.Size(579, 724);
            this.injectionDataChartControl.TabIndex = 6;
            // 
            // injectionDataGridControl
            // 
            this.injectionDataGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.injectionDataGridControl.Location = new System.Drawing.Point(588, 3);
            this.injectionDataGridControl.MainView = this.injectionDataGridView;
            this.injectionDataGridControl.Name = "injectionDataGridControl";
            this.injectionDataGridControl.Size = new System.Drawing.Size(186, 724);
            this.injectionDataGridControl.TabIndex = 5;
            this.injectionDataGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.injectionDataGridView});
            // 
            // injectionDataGridView
            // 
            this.injectionDataGridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.injectionDataGridView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.injectionDataGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.injectionDataGridView.GridControl = this.injectionDataGridControl;
            this.injectionDataGridView.Name = "injectionDataGridView";
            this.injectionDataGridView.OptionsBehavior.ReadOnly = true;
            this.injectionDataGridView.OptionsCustomization.AllowSort = false;
            this.injectionDataGridView.OptionsFilter.AllowFilterEditor = false;
            this.injectionDataGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.injectionDataGridView.OptionsView.ShowGroupPanel = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.trackBarControl1);
            this.xtraTabPage3.Controls.Add(this.chartControl1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(777, 730);
            this.xtraTabPage3.Text = "График расхода";
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(3, 3);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(771, 667);
            this.chartControl1.TabIndex = 7;
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarControl1.Location = new System.Drawing.Point(4, 682);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Size = new System.Drawing.Size(771, 45);
            this.trackBarControl1.TabIndex = 8;
            this.trackBarControl1.Scroll += new System.EventHandler(this.trackBarControl1_Scroll);
            // 
            // PressureMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "PressureMapControl";
            this.Size = new System.Drawing.Size(886, 732);
            ((System.ComponentModel.ISupportInitialize)(this.wellMapChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGroupControl)).EndInit();
            this.wellGroupControl.ResumeLayout(false);
            this.wellGroupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.injectionDataGridView)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl wellMapChartControl;
        private DevExpress.XtraEditors.SimpleButton addWellButton;
        private DevExpress.XtraEditors.GroupControl wellGroupControl;
        private DevExpress.XtraGrid.GridControl wellGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView wellGridView;
        private DevExpress.XtraEditors.SimpleButton deleteWellButton;
        private DevExpress.XtraEditors.LabelControl xLabel;
        private DevExpress.XtraEditors.LabelControl yLabel;
        private DevExpress.XtraEditors.TextEdit yTextEdit;
        private DevExpress.XtraEditors.TextEdit xTextEdit;
        private DevExpress.XtraEditors.SimpleButton loadFromExcelButton;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl injectionDataGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView injectionDataGridView;
        private DevExpress.XtraCharts.ChartControl injectionDataChartControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}
