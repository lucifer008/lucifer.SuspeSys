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
using SuspeSys.Client.Action.ProcessFlowChart;
using SuspeSys.Domain.Common;
using SuspeSys.Client.Modules.Ext;

namespace SuspeSys.Client.Modules
{
    public partial class SusFlowChartControl : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void QueryHandle(QueryModel queryModel);
        public event QueryHandle OnQuery;
        public SusFlowChartControl()
        {
            InitializeComponent();
        }

        private void coboFlowChart_SelectedValueChanged(object sender, EventArgs e)
        {
            if (OnQuery!=null) {
                var queryModel = new QueryModel() {
                    FlowChartId = ((SusComboBoxItem)coboFlowChart.SelectedItem).Value?.ToString()
                };
                OnQuery(queryModel);
            }
        }

        private void SusFlowChartControl_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            var action = new ProcessFlowChartQueryAction();
            var list = action.GetProcessFlowChartListByOnlineProducts(CurrentUser.Instance.CurrentSiteGroup?.GroupNo?.Trim());
            coboFlowChart.Properties.Items.Clear();
            foreach (var item in list)
            {
                coboFlowChart.Properties.Items.Add(new SusComboBoxItem() { Text = item.LinkName?.Trim(), Value = item.Id, Tag = item });
            }
            if(list.Count>0)
                coboFlowChart.SelectedIndex = 0;
        }
    }
}
