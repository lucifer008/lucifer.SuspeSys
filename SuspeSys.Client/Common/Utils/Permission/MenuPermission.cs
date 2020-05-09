using DevExpress.XtraBars.Navigation;
using SuspeSys.Client.Modules.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Common.Utils.Permission
{
    public class MenuPermission
    {
        public static void Process(AccordionControlElementCollection element)
        {
            if (element == null)
                return;

            foreach (AccordionControlElement item in element)
            {
                if (item.Tag == null && string.IsNullOrEmpty(item.Text)) 
                    continue;


                int hiddenCount = 0;
                foreach (AccordionControlElement second in item.Elements)
                {
                    if (second.Tag == null && string.IsNullOrEmpty(second.Text))
                        continue;

                    if (second.Tag is TagExt)
                    {
                        var tag = second.Tag as TagExt;

                        if (!Domain.Common.CurrentUser.Instance.HasPermisson(tag.Id))
                        {
                            second.Visible = false;
                            hiddenCount++;
                        }
                            
                    }
                }

                if (hiddenCount == item.Elements.Count)
                    item.Visible = false;
                
            }
        }
    }
}
