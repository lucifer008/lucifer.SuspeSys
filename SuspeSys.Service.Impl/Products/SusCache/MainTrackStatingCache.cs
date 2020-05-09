using SuspeSys.Domain;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache
{
    public class MainTrackStatingCache
    {
        private MainTrackStatingCache() { }
        public static MainTrackStatingCache Instacne { get { return new MainTrackStatingCache(); } }
        public static Dictionary<int, List<MainTrackStatingCacheModel>> MainTrackStating = new Dictionary<int, List<MainTrackStatingCacheModel>>();

        public void InitMainTrackStating(int mainTrackNumber, IList<ProcessFlowStatingItem> pfStatingItemList)
        {
            foreach (var psi in pfStatingItemList)
            {
                if (!MainTrackStating.ContainsKey(mainTrackNumber))
                {
                    var list = new List<MainTrackStatingCacheModel>();
                    list.Add(new MainTrackStatingCacheModel()
                    {
                        MainTrackNumber = mainTrackNumber,
                        Capacity = (int)psi.Stating?.Capacity.Value,
                        StatingNo = int.Parse(psi.Stating?.StatingNo?.Trim())
                    });
                    MainTrackStating.Add(mainTrackNumber, list);
                }
                else
                {
                    var statingList = MainTrackStating[mainTrackNumber];
                    var isExs = false;
                    foreach (var s in statingList)
                    {
                        var sttNo = s.StatingNo;
                        var newStatingNo =int.Parse(psi.Stating?.StatingNo?.Trim());
                        if (newStatingNo == sttNo) {
                            isExs = true;
                            break;
                        }
                    }
                    if (!isExs) {
                        statingList.Add(new MainTrackStatingCacheModel()
                        {
                            MainTrackNumber = mainTrackNumber,
                            Capacity = (int)psi.Stating?.Capacity.Value,
                            StatingNo = int.Parse(psi.Stating?.StatingNo?.Trim())
                        });
                    }
                }
            }
        }
    }
}
