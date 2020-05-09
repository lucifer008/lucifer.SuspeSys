using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Cus
{
    /// <summary>
    /// 站内衣架集合
    /// </summary>
    [Serializable]
    public class StatingInHangerModel : HangerProductFlowChart
    {
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            var o = obj as StatingInHangerModel;
            var isEq = o.HangerNo.Equals(this.HangerNo) && o.MainTrackNumber.Value==this.MainTrackNumber.Value && o.StatingNo.Value==this.StatingNo.Value;
            return isEq;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
