
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
//using DevExpress.XtraReports.Design;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using log4net;
using SuspeSys.Client.Modules;
using SuspeSys.Client.Modules.CuttingBed;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Domain;
using SuspeSys.Service;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
//using static System.Windows.Forms.Control;
using LanguageCommon = SuspeSys.Domain.Common;

namespace SuspeSys.Client.Action
{
    public class LanguageAction : BaseAction
    {
        public static readonly LanguageAction Instance = new LanguageAction();
        private static readonly ILanguageService languageService = new LanguageServiceImpl();
        //private static readonly ILog log = LogManager.GetLogger(typeof(LanguageAction));
        private LanguageAction() { }
        public void InitLanguage(string connectionString)
        {
            log.Info("语言初始化开始...");
            LanguageCommon.SusLanguageCons.Language = languageService.InitLanguage(connectionString);
            LanguageCommon.SusLanguageCons.CurrentLanguage = LanguageCommon.SusLanguageCons.SimplifiedChinese;
            LanguageCommon.SusLanguageCons.LastLanguage = null;//LanguageCommon.SusLanguageCons.SimplifiedChinese;
            log.Info("语言初始化完成.");
        }
        public string BindLanguageTxt(string lKey, string lTxt = null)
        {
            if (LanguageCommon.SusLanguageCons.Language.Count == 0)
                return lTxt;
            var dicc = LanguageCommon.SusLanguageCons.Language[LanguageCommon.SusLanguageCons.CurrentLanguage];
            if (null != LanguageCommon.SusLanguageCons.LastLanguage && LanguageCommon.SusLanguageCons.LastLanguage.Equals(LanguageCommon.SusLanguageCons.CurrentLanguage))
                return lTxt;
            if (!dicc.ContainsKey(lKey))
            {
                return "";
            }

            return dicc[lKey];
        }
        public void ChangeLanguage(Object oj)
        {
            if (string.IsNullOrEmpty(LanguageCommon.SusLanguageCons.CurrentLanguage))
            {
                log.Info("无语言设置...");
                return;
            }

            ControlCollection ccs = null;
            if (oj is ControlCollection)
            {
                ccs = (ControlCollection)oj;
            }
            if (ccs == null && oj is AccordionControlElementCollection)
            {
                SetAccordionControlElementCollection(oj);
            }
            if (ccs == null && oj is DevExpress.XtraTab.XtraTabPageCollection)
            {
                SetXtraTabPageCollection(oj);
            }
            if (ccs == null && oj is System.Windows.Forms.TabControl.TabPageCollection)
            {
                SetTabPageCollection(oj);

            }
            if (ccs == null)
                return;
            foreach (var ct in ccs)
            {
                if (ct is DevExpress.XtraPivotGrid.PivotGridControl)
                {
                    var pgc = (DevExpress.XtraPivotGrid.PivotGridControl)ct;
                    foreach (DevExpress.XtraPivotGrid.PivotGridField item in pgc.Fields)
                    {
                        var txt = item.Name;
                        var lTxt = BindLanguageTxt(txt);
                        if (!string.IsNullOrEmpty(lTxt))
                            item.Caption = lTxt;
                    }
                }
                if (ct is DevExpress.XtraEditors.RadioGroup)
                {
                    foreach (var item in ((DevExpress.XtraEditors.RadioGroup)ct).Properties.Items)
                    {
                        if (item is DevExpress.XtraEditors.Controls.RadioGroupItem)
                        {
                            var itemmm = (DevExpress.XtraEditors.Controls.RadioGroupItem)item;
                            if (itemmm.Tag is TagExt)
                            {
                                var txt = (SuspeSys.Client.Modules.Ext.TagExt)itemmm.Tag;
                                var lTxt = BindLanguageTxt(txt.Name);
                                if (!string.IsNullOrEmpty(lTxt))
                                    itemmm.Description = lTxt;
                            }
                        }
                    }
                }
                if (ct is SusQueryControl)
                {
                    ChangeLanguage(((SusQueryControl)ct).Controls);
                }
                if (ct is GridControl)
                {
                    SetGridControlLanguage(ct);
                }
                if (ct is System.Windows.Forms.Panel)
                {
                    ChangeLanguage(((System.Windows.Forms.Panel)ct).Controls);
                }
                if (ct is SuspeSys.Client.Modules.SusToolBar)
                {
                    ChangeLanguage(((SuspeSys.Client.Modules.SusToolBar)ct).Controls);
                }
                if (ct is SuspeSys.Client.Modules.SusGrid)
                {
                    ChangeLanguage(((SuspeSys.Client.Modules.SusGrid)ct).Controls);
                }
                if (ct is SuspeSys.Client.Modules.SusPage)
                {
                    ChangeLanguage(((SuspeSys.Client.Modules.SusPage)ct).Controls);
                }
                if (ct is System.Windows.Forms.FlowLayoutPanel)
                {
                    ChangeLanguage(((System.Windows.Forms.FlowLayoutPanel)ct).Controls);
                }
                if (ct is ProcessFlowMain)
                {
                    ChangeLanguage(((ProcessFlowMain)ct).Controls);
                }
                if (ct is ProcessFlowChartMain)
                {
                    ChangeLanguage(((ProcessFlowChartMain)ct).Controls);
                }
                if (ct is SusTreeList)
                {
                    SetSusTreeListLanguage(ct);
                }
                if (ct is TreeList)
                {
                    SetTreeListLanguage(ct);
                }
                if (ct is BarButtonItem)
                {
                    BarButtonItem lb11 = (BarButtonItem)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Caption = lTxt;
                }
                if (ct is CheckEdit)
                {
                    CheckEdit lb11 = (CheckEdit)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is System.Windows.Forms.Label)
                {
                    System.Windows.Forms.Label lb11 = (System.Windows.Forms.Label)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is LabelControl)
                {
                    LabelControl lb11 = (LabelControl)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is System.Windows.Forms.Label)
                {
                    System.Windows.Forms.Label lb11 = (System.Windows.Forms.Label)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is XtraTabPage)
                {
                    ChangeLanguage(((XtraTabPage)ct).Controls);
                }
                SetRibbonStatusBar(ct);
                if (ct is System.Windows.Forms.Label)
                {
                    System.Windows.Forms.Label lb11 = (System.Windows.Forms.Label)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is LabelControl)
                {
                    LabelControl lb11 = (LabelControl)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is DevExpress.XtraBars.Docking.DockPanel)
                {
                    DevExpress.XtraBars.Docking.DockPanel lb11 = (DevExpress.XtraBars.Docking.DockPanel)ct;
                    var lTxt = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        lb11.Text = lTxt;
                }
                if (ct is RibbonControl)
                {
                    SetRibbonControlLanguage(ct);
                    if (!string.IsNullOrEmpty(((RibbonControl)ct).ApplicationCaption))
                    {
                        ((RibbonControl)ct).ApplicationCaption = BindLanguageTxt(((RibbonControl)ct).Name);
                    }
                }
                if (ct is SkinRibbonGalleryBarItem)
                {

                }
                if (ct is SimpleButton)
                {
                    SimpleButton cBtn = (SimpleButton)ct;
                    var lTxt = BindLanguageTxt(cBtn.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        cBtn.Text = lTxt;
                }
                SetRibbonControl(ct);
                // Control cols = (Control)ct;
                if (ct is GroupControl)
                {
                    if (((GroupControl)ct).Controls.Count > 0)
                    {
                        ChangeLanguage(((GroupControl)ct).Controls);
                    }
                    GroupControl cBtn = (GroupControl)ct;
                    var lTxt = BindLanguageTxt(cBtn.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        cBtn.Text = lTxt;
                }
                if (ct is DevExpress.XtraBars.Docking.DockPanel)
                {
                    if (((DevExpress.XtraBars.Docking.DockPanel)ct).Controls.Count > 0)
                    {
                        ChangeLanguage(((DevExpress.XtraBars.Docking.DockPanel)ct).Controls);
                    }
                }

                if (ct is DevExpress.XtraBars.Docking.ControlContainer)
                {
                    if (((DevExpress.XtraBars.Docking.ControlContainer)ct).Controls.Count > 0)
                    {
                        ChangeLanguage(((DevExpress.XtraBars.Docking.ControlContainer)ct).Controls);
                    }
                }

                if (ct is DevExpress.XtraEditors.PanelControl)
                {
                    if (((DevExpress.XtraEditors.PanelControl)ct).Controls.Count > 0)
                    {
                        ChangeLanguage(((DevExpress.XtraEditors.PanelControl)ct).Controls);
                    }
                }
                if (ct is DevExpress.XtraBars.Docking.DockManager)
                {
                    if (((DevExpress.XtraBars.Docking.DockManager)ct).RootPanels.Count > 0)
                    {
                        ChangeLanguage(((DevExpress.XtraBars.Docking.DockManager)ct).RootPanels);
                    }
                }
                if (ct is SusXtraUserControl)
                {
                    var sxuc = (SusXtraUserControl)ct;
                    ChangeLanguage(sxuc.Controls);
                }
                if (ct is XtraUserControl1)
                {
                    var sxuc = (XtraUserControl1)ct;
                    if (null != sxuc)
                    {
                        ChangeLanguage(sxuc.Controls);
                        if (sxuc.LeftAccordionControl != null)
                        {
                            ChangeLanguage(sxuc.LeftAccordionControl.Controls);
                            ChangeLanguage(sxuc.LeftAccordionControl.Elements);
                        }
                        if (sxuc.MainTabControl != null)
                        {
                            ChangeLanguage(sxuc.MainTabControl.Controls);
                            ChangeLanguage(sxuc.MainTabControl.TabPages);
                        }
                    }

                }
                if (ct is XtraTabControl)
                {
                    ChangeLanguage(((XtraTabControl)ct).Controls);
                }
                if (ct is DevExpress.XtraBars.Docking.DockPanel)
                {
                    var dp = (DevExpress.XtraBars.Docking.DockPanel)ct;
                    var lTxt = BindLanguageTxt(dp.Name);
                    if (!string.IsNullOrEmpty(lTxt))
                        dp.Text = lTxt;

                    ChangeLanguage(dp.Controls);
                }
                if (ct is ControlContainer)
                {
                    ChangeLanguage(((ControlContainer)ct).Controls);
                }
                if (ct is SuspeSys.Client.Modules.Ext.SusAccordionControl)
                {
                    ChangeLanguage(((SuspeSys.Client.Modules.Ext.SusAccordionControl)ct).Elements);
                }
                if (ct is SuspeSys.Client.Modules.Ext.SusAccordionControl)
                {
                    ChangeLanguage(((SuspeSys.Client.Modules.Ext.SusAccordionControl)ct).Controls);
                }
                if (ct is System.Windows.Forms.TabControl)
                {
                    ChangeLanguage(((System.Windows.Forms.TabControl)ct).TabPages);
                }
            }

        }

        private void SetSusTreeListLanguage(object ct)
        {
            if (ct is SusTreeList)
            {
                var sts = (SusTreeList)ct;
                foreach (TreeListColumn st in sts.Columns)
                {
                    var txt = BindLanguageTxt(st.Name);
                    if (!string.IsNullOrEmpty(txt))
                        st.Caption = txt;
                    if (st.ColumnEdit is RepositoryItemTextEdit)
                    {
                        var riEdt = (RepositoryItemTextEdit)st.ColumnEdit;
                        var reName = riEdt.Name;
                        txt = BindLanguageTxt(reName);
                        if (!string.IsNullOrEmpty(txt))
                            st.Caption = txt;
                    }
                }
            }
        }
        private void SetTreeListLanguage(object ct)
        {
            if (ct is TreeList)
            {
                var sts = (TreeList)ct;
                foreach (TreeListColumn st in sts.Columns)
                {
                    var txt = BindLanguageTxt(st.Name);
                    if (!string.IsNullOrEmpty(txt))
                        st.Caption = txt;
                    if (st.ColumnEdit is RepositoryItemTextEdit)
                    {
                        var riEdt = (RepositoryItemTextEdit)st.ColumnEdit;
                        var reName = riEdt.Name;
                        txt = BindLanguageTxt(reName);
                        if (!string.IsNullOrEmpty(txt))
                            st.Caption = txt;
                    }
                }
            }
        }

        private void SetTabPageCollection(object oj)
        {
            var xtpc = (System.Windows.Forms.TabControl.TabPageCollection)oj;
            foreach (var jj in xtpc)
            {
                if (jj is System.Windows.Forms.TabPage)
                {
                    ChangeLanguage(((System.Windows.Forms.TabPage)jj).Controls);

                }
                if (jj is System.Windows.Forms.TabPage)
                {
                    var jjTag = ((System.Windows.Forms.TabPage)jj).Tag;
                    if (jjTag is TagExt)
                    {
                        var txt = (SuspeSys.Client.Modules.Ext.TagExt)jjTag;
                        var lTxt = BindLanguageTxt(txt.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            ((System.Windows.Forms.TabPage)jj).Text = lTxt;
                    }
                    System.Windows.Forms.TabPage lb11 = (System.Windows.Forms.TabPage)jj;
                    var lTxt2 = BindLanguageTxt(lb11.Name);
                    if (!string.IsNullOrEmpty(lTxt2))
                        lb11.Text = lTxt2;
                }
            }
        }

        private void SetXtraTabPageCollection(object oj)
        {
            var xtpc = (DevExpress.XtraTab.XtraTabPageCollection)oj;
            foreach (var jj in xtpc)
            {
                if (jj is DevExpress.XtraTab.XtraTabPage)
                {
                    ChangeLanguage(((DevExpress.XtraTab.XtraTabPage)jj).Controls);

                }
                if (jj is SusXtraTabPage)
                {
                    var jjTag = ((SusXtraTabPage)jj).Tag;
                    if (jjTag is TagExt)
                    {
                        var txt = (SuspeSys.Client.Modules.Ext.TagExt)jjTag;
                        var lTxt = BindLanguageTxt(txt.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            ((SusXtraTabPage)jj).Text = lTxt;
                    }
                }
            }
        }

        private void SetAccordionControlElementCollection(object oj)
        {
            var ojjes = (AccordionControlElementCollection)oj;

            foreach (var ct in ojjes)
            {
                if (ct is AccordionControlElement)
                {
                    var ae = (AccordionControlElement)ct;
                    if (ae.Tag is SuspeSys.Client.Modules.Ext.TagExt)
                    {
                        var txt = (SuspeSys.Client.Modules.Ext.TagExt)ae.Tag;
                        var lTxt = BindLanguageTxt(txt.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            ae.Text = lTxt;
                    }
                    foreach (var els in ae.Elements)
                    {
                        var ess = (DevExpress.XtraBars.Navigation.AccordionControlElement)els;
                        if (ess.Tag is SuspeSys.Client.Modules.Ext.TagExt)
                        {
                            var txt = (SuspeSys.Client.Modules.Ext.TagExt)ess.Tag;
                            if (!string.IsNullOrEmpty(txt.Name))
                            {
                                var lTxt = BindLanguageTxt(txt.Name);
                                if (!string.IsNullOrEmpty(lTxt))
                                    ess.Text = lTxt;
                            }
                        }
                        if (null != ess)
                        {
                            SetAccordionControlElementCollection(ess.Elements);
                        }
                    }
                }
            }
        }

        private void SetRibbonControl(object ct)
        {
            if (ct is RibbonControl)
            {
                RibbonControl rcl = (RibbonControl)ct;
                foreach (var rct in rcl.Controls)
                {
                    if (rct is GridControl)
                    {
                        SetGridControlLanguage(rct);
                    }
                }
                foreach (var rct in rcl.Items)
                {
                    if (rct is BarSubItem)
                    {
                        // SetGridControlLanguage(rct);
                        BarSubItem lb11 = (BarSubItem)rct;
                        var lTxt = BindLanguageTxt(lb11.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            lb11.Caption = lTxt;
                    }
                    if (rct is BarEditItem)
                    {
                        //SetGridControlLanguage(rct);
                        BarEditItem lb11 = (BarEditItem)rct;
                        var lTxt = BindLanguageTxt(lb11.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            lb11.Caption = lTxt;
                    }
                }
            }
        }

        private void SetRibbonStatusBar(object ct)
        {
            if (ct is DevExpress.XtraBars.Ribbon.RibbonStatusBar)
            {
                var cs = (DevExpress.XtraBars.Ribbon.RibbonStatusBar)ct;
                foreach (var ctItem in cs.ItemLinks)
                {
                    if (ctItem is BarButtonItem)
                    {
                        BarButtonItem lb11 = (BarButtonItem)ctItem;
                        var lTxt = BindLanguageTxt(lb11.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            lb11.Caption = lTxt;
                    }
                    if (ctItem is DevExpress.XtraBars.BarStaticItem)
                    {
                        BarStaticItem lb11 = (BarStaticItem)ctItem;
                        var lTxt = BindLanguageTxt(lb11.Name);
                        if (!string.IsNullOrEmpty(lTxt))
                            lb11.Caption = lTxt;
                    }
                    if (ctItem is DevExpress.XtraBars.BarEditItemLink)
                    {
                        var rLink = ((DevExpress.XtraBars.BarEditItemLink)ctItem).Item;
                        if (rLink is BarEditItem)
                        {
                            var rLinkName = rLink.Name;
                            var cTxt = BindLanguageTxt(rLinkName);
                            if (!string.IsNullOrEmpty(cTxt))
                                rLink.Caption = cTxt;
                        }

                    }
                    if (ctItem is DevExpress.XtraBars.BarItemLink)
                    {
                        var iem = ((DevExpress.XtraBars.BarItemLink)ctItem).Item;
                        if (iem is DevExpress.XtraBars.BarButtonItem)
                        {
                            var rLink = iem;
                            var rLinkName = rLink.Name;
                            var cTxt = BindLanguageTxt(rLinkName);
                            if (!string.IsNullOrEmpty(cTxt))
                                rLink.Caption = cTxt;
                        }
                    }
                    if (ctItem is BarButtonItemLink)
                    {
                        var iem = ((DevExpress.XtraBars.BarButtonItemLink)ctItem).Item;
                        if (iem is DevExpress.XtraBars.BarButtonItem)
                        {
                            var rLink = iem;
                            var rLinkName = rLink.Name;
                            var cTxt = BindLanguageTxt(rLinkName);
                            if (!string.IsNullOrEmpty(cTxt))
                                rLink.Caption = cTxt;
                        }
                    }
                    if (ctItem is BarStaticItemLink)
                    {
                        var iem = ((DevExpress.XtraBars.BarStaticItemLink)ctItem).Item;
                        if (iem is DevExpress.XtraBars.BarStaticItem)
                        {
                            var rLink = iem;
                            var rLinkName = rLink.Name;
                            var cTxt = BindLanguageTxt(rLinkName);
                            if (!string.IsNullOrEmpty(cTxt))
                                rLink.Caption = cTxt;
                        }
                    }
                }
            }
        }

        private void SetRibbonControlLanguage(object ct)
        {
            RibbonControl rcMenu = (RibbonControl)ct;
            foreach (RibbonPage rpage in rcMenu.Pages)
            {
                var rName = rpage.Name;
                var rtxt = BindLanguageTxt(rName);
                if (!string.IsNullOrEmpty(rtxt))
                    rpage.Text = rtxt;
               
                foreach (RibbonPageGroup gc in rpage.Groups)
                {
                    var gcName = gc.Name;
                    var lTxt = BindLanguageTxt(gcName);
                    //if (!string.IsNullOrEmpty(lTxt))
                    //    rpage.Text = lTxt;
                    if (!string.IsNullOrEmpty(lTxt))
                        gc.Text = lTxt;
                    foreach (var rItemLink in gc.ItemLinks)
                    {
                        if (rItemLink is DevExpress.XtraBars.BarItemLink)
                        {
                            var iem = ((DevExpress.XtraBars.BarItemLink)rItemLink).Item;
                            if (iem is DevExpress.XtraBars.BarButtonItem)
                            {
                                var rLink = iem;
                                var rLinkName = rLink.Name;
                                var cTxt = BindLanguageTxt(rName);
                                if (!string.IsNullOrEmpty(cTxt))
                                    rLink.Caption = cTxt;
                            }
                        }
                        if (rItemLink is DevExpress.XtraBars.BarButtonItem)
                        {
                            //rLink
                            //RibbonPageGroupItemLink
                            var rLink = (DevExpress.XtraBars.BarButtonItem)rItemLink;
                            var rLinkName = rLink.Name;
                            var cTxt = BindLanguageTxt(rName);
                            if (!string.IsNullOrEmpty(cTxt))
                                rLink.Caption = cTxt;
                        }
                       
                    }
                }
            }
            //RibbonControl rcMenu = (RibbonControl)ct;
            foreach (BarItem barItem in rcMenu.Items)
            {
                if (barItem is BarButtonItem)
                {
                    var bName = barItem.Name;
                    var cTxt = BindLanguageTxt(bName);
                    if (!string.IsNullOrEmpty(cTxt))
                        barItem.Caption = cTxt;
                }
            }
        }

        private void SetGridControlLanguage(object ct)
        {
            GridControl gcc = (GridControl)ct;
            if (gcc.ContextMenuStrip != null)
            {
                var cms = gcc.ContextMenuStrip;
                foreach (ToolStripMenuItem itemm in cms.Items)
                {

                    var reName = itemm.Name;
                    var txt = BindLanguageTxt(reName);
                    if (!string.IsNullOrEmpty(txt))
                        itemm.Text = txt;
                }
            }
            foreach (var gv in (gcc.ViewCollection))
            {
                if (gv is DevExpress.XtraGrid.Views.Grid.GridView)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gvv = (DevExpress.XtraGrid.Views.Grid.GridView)gv;
                    foreach (var gColumn in gvv.Columns)
                    {
                        if (gColumn is GridColumn)
                        {
                            GridColumn gnResult = (GridColumn)gColumn;
                            var txt = BindLanguageTxt(gnResult.Name);
                            if (!string.IsNullOrEmpty(txt))
                                gnResult.Caption = txt;
                            if (gnResult.ColumnEdit is RepositoryItemTextEdit)
                            {
                                var riEdt = (RepositoryItemTextEdit)gnResult.ColumnEdit;
                                var reName = riEdt.Name;
                                txt = BindLanguageTxt(reName);
                                if (!string.IsNullOrEmpty(txt))
                                    gnResult.Caption = txt;
                            }
                            if (gnResult.ColumnEdit is RepositoryItemCheckEdit)
                            {
                                var riEdt = (RepositoryItemCheckEdit)gnResult.ColumnEdit;
                                var reName = riEdt.Name;
                                txt = BindLanguageTxt(reName);
                                if (!string.IsNullOrEmpty(txt))
                                    gnResult.Caption = txt;
                            }
                        }
                    }
                }
                if (gv is SusGridView)
                {
                    SusGridView gvv = (SusGridView)gv;
                    foreach (var gColumn in gvv.Columns)
                    {
                        if (gColumn is GridColumn)
                        {
                            GridColumn gnResult = (GridColumn)gColumn;
                            var txt = BindLanguageTxt(gnResult.Name);
                            if (!string.IsNullOrEmpty(txt))
                                gnResult.Caption = txt;
                            if (gnResult.ColumnEdit is RepositoryItemTextEdit)
                            {
                                var riEdt = (RepositoryItemTextEdit)gnResult.ColumnEdit;
                                var reName = riEdt.Name;
                                txt = BindLanguageTxt(reName);
                                if (!string.IsNullOrEmpty(txt))
                                    gnResult.Caption = txt;
                            }
                        }
                    }
                }
            }
        }
    }
}
