using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class CardType
    {
        /// <summary>
        /// 衣架卡
        /// </summary>
        public static readonly CardType Hanger = new CardType(0x01, "衣架卡");

        /// <summary>
        /// 衣车卡
        /// </summary>
        public static readonly CardType Clothes= new CardType(0x02, "衣车卡");

        /// <summary>
        /// 机修卡
        /// </summary>
        public static readonly CardType MachineRepair= new CardType(0x03, "机修卡");

        /// <summary>
        /// 员工卡
        /// </summary>
        public static readonly CardType Employeees = new CardType(0x04, "员工卡");
        private CardType(byte _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public byte Value { set; get; }
        public string Desption { set; get; }
        public static int GetValueByDesp(string desp) {
            if (Hanger.Desption.Equals(desp)) return Hanger.Value;
            if (Clothes.Desption.Equals(desp)) return Clothes.Value;
            if (MachineRepair.Desption.Equals(desp)) return MachineRepair.Value;
            if (Employeees.Desption.Equals(desp)) return Employeees.Value;
            throw new ApplicationException("卡类型不存在!");
        }
    }
}
