using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SuspeSys.Server
{
    public partial class AuthorizationIndex : DevExpress.XtraEditors.XtraForm
    {
        public AuthorizationIndex()
        {
            InitializeComponent();
        }

        private void AuthorizationIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}