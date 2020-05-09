
using System.ComponentModel;

namespace SuspeSys.Domain.SusEnum
{
    public enum ModulesType : int
    {
        [Description("菜单")]
        Menu = 1,

        [Description("页面")]
        Page = 2,

        [Description("按钮")]
        Button = 3
    }
}
