using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public partial class LedScreenConfig
    {
        public string LSCId { set; get; }
        public string GroupNoMainTrackNumber { set; get; }
        public IList<LedScreenPage> LedScreenPageList = new List<LedScreenPage>();
    }
}
