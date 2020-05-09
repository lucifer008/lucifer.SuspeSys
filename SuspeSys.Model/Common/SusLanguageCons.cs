using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Common
{
    public class SusLanguageCons
    {
        public  static Dictionary<string, Dictionary<string, string>> Language = new Dictionary<string, Dictionary<string, string>>();
        public static string CurrentLanguage { get; set; }
        public static string CurrentLanguageTxt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string LastLanguage { get; set; }
        /// <summary>
        /// 简体
        /// </summary>
        public readonly static string SimplifiedChinese = "中文(简体)";
        /// <summary>
        /// 繁体
        /// </summary>
        public readonly static string TraditionalChinese = "中文(繁体)";
        /// <summary>
        /// 英语
        /// </summary>
        public readonly static string English = "English";
        /// <summary>
        /// 柬埔寨
        /// </summary>
        public readonly static string Cambodia = "Cambodia";
        /// <summary>
        /// 越南
        /// </summary>
        public readonly static string Vietnamese = "Vietnamese";

    }
}
