using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.LED
{
    public class LEDAction : BaseAction
    {
        public static readonly LEDAction Instance = new LEDAction();
        private LEDAction() { }

        public void SaveLedScreenInfo(IList<LedScreenConfig> ledScreenConfigList, IList<LedScreenPage> ledScreenPageList)
        {
            //var sLedScreenConfigList = new List<LedScreenConfig>();
            foreach (var lsc in ledScreenConfigList)
            {
                foreach (var lp in ledScreenPageList)
                {
                    if (null != lsc.LSCId && lsc.LSCId.Equals(lp.ParentId))
                    {
                        lsc.LedScreenPageList.Add(lp);
                    }
                    
                }
            }

            ledService.SaveLedScreenInfo(ledScreenConfigList);
        }

        internal void SaveLedHoursPlanTableItem(List<LedHoursPlanTableItem> ledHoursPlanTableItemList)
        {
            ledService.SaveLedHoursPlanTableItem(ledHoursPlanTableItemList);
        }

        internal void DeleteLEDConfig(string id)
        {
            ledService.DeleteLEDConfig(id);
        }

        internal void DeleteLedScreenPage(string id)
        {
            ledService.DeleteLedScreenPage(id);
        }

        internal void DeleteLedLedHoursPlanTableIteme(string id)
        {
            ledService.DeleteLedLedHoursPlanTableIteme(id);
        }
    }
}
