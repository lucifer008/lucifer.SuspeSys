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
using DevExpress.DevAV.Modules;
using DevExpress.XtraBars.Ribbon;

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 吊挂管理
    /// </summary>
    public partial class Suspe : BaseModuleControl, IRibbonModule
    {
        public Suspe():base(null)
        {
            InitializeComponent();
        }

        RibbonControl IRibbonModule.Ribbon
        {
            get
            {
                return ribbonControl1;
            }
        }
    }
}
