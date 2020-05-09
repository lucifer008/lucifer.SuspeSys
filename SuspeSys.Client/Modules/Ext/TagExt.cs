using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext
{
    public class TagExt
    {
        public string Id { get; set; }
        public TagExt(string Id)
        {
            this.Id = Id;
        }
        public string Name { get; set; }
    }
}
