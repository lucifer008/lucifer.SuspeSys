using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class SystemLogs
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string LogInfo { get; set; }

        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
