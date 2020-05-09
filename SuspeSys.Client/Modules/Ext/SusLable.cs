using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext
{
    public class SusLable : LabelControl
    {
        public SusLable()
        {
            //InitializeComponent();

            Font font = new Font("宋体", 9);
            this.Font = font;

            this.AutoSizeMode = LabelAutoSizeMode.Default;

        }
    }
}
