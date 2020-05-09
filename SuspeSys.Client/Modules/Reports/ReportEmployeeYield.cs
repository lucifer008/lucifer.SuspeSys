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

namespace SuspeSys.Client.Modules.Reports
{
    public partial class ReportEmployeeYield : DevExpress.XtraReports.UI.XtraReport { 
        public ReportEmployeeYield()
        {
            InitializeComponent();
        }
        public BindingSource ReportBindSource {
            get { return this.bindingSource1; }
        }
    }
}
