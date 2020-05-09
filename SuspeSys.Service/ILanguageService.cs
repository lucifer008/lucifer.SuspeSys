using SuspeSys.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service
{
    public interface ILanguageService
    {
        Dictionary<string, Dictionary<string, string>> InitLanguage(string connString);
        void UploadLanguage(IList<MulLanguage> mulLanguageList);
        MulLanguage GetMulLanguage(string ResKey);
        bool IsExist(string resKey);
    }
}
