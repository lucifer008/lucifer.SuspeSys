using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Common
{
    /// <summary>
    /// 消息上下文
    /// </summary>
    public class SusNetContext
    {
        static SusNetContext()
        {
            //Init
        }

        /// <summary>
        /// 【协议2.0】初始化制品推送地址与排产号的关系
        /// </summary>
        public static void InitProductsCMD() {
            ProductNumberAddresList.Clear();
            ProductNumberHeadAddresList.Clear();

            var begin = 24576;//60 00
            var end = 26623;//67 FF
            //var dicProductNumber = new Dictionary<int, List<string>>();
            var i = 1;
            var pNumber = 0;
            var addressList = new List<string>();
            for (var index = begin; index <= end; index++)
            {
                var hexStr = HexHelper.TenToHexString2Len(index);
                Console.WriteLine(index + "--->" + hexStr);
                //Console.WriteLine("");
                addressList.Add(hexStr);
                if (i % 8 == 0)
                {
                    string[] aa = new string[8];
                    addressList.CopyTo(aa);
                    ProductNumberAddresList.Add(pNumber, new List<string>(aa));
                    ProductNumberHeadAddresList.Add(pNumber, new List<string>(aa)[0]);
                    addressList = new List<string>();
                    pNumber++;
                }

                i++;
            }
        }
        /// <summary>
        /// 【协议2.0】排产号与地址的约定关系
        /// </summary>
        public static Dictionary<int,List<string>> ProductNumberAddresList = new Dictionary<int, List<string>>();
        /// <summary>
        /// 【协议2.0】排产号约定地址的首位地址
        /// </summary>
        public static Dictionary<int, string> ProductNumberHeadAddresList = new Dictionary<int, string>();
    }
}
