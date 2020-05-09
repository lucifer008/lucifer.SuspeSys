using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.WpfClient
{
    public class Member
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public SexOpt Sex { get; set; }
        public bool Pass { get; set; }
        public Uri Email { get; set; }
    }
}
