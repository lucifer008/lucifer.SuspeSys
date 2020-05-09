using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;

namespace SuspeSys.Service.LED
{
    public interface ILEDService
    {
        void SaveLedScreenInfo(IList<LedScreenConfig> ledScreenConfigList);
        void SaveLedHoursPlanTableItem(List<LedHoursPlanTableItem> ledHoursPlanTableItemList);
        void DeleteLEDConfig(string id);
        void DeleteLedScreenPage(string id);
        void DeleteLedLedHoursPlanTableIteme(string id);
    }
}
