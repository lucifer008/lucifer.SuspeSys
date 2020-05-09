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

namespace SuspeSys.Client
{
    public partial class SubmitStatus : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public SubmitStatus()
        {
            InitializeComponent();
        }

        private void SubmitStatus_Load(object sender, EventArgs e)
        {
            if (null != MessageReceived) {
                MessageReceived += SubmitStatus_MessageReceived;
            }
        }

        private void SubmitStatus_MessageReceived(object sender, MessageEventArgs e)
        {
            progressBarControl2.EditValue = e.ProgressValue;
            if (e.ProgressValue==100) {
                
                this.Close();
            }
        }
    }
}