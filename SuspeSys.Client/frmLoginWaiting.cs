using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client
{
    public partial class frmLoginWaiting : SplashScreen
    {
        public frmLoginWaiting()
        {
            InitializeComponent();
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }



        public enum SplashScreenCommand
        {
        }
    }
}
