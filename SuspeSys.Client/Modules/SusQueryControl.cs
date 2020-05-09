using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using SuspeSys.Domain;
using SuspeSys.Client.Action.Query;
using DevExpress.XtraEditors.Controls;

namespace SuspeSys.Client.Modules
{
    [DXToolboxItem(DXToolboxItemKind.Free)]
    public partial class SusQueryControl : DevExpress.XtraEditors.XtraUserControl
    {
        public SusQueryControl()
        {
            InitializeComponent();
            InitEvent();
            InitControl();
        }

        private void InitControl()
        {
            //制单
            var pc = new PopupContainerControl();
            pc.Width = 533;
            pc.Height = 400;
            pc.Controls.Add(new SusEmpty(1) { Tag = 1, pcEdit = txtProcessOrderNo });
            txtProcessOrderNo.Properties.PopupControl = pc;

            //PO
            var pcPO = new PopupContainerControl();
            pcPO.Width = 533;
            pcPO.Height = 400;
            pcPO.Controls.Add(new SusEmpty(2) { Tag = 2, pcEdit = txtPO });
            txtPO.Properties.PopupControl = pcPO;

            //款式
            var pcStyle = new PopupContainerControl();
            pcStyle.Width = 533;
            pcStyle.Height = 400;
            pcStyle.Controls.Add(new SusEmpty(3) { Tag = 3, pcEdit = txtStyleCode });
            txtStyleCode.Properties.PopupControl = pcStyle;

            //员工
            var pcEmployee = new PopupContainerControl();
            pcEmployee.Width = 533;
            pcEmployee.Height = 400;
            pcEmployee.Controls.Add(new SusEmpty(4) { Tag = 4, pcEdit = txtEmployeeName });
            txtEmployeeName.Properties.PopupControl = pcEmployee;


            //颜色
            var colorQueryAction = new CommonQueryAction<PoColor>();
            colorQueryAction.GetList();
            var list = colorQueryAction.Model.List;
            txtColor.Properties.Items.Clear();
            foreach (var m in list)
            {
                var cb = new CheckedListBoxItem(m.Id, m.ColorValue?.Trim(), m);
                txtColor.Properties.Items.Add(cb);
            }
            //尺码
            var sizeQueryAction = new CommonQueryAction<PSize>();
            sizeQueryAction.GetList();
            var sizeList = sizeQueryAction.Model.List;
            txtSize.Properties.Items.Clear();
            foreach (var m in sizeList)
            {
                var cb = new CheckedListBoxItem(m.Id, m.Size?.Trim(), m);
                txtSize.Properties.Items.Add(cb);
            }
            //车间
            var workShopQueryAction = new CommonQueryAction<Workshop>();
            workShopQueryAction.GetList();
            var workshopList = workShopQueryAction.Model.List;
            txtWorkshop.Properties.Items.Clear();
            foreach (var m in workshopList)
            {
                var cb = new CheckedListBoxItem(m.Id, m.WorName?.Trim(), m);
                txtWorkshop.Properties.Items.Add(cb);
            }
            //生产组别
            var siteGroupQueryAction = new CommonQueryAction<SiteGroup>();
            siteGroupQueryAction.GetList();
            var siteGroupList = siteGroupQueryAction.Model.List;
            txtGroupNo.Properties.Items.Clear();
            foreach (var m in siteGroupList)
            {
                var cb = new CheckedListBoxItem(m.Id, m.GroupNo?.Trim(), m);
                txtGroupNo.Properties.Items.Add(cb);
            }
        }

        public delegate void QueryHandle(QueryModel queryModel);
        public event QueryHandle OnQuery;
        void InitEvent()
        {
            this.rgDateRange.SelectedIndexChanged += new System.EventHandler(this.rgDateRange_SelectedIndexChanged);

        }
        private void rgDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDateRange();
        }

        private void BindDateRange()
        {
            var sValue = rgDateRange.Properties.Items[rgDateRange.SelectedIndex].Value?.ToString();
            switch (sValue)
            {
                case "-1"://全部
                    dateBegin.Text = string.Empty;
                    dateEnd.Text = string.Empty;
                    break;
                case "0"://今天
                    dateBegin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "Yesterday":
                    dateBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LatelyThreeDay":
                    dateBegin.Text = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "CurrentWeek":
                    dateBegin.Text = GetWeekFirstDayMon(DateTime.Now).ToString("yyyy-MM-dd");
                    dateEnd.Text = GetWeekLastDaySun(DateTime.Now).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "CurrentMonth":
                    dateBegin.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-01");
                    dateEnd.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LastWeek":
                    dateBegin.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).AddDays(6).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LastMonth":
                    dateBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                    dateEnd.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
            }
        }
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
        QueryModel GetQueryCondition()
        {
            var model = new QueryModel();
            model.ProcessOrderNo = txtProcessOrderNo.Text?.Trim();
            model.SusColor = txtColor.Text?.Trim();
            model.SusSize = txtSize.Text?.Trim();
            model.SusStyle = txtStyleCode.Text?.Trim();
            model.Workshop = txtWorkshop.Text?.Trim();
            model.PO = txtPO.Text?.Trim();
            model.FlowSelection = txtFlowSelection.Text?.Trim();
            model.BeginDate = dateBegin.Text;
            model.EndDate = dateEnd.Text;
            model.GroupNo = txtGroupNo.Text?.Trim();
            model.EmployeeName = txtEmployeeName.Text?.Trim();
            return model;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Cursor = Cursors.WaitCursor;
                if (null == OnQuery)
                {
                    MessageBox.Show("查询未实现!");
                    return;
                }
                OnQuery(GetQueryCondition());
            }
            finally
            {
                btnQuery.Cursor = Cursors.Default;
            }
        }

        private void txtProcessOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            XtraMessageBox.Show(e.KeyChar.ToString());
        }

        private void txtProcessOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    btnQuery.Cursor = Cursors.WaitCursor;
                    if (null == OnQuery)
                    {
                        MessageBox.Show("查询未实现!");
                        return;
                    }
                    OnQuery(GetQueryCondition());
                }
                finally
                {
                    btnQuery.Cursor = Cursors.Default;
                }
            }
        }
    }
    public class QueryModel
    {
        /// <summary>
        /// 制单号
        /// </summary>
        public string ProcessOrderNo { set; get; }
        /// <summary>
        /// PO号
        /// </summary>
        public string PO { set; get; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string SusColor { set; get; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string SusSize { set; get; }
        /// <summary>
        /// 款式
        /// </summary>
        public string SusStyle { set; get; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string Workshop { set; get; }
        /// <summary>
        /// 工段
        /// </summary>
        public string FlowSelection { set; get; }
        /// <summary>
        /// 组
        /// </summary>
        public string GroupNo { set; get; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate { set; get; }
        /// <summary>
        /// 截至日期
        /// </summary>
        public string EndDate { set; get; }
        /// <summary>
        /// 工艺图Id
        /// </summary>
        public string FlowChartId { set; get; }
        /// <summary>
        /// 员工
        /// </summary>
        public string EmployeeName { set; get; }
    }
}
