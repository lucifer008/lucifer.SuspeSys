using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    [Serializable]
    public class ClientMachinesModel : ClientMachines {

        public ClientMachinesModel()
        {
            this.Pipelining = new List<PipeliningModel>();
        }

        public bool Checked { get; set; }

        public IList<PipeliningModel> Pipelining { get; set; }

    }
}
