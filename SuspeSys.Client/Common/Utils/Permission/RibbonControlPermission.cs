using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Common.Utils.Permission
{
    /// <summary>
    /// BtnBar权限处理
    /// </summary>
    public class RibbonControlPermission
    {

        public static void Process(RibbonControl root)
        {
            foreach (RibbonPage page in root.Pages)
            {
                int visiblePageCount = 0;
                foreach (RibbonPageGroup pageGroup in page.Groups)
                {
                    int visibleCount = 0;
                    foreach (var item in pageGroup.ItemLinks)
                    {
                        if (item is BarButtonItemLink)
                        {
                            var btn = (BarButtonItemLink)item;
                            if (!Domain.Common.CurrentUser.Instance.HasPermisson(btn.Item.Name))
                                btn.Visible = false;

                            if (btn.Visible)
                                visibleCount++;
                        }
                        if (item is BarSubItemLink)
                        {
                            var btn = (BarSubItemLink)item;
                            if (!Domain.Common.CurrentUser.Instance.HasPermisson(btn.Item.Name))
                                btn.Visible = false;

                            if (btn.Visible)
                                visibleCount++;
                        }
                        
                    }

                    pageGroup.Visible = (0 != visibleCount);

                    if (pageGroup.Visible)
                        visiblePageCount++;
                }

                //Pgae  吊挂管理
                page.Visible = (0 != visiblePageCount);
            }

        }
    }
}
