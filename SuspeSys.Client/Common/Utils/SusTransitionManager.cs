using DevExpress.Utils;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors;

using SuspeSys.Client.Modules.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Common.Utils
{
    public class SusTransitionManager
    {
        public SusTransitionManager() { }

        private static TransitionManager transitionManager = new TransitionManager();
        public static void StartTransition(Control ctrl, string caption)
        {
            var transition = new DevExpress.Utils.Animation.Transition();
            transition.ShowWaitingIndicator = DefaultBoolean.True;
            transition.WaitingIndicatorProperties.Caption = string.Empty;//DevExpress.XtraEditors.EnumDisplayTextHelper.GetDisplayText(caption);
            transition.WaitingIndicatorProperties.Description = "Loading...";
            transition.WaitingIndicatorProperties.ContentMinSize = new System.Drawing.Size(160, 0);

            var clockTransition = new DevExpress.Utils.Animation.ClockTransition();
            transition.Control = ctrl;
            transition.TransitionType = clockTransition;
            transitionManager.Transitions.Add(transition);


            transitionManager.StartTransition(ctrl);
        }
        public static void EndTransition()
        {
            transitionManager.EndTransition();
        }
    }
}
