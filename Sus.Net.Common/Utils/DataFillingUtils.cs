using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Utils
{
    public class DataFillingUtils
    {
        /// <summary>
        /// 填补数据位，不足为补0,补足5字节
        /// </summary>
        /// <param name="data">待填补数据</param>
        /// <param name="tag">0：在前填补;1:在后填补</param>
        /// <returns></returns>
        public static List<byte> Get5ByteDF(List<byte> data, int tag)
        {
            if (data.Count == 5) return data;
            var reslut = new List<Byte>();
            var temData = new List<byte>();
             for (var i = 0; i < 5 - data.Count; i++)
            {
                temData.Add(0);
            }
             
            if (tag == 0)
            {
                reslut.AddRange(temData.ToArray());
                reslut.AddRange(data.ToArray());
                return reslut;
            }
            reslut.AddRange(data.ToArray());
            reslut.AddRange(temData.ToArray());
            return reslut;
        }
    }
}
