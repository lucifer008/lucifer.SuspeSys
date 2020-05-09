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
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Domain;
using SuspeSys.Client.Action.SuspeRemotingClient;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    public partial class EmployeeDialog : DevExpress.XtraEditors.XtraForm
    {
        public EmployeeDialog()
        {
            InitializeComponent();
        }
        public EmployeeModel Employee;
        public int MainTrackNumber;
        public int StatingNo;
        private ProductRealtimeInfoIndex pRealtimeInfo;
        public EmployeeDialog(EmployeeModel emModel,int mainTrackNumber,int statingNo, ProductRealtimeInfoIndex _pRealtimeInfo) : this()
        {
            Employee = emModel;
            this.MainTrackNumber = mainTrackNumber;
            this.StatingNo = statingNo;
            this.pRealtimeInfo = _pRealtimeInfo;
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Code",Visible=true,Name="Code"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="姓名",FieldName="RealName",Visible=true,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="人事组别",FieldName="SiteGroupNo",Visible=true,Name="GroupNO"}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            gridView.Columns["Code"].OptionsColumn.AllowEdit = false;
            gridView.Columns["RealName"].OptionsColumn.AllowEdit = false;
            gridView.Columns["SiteGroupNo"].OptionsColumn.AllowEdit = false;

            gridView.MouseDown += GridView_MouseDown;
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                var gv = gridControl1.MainView as SusGridView;
                if (null == gv) return;
                var empoyee = gv.GetRow(gv.FocusedRowHandle) as EmployeeModel;
                var info = string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompConfirmLoginEmployee")//"确认登录员工:{0}【{1}】"
                    , empoyee?.Code?.Trim(),
                        empoyee?.RealName?.Trim());
                if (null != Employee)
                {
                    info = string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompLoginEmInfo")//"工位【{0}】已有员工:{1}【{2}】登录,需替换为 员工:{3}【{4}】"
                        , 
                        Employee?.StatingNo?.Trim(),
                        Employee?.RealName?.Trim(),
                        Employee?.Code?.Trim(),
                        empoyee?.Code?.Trim(),
                        empoyee?.RealName?.Trim());
                }
                
                var rs = MessageBox.Show(info, Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int mainTrackNo =MainTrackNumber;
                    int statingNo = StatingNo;
                   
                    int type = 0;
                    SuspeRemotingService.statingService.ManualEmployeeLoginStating(mainTrackNo, statingNo, type, empoyee);
                    pRealtimeInfo.RefshData();
                    //this.Close();
                }
            }
        }

        private void EmployeeDialog_Load(object sender, EventArgs e)
        {
            BindGridHeader(gridControl1);
            Query();
        }

        private void Query()
        {
            var groupNo = txtGroup.Text?.Trim();
            var code = txtEmployeeCodeOrName.Text?.Trim();
            var list = StatingServiceImpl.Instance.GetStatingGroupEmployeeList(groupNo, code, code);
            gridControl1.DataSource = list;
        }

        private void txtGroup_EditValueChanged(object sender, EventArgs e)
        {
            Query();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Client.Action.LanguageAction.Instance.ChangeLanguage(this.Controls);
        }
    }
}