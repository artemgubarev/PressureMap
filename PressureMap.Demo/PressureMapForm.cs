using System.Windows.Forms;

namespace PressureMap.Demo
{
    public partial class PressureMapForm : DevExpress.XtraEditors.XtraForm
    {
        public PressureMapForm()
        {
            InitializeComponent();
            
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            
            int formWidth = screenWidth * 4 / 5;
            int formHeight = screenHeight * 6 / 7;
            
            this.Width = formWidth;
            this.Height = formHeight;
        }
    }
}