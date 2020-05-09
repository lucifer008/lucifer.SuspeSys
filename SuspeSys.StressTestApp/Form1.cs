using SuspeSys.StressTestApp.Rework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.StressTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // new ReworkDecodeUploadUnitCompanyTest();
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            //1000-1050
            btnStart1.Cursor = Cursors.WaitCursor;
            var rut=  new ReworkDecodeUploadUnitCompanyTest();
            rut.TestManyThreadOneMainTrackNumberCommonHanger();

            btnStart1.Cursor = Cursors.Default;
            btnStart1.Enabled = false;
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            //2000-2050
            btnStart2.Cursor = Cursors.WaitCursor;
            var rut = new ReworkDecodeUploadUnitCompanyTest();
            rut.TestManyThreadOneMainTrackNumberCommonHanger2();

            btnStart2.Cursor = Cursors.Default;
            btnStart2.Enabled = false;
        }

        private void btnStart3_Click(object sender, EventArgs e)
        {
            //3000-3050
            btnStart3.Cursor = Cursors.WaitCursor;
            var rut = new ReworkDecodeUploadUnitCompanyTest();
            rut.TestManyThreadOneMainTrackNumberCommonHanger3();

            btnStart3.Cursor = Cursors.Default;
            btnStart3.Enabled = false;
        }

        private void btnStart4_Click(object sender, EventArgs e)
        {
            btnStart4.Cursor = Cursors.WaitCursor;
            var rut = new ReworkDecodeUploadUnitCompanyTest();
           
            rut.TestManyThreadOneMainTrackNumberCommonHangerExt(4000, 50);
            btnStart4.Enabled = false;
        }

        private void btnStart5_Click(object sender, EventArgs e)
        {
            btnStart5.Cursor = Cursors.WaitCursor;
            var rut = new ReworkDecodeUploadUnitCompanyTest();

            rut.TestManyThreadOneMainTrackNumberCommonHangerExt(5000, 50);
            btnStart5.Enabled = false;
        }

        private void btnStart6_Click(object sender, EventArgs e)
        {
            btnStart6.Cursor = Cursors.WaitCursor;
            var rut = new ReworkDecodeUploadUnitCompanyTest();

            rut.TestManyThreadOneMainTrackNumberCommonHangerExt(6000, 50);
            btnStart6.Enabled = false;
        }
    }
}
