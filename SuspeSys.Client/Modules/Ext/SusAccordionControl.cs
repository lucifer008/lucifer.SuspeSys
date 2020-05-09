using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext
{
    /// <summary>
    /// 扩展AccordionControl
    /// </summary>
    public class SusAccordionControl: AccordionControl
    {
        public SusAccordionControl() {}
        public IFilterContent GetSearchFilterControl() {
            return GetFilterControl();
        }

        /// <summary>
        /// 重写Refresh，用于权限判断
        /// </summary>
        public override void Refresh()
        {
            Common.Utils.Permission.MenuPermission.Process(this.Elements);

            base.Refresh(); 
        }
    }
}
