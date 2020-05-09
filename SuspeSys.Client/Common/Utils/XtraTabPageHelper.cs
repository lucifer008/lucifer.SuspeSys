using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Common.Utils
{
    public class XtraTabPageHelper
    {
        public static void AddTabPage(XtraTabControl xtc, XtraTabPage  xtPage,XtraUserControl ucFrom ) {
            try
            {
                var waitParameter = xtPage.Text;
                SusTransitionManager.StartTransition(xtc, waitParameter);
                if (xtc.TabPages.Contains(xtPage))
                {
                    //xtc.SelectedTabPage = xtPage;
                    //xtc.Refresh();
                  // xtc.SelectedTabPageIndex = xtc.TabPages.IndexOf(xtPage);
                    xtc.TabPages.RemoveAt(xtc.TabPages.IndexOf(xtPage));
                   // return;
                }
                //ucFrom.Parent = xtPage;
                ucFrom.Dock = DockStyle.Fill;
                xtPage.Controls.Add(ucFrom);
                xtc.SelectedTabPage = xtPage;
                xtc.TabPages.Add(xtPage);
            }
            finally {
                SusTransitionManager.EndTransition();
            }
        }
    }
}
