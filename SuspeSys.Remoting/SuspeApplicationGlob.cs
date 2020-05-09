
using SuspeSys.CustomerControls.Sqlite.Entity;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Remoting
{
    public class SuspeApplicationGlob
    {
        private SuspeApplicationGlob() { }
        public readonly static SuspeApplicationGlob Instance = new SuspeApplicationGlob();
        public void ResetDBConfig(DatabaseConnection defaultDB)
        {
            SuspeApplication.ResetConfig(defaultDB);
        }
    }
}
