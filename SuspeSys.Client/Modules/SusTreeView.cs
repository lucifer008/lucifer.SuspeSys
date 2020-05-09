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
using DevExpress.XtraTreeList;

namespace SuspeSys.Client.Modules
{
    public partial class SusTreeView : DevExpress.XtraEditors.XtraUserControl
    {
        public SusTreeView()
        {
            InitializeComponent();
        }
        public TreeList TLData { get { return this.tvData; } }
    }
}
