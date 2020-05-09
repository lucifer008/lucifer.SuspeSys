using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    using SuspeSys.Domain.Base;
    using System.ComponentModel;
    
    [Serializable]
    public partial class City : MetaData {
        public City() { }
        public virtual string Id { get; set; }
        public virtual Province Province { get; set; }
        public virtual string CityCode { get; set; }
        public virtual string CityName { get; set; }
    }
}
