using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class StatingType
    {

        /// <summary>
        /// 车缝站编码
        /// </summary>
        public static string StatingSeamingCode = "100";
        /// <summary>
        /// 多功能站编码
        /// </summary>
        public static string StatingMultiFunctionCode = "101";

        static StatingType()
        {
            if (StatingTypeList == null)
                StatingTypeList = new List<StatingType>();

            StatingTypeList.Add(StatingHanger);
            StatingTypeList.Add(StatingSeaming);
            StatingTypeList.Add(StatingMultiFunction);
            StatingTypeList.Add(StatingRework);
            StatingTypeList.Add(StatingStorage);
            StatingTypeList.Add(StatingOverload);
            StatingTypeList.Add(StatingQC);
            StatingTypeList.Add(StatingBridging);
        }

        public static int GetStatingType(string description)
        {
            var statingType = StatingTypeList.FirstOrDefault(o => o.Desption == description.Trim());
            if (statingType != null)
                return statingType.Value;
            else
                throw new Exception("未找到匹配的站点类型");

        }
        public static string GetStatingCode(string description)
        {
            var statingType = StatingTypeList.FirstOrDefault(o => o.Desption == description.Trim());
            if (statingType != null)
                return statingType.Code?.Trim();
            else
                throw new Exception("未找到匹配的站点类型");

        }
        public static List<StatingType> StatingTypeList
        {
            get;
            private set;
        }

        /// <summary>
        /// 挂片站
        /// </summary>
        public static readonly StatingType StatingHanger = new StatingType(0x01, "挂片站", "104", 0xFFff3300, 0xFFEDEDED);

        /// <summary>
        /// 车缝站
        /// </summary>
        public static readonly StatingType StatingSeaming = new StatingType(0x02, "车缝站", "100", 0xFF3399cc, 0xFFEDEDED);

        /// <summary>
        /// 多功能站
        /// </summary>
        public static readonly StatingType StatingMultiFunction = new StatingType(0x03, "多功能站", "101", 0xFFFF7F24, 0xFFEDEDED);

        /// <summary>
        /// 返工专用
        /// </summary>
        public static readonly StatingType StatingRework = new StatingType(0x04, "专用返工站", "103", 0xFFCD3278, 0xFFEDEDED);

        /// <summary>
        /// 储存站
        /// </summary>
        public static readonly StatingType StatingStorage = new StatingType(0x05, "储存站", "102", 0xFF5D478B, 0xFFEDEDED);

        /// <summary>
        /// 超载站
        /// </summary>
        public static readonly StatingType StatingOverload = new StatingType(0x06, "超载站", "105", 0xFF3B3B3B, 0xFFEDEDED);

        /// <summary>
        /// QC站
        /// </summary>
        public static readonly StatingType StatingQC = new StatingType(0x07, "QC站", "106", 0xFF8B8B00, 0xFFEDEDED);

        /// <summary>
        /// 桥接专用
        /// </summary>
        public static readonly StatingType StatingBridging = new StatingType(0x08, "桥接专用", "107", 0xFF90D020, 0xFFEDEDED);

        private StatingType(byte _value, string desption,string code, uint beginARGB, uint endARGB)
        {
            Value = _value;
            Desption = desption;
            this.Code = code;
            this.BeginARGB = beginARGB;
            this.EndARGB = endARGB;
            
        }
        public uint BeginARGB { get; set; }

        public uint EndARGB { get; set; }
        /// <summary>
        /// 数据库对应编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 硬件对应编码
        /// </summary>
        public byte Value { set; get; }
        public string Desption { set; get; }
        
    }
}
