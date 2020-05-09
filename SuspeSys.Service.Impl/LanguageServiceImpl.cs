using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Base;
using SuspeSys.Dao;
using SuspeSys.Domain.Common;

namespace SuspeSys.Service.Impl
{
    public class LanguageServiceImpl : ILanguageService
    {
        public MulLanguage GetMulLanguage(string ResKey)
        {
            string sql = string.Format("select * from MulLanguage where ResKey=@ResKey");
            return DapperHelp.QueryForObject<MulLanguage>(sql, new { ResKey = ResKey });
        }
        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="ResKey"></param>
        /// <param name="tag">1:中文简体，2：中文繁体;3:英语;4:越南语;5:柬埔寨</param>
        /// <returns></returns>
        public string GetMulLanguageTxt(string ResKey, int tag)
        {
            var mLanguage = GetMulLanguage(ResKey);
            if (null == mLanguage) return "";
            switch (tag)
            {
                case 1:
                    return mLanguage.SimplifiedChinese;
                    
                case 2:
                    return mLanguage.TraditionalChinese;
                case 3:
                    return mLanguage.English;
                case 4:
                    return mLanguage.Vietnamese;
                case 5:
                    return mLanguage.Cambodia;
            }
            return null;
        }
        public Dictionary<string, Dictionary<string, string>> InitLanguage(string connString)
        {
            string sql = string.Format("select * from MulLanguage");
            var listLanguage = DapperHelp.QueryForList1<MulLanguage>(connString,sql);
            var dicResult = new Dictionary<string, Dictionary<string, string>>();
            var dicZh = new Dictionary<string, string>();
            var dicTra = new Dictionary<string, string>();
            var dicEn = new Dictionary<string, string>();
            var dicV = new Dictionary<string, string>();
            var dicCa = new Dictionary<string, string>();
            foreach (var lge in listLanguage)
            {
                dicZh.Add(lge.ResKey, lge.SimplifiedChinese);
                dicTra.Add(lge.ResKey, lge.TraditionalChinese);
                dicEn.Add(lge.ResKey, lge.English);
                dicV.Add(lge.ResKey, lge.Vietnamese);
                dicCa.Add(lge.ResKey, lge.Cambodia);
            }
            dicResult.Add(SusLanguageCons.SimplifiedChinese, dicZh);
            dicResult.Add(SusLanguageCons.TraditionalChinese, dicTra);
            dicResult.Add(SusLanguageCons.English, dicEn);
            dicResult.Add(SusLanguageCons.Vietnamese, dicV);
            dicResult.Add(SusLanguageCons.Cambodia, dicCa);
            return dicResult;
        }

        public void UploadLanguage(IList<MulLanguage> mulLanguageList)
        {
            foreach (var lge in mulLanguageList)
            {
                string sql = string.Format("select count(1) from MulLanguage where ResKey=@ResKey");
                var rCount = DapperHelp.QueryForObject<long>(sql, lge);
                if (rCount == 0)
                {
                    DapperHelp.Add<MulLanguage>(lge);
                }
                else
                {
                    DapperHelp.Edit<MulLanguage>(lge);
                }
            }
        }

        public bool IsExist(string resKey)
        {
            var obj = GetMulLanguage(resKey);
            if (null!=obj) {
                return true;
            }
            return false;
        }
    }
}
