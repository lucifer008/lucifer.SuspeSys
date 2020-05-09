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
using SuspeSys.Client.Action.SuspeRemotingClient;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    public partial class StatingPropertyMaintain : DevExpress.XtraEditors.XtraForm
    {
        public StatingPropertyMaintain()
        {
            InitializeComponent();
        }
        private int tag;
        public string StatingNo { set; get; }
        public string GroupNo { set; get; }
        public StatingPropertyMaintain(int _tag,string frmTitle,string propertyTitle):this() {
            this.tag = _tag;
            this.Text = frmTitle;
            this.lblTitle.Text = propertyTitle;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string groupNo = GroupNo;
            switch (tag) {
                case 1://修改站内数
                    int inStatingNum = int.Parse(txtValues.Text);
                    SuspeRemotingService.statingService.UpdateStatingInNum(groupNo,StatingNo, inStatingNum);
                    break;
                case 2://修改站容量
                    int capacity = int.Parse(txtValues.Text);
                    SuspeRemotingService.statingService.UpdateStatingCapacity(groupNo, StatingNo, capacity);
                    break;
            }
            MessageBox.Show("已保存");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}