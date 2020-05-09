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
using SuspeSys.Client.Action.ProductionLineSet.Model;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Common;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class StatingInput : DevExpress.XtraEditors.XtraForm
    {
        public StatingInput()
        {
            InitializeComponent();
        }
        public StatingInput(IList<SuspeSys.Domain.Stating> _statingList,short mainTrackNumber) :this() {
            StatingList = _statingList;
            txtMainTrackNumber.Text = mainTrackNumber.ToString();
        }
        public StatingInfoModel StatingInfoModel {
            private set;
            get;
        }
        public IList<SuspeSys.Domain.Stating> StatingList { set; get; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                btnOk.Cursor = Cursors.WaitCursor;
                var beginNum = txtBeginNum.Text?.Trim();
                var endNum =txtEndNum.Text?.Trim();
                var mainTrackNumber =txtMainTrackNumber.Text?.Trim();
                if (string.IsNullOrEmpty(beginNum)) {
                    XtraMessageBox.Show("开始编号不能为空!","温馨提示");
                    txtBeginNum.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(endNum))
                {
                    XtraMessageBox.Show("结束编号不能为空!", "温馨提示");
                    txtEndNum.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(mainTrackNumber))
                {
                    XtraMessageBox.Show("主轨号不能为空!", "温馨提示");
                    txtMainTrackNumber.Focus();
                    return;
                }
                if (int.Parse(endNum)>255)
                {
                    XtraMessageBox.Show("站号不能大于255!", "温馨提示");
                    txtEndNum.Focus();
                    return;
                }
                StatingInfoModel = GetStatingInfoModel();
                var statList = CommonAction.GetList<DaoModel.Stating>("select * from Stating where deleted=0");
                var dt = statList;
                if (null != dt)
                {
                    //var isExistMainTrackNumber = dt.Where(f => f.MainTrackNumber.Equals(StatingInfoModel.MainTrackNumber.ToString())).ToList<DaoModel.Stating>().Count > 0;
                    //if (isExistMainTrackNumber)
                    //{
                    //    XtraMessageBox.Show("主轨号已存在", "温馨提示");
                    //    return;
                    //}

                    for (var index = StatingInfoModel.BeginNum; index <= StatingInfoModel.EndNum; index++)
                    {
                        var isExistStating = dt.Where(f => f.StatingNo.Trim().Equals(index.ToString()) && f.MainTrackNumber==StatingInfoModel.MainTrackNumber && f.Deleted.Value==0).ToList<DaoModel.Stating>().Count > 0;
                        if (isExistStating)
                        {
                            XtraMessageBox.Show(string.Format("主轨号【{0}】站号【{1}】已存在!",StatingInfoModel.MainTrackNumber,StatingInfoModel.BeginNum), "温馨提示");
                            return;
                        }
                    }
                }
                this.Close();
            }
            finally
            {
                btnOk.Cursor = Cursors.Default;
               
            }
        }
        StatingInfoModel GetStatingInfoModel()
        {
            var statingInfoModel = new StatingInfoModel();
            statingInfoModel.BeginNum = Convert.ToInt32(txtBeginNum.Text);
            statingInfoModel.EndNum = Convert.ToInt32(txtEndNum.Text);
            statingInfoModel.MainTrackNumber = Convert.ToInt32(txtMainTrackNumber.Text);
            return statingInfoModel;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}