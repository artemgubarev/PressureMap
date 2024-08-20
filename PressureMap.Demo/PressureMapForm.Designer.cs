namespace PressureMap.Demo
{
    partial class PressureMapForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pressureMapControl1 = new PressureMap.PressureMapControl();
            this.SuspendLayout();
            // 
            // pressureMapControl1
            // 
            this.pressureMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pressureMapControl1.Location = new System.Drawing.Point(0, 0);
            this.pressureMapControl1.Name = "pressureMapControl1";
            this.pressureMapControl1.Size = new System.Drawing.Size(975, 584);
            this.pressureMapControl1.TabIndex = 0;
            // 
            // PressureMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 584);
            this.Controls.Add(this.pressureMapControl1);
            this.Name = "PressureMapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Карта давлений";
            this.ResumeLayout(false);

        }

        #endregion

        private PressureMapControl pressureMapControl1;
    }
}