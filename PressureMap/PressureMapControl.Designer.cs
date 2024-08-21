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
            this.xLabel = new DevExpress.XtraEditors.LabelControl();
            this.yLabel = new DevExpress.XtraEditors.LabelControl();
            this.deleteWellButton = new DevExpress.XtraEditors.SimpleButton();
            this.yTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.xTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.wellGridControl = new DevExpress.XtraGrid.GridControl();
            this.wellGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.wellMapChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGroupControl)).BeginInit();
            this.wellGroupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridView)).BeginInit();
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
            this.wellMapChartControl.Size = new System.Drawing.Size(597, 583);
            this.wellMapChartControl.TabIndex = 0;
            // 
            // addWellButton
            // 
            this.addWellButton.AllowFocus = false;
            this.addWellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addWellButton.Location = new System.Drawing.Point(8, 555);
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
            this.wellGroupControl.Controls.Add(this.xLabel);
            this.wellGroupControl.Controls.Add(this.yLabel);
            this.wellGroupControl.Controls.Add(this.deleteWellButton);
            this.wellGroupControl.Controls.Add(this.yTextEdit);
            this.wellGroupControl.Controls.Add(this.xTextEdit);
            this.wellGroupControl.Controls.Add(this.wellGridControl);
            this.wellGroupControl.Controls.Add(this.addWellButton);
            this.wellGroupControl.Location = new System.Drawing.Point(606, 3);
            this.wellGroupControl.Name = "wellGroupControl";
            this.wellGroupControl.Size = new System.Drawing.Size(284, 583);
            this.wellGroupControl.TabIndex = 2;
            this.wellGroupControl.Text = "Скважины";
            // 
            // xLabel
            // 
            this.xLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xLabel.Location = new System.Drawing.Point(8, 501);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(10, 13);
            this.xLabel.TabIndex = 8;
            this.xLabel.Text = "X:";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yLabel.Location = new System.Drawing.Point(8, 531);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(10, 13);
            this.yLabel.TabIndex = 7;
            this.yLabel.Text = "Y:";
            // 
            // deleteWellButton
            // 
            this.deleteWellButton.AllowFocus = false;
            this.deleteWellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteWellButton.Location = new System.Drawing.Point(165, 555);
            this.deleteWellButton.Name = "deleteWellButton";
            this.deleteWellButton.Size = new System.Drawing.Size(114, 23);
            this.deleteWellButton.TabIndex = 3;
            this.deleteWellButton.Text = "Удалить";
            this.deleteWellButton.Click += new System.EventHandler(this.deleteWellButton_Click);
            // 
            // yTextEdit
            // 
            this.yTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yTextEdit.Location = new System.Drawing.Point(24, 528);
            this.yTextEdit.Name = "yTextEdit";
            this.yTextEdit.Size = new System.Drawing.Size(100, 20);
            this.yTextEdit.TabIndex = 6;
            // 
            // xTextEdit
            // 
            this.xTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xTextEdit.Location = new System.Drawing.Point(24, 498);
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
            this.wellGridControl.Size = new System.Drawing.Size(274, 466);
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
            // PressureMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wellGroupControl);
            this.Controls.Add(this.wellMapChartControl);
            this.Name = "PressureMapControl";
            this.Size = new System.Drawing.Size(893, 589);
            ((System.ComponentModel.ISupportInitialize)(this.wellMapChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGroupControl)).EndInit();
            this.wellGroupControl.ResumeLayout(false);
            this.wellGroupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellGridView)).EndInit();
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
    }
}
