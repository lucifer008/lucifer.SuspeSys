using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.SusTcp
{
    public interface IReloadCacheService
    {
        /// <summary>
        /// 重新加载系统参数缓存
        /// </summary>
        void ReloadSystemParameter();
        void ReloadBridgeSet();
        
    }
}
