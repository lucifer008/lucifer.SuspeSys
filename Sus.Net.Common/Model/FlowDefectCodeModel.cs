using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Model
{
    public class FlowDefectCodeModel
    {
        public FlowDefectCodeModel(int mainTrackNumber, int statingNo)
        {
            this.MainTrackNumber = mainTrackNumber;
            this.StatingNo = statingNo;
           // this.HexAddress = hexAddress;
        }
        public int StatingNo { set; get; }
        public int MainTrackNumber { set; get; }
        public override bool Equals(object obj)
        {
            var fdc = obj as FlowDefectCodeModel;
            if (null == fdc) return false;
            if (fdc.MainTrackNumber.Equals(MainTrackNumber) && fdc.StatingNo.Equals(StatingNo))
            {
                return true;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public override string ToString()
        {
            var v = string.Format("{0}{1}{1}",MainTrackNumber,StatingNo);
            return v;
        }
      //  public string HexAddress { set; get; }

    }
    public class FlowDefectCodeItem {
        public FlowDefectCodeItem(string hexAddress) {
            this.HexAddress = hexAddress;
        }
        public override bool Equals(object obj)
        {
            var o = obj as FlowDefectCodeItem;

            if (null==o) {
                return false;
            }
            return o.HexAddress.Equals(HexAddress);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string HexAddress { set; get; }
        public List<byte> Data = new List<byte>();
    }
}
