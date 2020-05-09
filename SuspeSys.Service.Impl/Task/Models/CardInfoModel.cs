using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Task.Models
{
    public class CardInfoModel: CardLoginInfo
    {
        public virtual string CardInfoId { get; set; }
    }
}
