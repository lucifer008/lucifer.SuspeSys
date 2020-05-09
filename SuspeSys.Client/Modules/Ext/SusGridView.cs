using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext
{
    public class SusGridView : GridView
    {
        public SusGridView()
        {
            this.IndicatorWidth = 40;
            this.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            this.Appearance.EvenRow.BackColor = Color.FromArgb(20, 183, 186);
            this.Appearance.OddRow.BackColor = Color.FromArgb(150, 199, 237, 204);
            this.OptionsView.EnableAppearanceEvenRow = true;
            this.OptionsView.EnableAppearanceOddRow = true;
        }
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
