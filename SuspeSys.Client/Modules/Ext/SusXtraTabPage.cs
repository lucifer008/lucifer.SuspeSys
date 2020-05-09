using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext
{
    /// <summary>
    /// 扩展 XtraTabPage
    /// </summary>
    public class SusXtraTabPage: XtraTabPage
    {
        public SusXtraTabPage() {
           // this.
        }
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            var o = obj as XtraTabPage;
            var isEq = o.Name.Equals(this.Name);
            return isEq;
            //if (isEq) {
            //    this.Refresh();
            //}
            //return o.Name.Equals(this.Name);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public virtual string NameExt { set; get; }
    }
}
