
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Client.Modules.Permission
{
    public partial class ModuleAdd :  Ext.SusXtraUserControl
    {

        #region 页面属性
        //protected SuspeSys.Domain.Modules
        #endregion

        public ModuleAdd()
        {
            InitializeComponent();

            this.InitFormData();
        }

        /// <summary>
        /// 处理连续为不同的版本生成制单工序
        /// </summary>
        /// <param name="ucMain"></param>
        public ModuleAdd(XtraUserControl1 ucMain, string moduleId):this()
        {
            //processFlowMain1.ProcessFlowSusToolBar = susToolBar1;
            this.ucMain = ucMain;
            //processFlowMain1.ucMain = ucMain;
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitFormData()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = EnumHelper.GetDictionary(typeof(ModulesType));

            this.cmbResourceType.DataSource = bs;
            this.cmbResourceType.ValueMember = "key";
            this.cmbResourceType.DisplayMember = "Value";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void ModuleAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
