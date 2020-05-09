using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuspeSys.Client.Action.PersonnelManagement;
using SuspeSys.Client.Action.Common;
using SuspeSys.Domain.Ext;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class CardInfoAdd : Ext.SusXtraUserControl
    {
        EmployeeAddAction _action = new EmployeeAddAction();

        public CardInfoAdd()
        {
            InitializeComponent();

            this.susToolBar1.ShowRefreshButton = false;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = false;
            this.susToolBar1.ShowCancelButton = false;
        }

        private CardInfoModel _CardInfoModel;


        public CardInfoAdd(XtraUserControl1 ucMain, string moduleId, CardInfoModel CardInfoModel) : this()
        {
            //processFlowMain1.ProcessFlowSusToolBar = susToolBar1;
            this.ucMain = ucMain;
            //processFlowMain1.ucMain = ucMain;

            if (CardInfoModel == null)
            {
                _CardInfoModel = new CardInfoModel();
            }
            else
            {
                _CardInfoModel = CardInfoModel;
            }

            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            throw new NotImplementedException();
        }

        private void CardInfoAdd_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            this.chcBoxEmployee.Properties.DataSource = 
            this.chcBoxEmployee.Properties.DisplayMember = "";
            
            //cmbCompany.DataSource = base.GetCompanyList();
            //cmbCompany.DisplayMember = "Name";
            //cmbCompany.ValueMember = "Id";

            //if (_CardInfoModel != null)
            //{
            //    this.chcBoxEmployee.EditValue =
            //}
        }
    }
}
