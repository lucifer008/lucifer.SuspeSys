using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.Report;
using DevExpress.Spreadsheet;

namespace SuspeSys.Client.Modules.Reports
{
    /// <summary>
    /// 产量达标详情表
    /// </summary>
    public partial class GroupCompetitionReport : SusXtraUserControl
    {
        public GroupCompetitionReport()
        {
            InitializeComponent();

        }
        public GroupCompetitionReport(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
            susQueryControl1.Visible = false;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susToolBar1.ShowExportButton = false;
            susToolBar1.ShowPrintButton = false;
        }
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        //Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Query:
                        susQueryControl1.Visible = !susQueryControl1.Visible;
                        break;

                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                Common.Utils.SusTransitionManager.EndTransition();
            }
        }

        ReportQueryAction reportQueryAction = ReportQueryAction.Instance;
        void Query(QueryModel qm)
        {
            var searchKey = string.Empty;
            var model = reportQueryAction.SearchGroupCompetitionReport(searchKey);

            //日产量
            int[] yValues = model.OutProductDataList.Select(f => f.DailyYield).ToArray();// { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValues = model.OutProductDataList.Select(f => f.StatDate).ToArray();// { "France", "Canada", "Germany", "USA", "Italy" };
            chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].IsVisibleInLegend = true;
            chart1.Series["Series1"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("EveryYield");//"每日产量"; EveryYield

            //目标产量
            int[] yValuesTarget = model.OutProductDataList.Select(f => f.TargetYield).ToArray();// { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValuesTarget = model.OutProductDataList.Select(f => f.StatDate).ToArray();// { "France", "Canada", "Germany", "USA", "Italy" };
            chart1.Series["Series2"].Points.DataBindXY(xValuesTarget, yValuesTarget);
            chart1.Series["Series2"].IsValueShownAsLabel = true;
            chart1.Series["Series2"].IsVisibleInLegend = true;
            chart1.Series["Series2"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("TargetYield");//"目标产量";



            //不良数
            int[] yValuesDefect = model.OutProductDataList.Select(f => f.DefectCount).ToArray();// { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValuesDefect = model.OutProductDataList.Select(f => f.StatDate).ToArray();// { "France", "Canada", "Germany", "USA", "Italy" };
            chart1.Series["Series3"].Points.DataBindXY(xValuesDefect, yValuesDefect);
            chart1.Series["Series3"].IsValueShownAsLabel = true;
            chart1.Series["Series3"].IsVisibleInLegend = true;
            chart1.Series["Series3"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("DefectCount");//"不良数";

            //达成率=当日/计划
            double[] yValuesYieldRate = model.OutProductDataList.Select(f => f.TargetYield == 0 ? 0 : (double.Parse(f.DailyYield.ToString()) / f.TargetYield)).ToArray();// { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValuesYieldRate = model.OutProductDataList.Select(f => f.StatDate).ToArray();
            chart2.Series["Series1"].Points.DataBindXY(xValuesYieldRate, yValuesYieldRate);
            chart2.Series["Series1"].IsValueShownAsLabel = true;
            chart2.Series["Series1"].IsVisibleInLegend = true;
            chart2.Series["Series1"].Label = "#PERCENT{P2}";

            ///表格
            ///

            Worksheet worksheet = spreadsheetControl1.ActiveWorksheet;
            //worksheet.Columns["A"].WidthInCharacters = 32;
            //worksheet.Columns["B"].WidthInCharacters = 20;
            //Style style = workbook.Styles[BuiltInStyleId.Input];

            // Specify the content and formatting for a source cell.
            //worksheet.Cells["A1"].Value = "Source Cell";

            //Cell sourceCell = worksheet.Cells["B1"];
            //sourceCell.Formula = "= PI()";
            //sourceCell.NumberFormat = "0.0000";
            
           // worksheet.Cells[0].Value = "日期";
            worksheet.Cells[0].ColumnWidth = 300;
            worksheet.Cells[0].FillColor = Color.Silver;
            for (var dIndex = 1; dIndex <= model.OutProductDataList.Count; dIndex++)
            {
                worksheet.Cells[0, dIndex].ColumnWidth = 300;
                worksheet.Cells[1, dIndex].ColumnWidth = 300;
                worksheet.Cells[2, dIndex].ColumnWidth = 300;
                worksheet.Cells[3, dIndex].ColumnWidth = 300;
                worksheet.Cells[4, dIndex].ColumnWidth = 300;
                worksheet.Cells[5, dIndex].ColumnWidth = 300;

                worksheet.Cells[0, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Cells[1, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Cells[2, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Cells[3, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Cells[4, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Cells[5, dIndex].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
             //   cellA2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[0, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[1, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[2, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[3, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[4, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                worksheet.Cells[5, dIndex].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;

                worksheet.Cells[0, dIndex].Value = xValues[dIndex - 1];
                worksheet.Cells[0, dIndex].FillColor = Color.Silver;

                worksheet.Cells[1, dIndex].Value = model.OutProductDataList[dIndex - 1].DailyYield;
                worksheet.Cells[2, dIndex].Value = model.OutProductDataList[dIndex - 1].TargetYield;
                var dRate = model.OutProductDataList[dIndex - 1].TargetYield == 0 ? decimal.Parse("0") : (decimal.Parse(model.OutProductDataList[dIndex - 1].DailyYield.ToString())) / model.OutProductDataList[dIndex - 1].TargetYield;

                worksheet.Cells[3, dIndex].Value = string.Format("{0:p2}", dRate);
                worksheet.Cells[4, dIndex].Value = model.OutProductDataList[dIndex - 1].DefectCount;

                var defectRate = (1 - dRate)<0?0 : (1 - dRate);
                worksheet.Cells[5, dIndex].Value =string.Format("{0:p2}", defectRate);
            }
            var co = new string[] {
                 Client.Action.LanguageAction.Instance.BindLanguageTxt("pgfDate"),
                  Client.Action.LanguageAction.Instance.BindLanguageTxt("EveryYield"),
                   Client.Action.LanguageAction.Instance.BindLanguageTxt("TargetYield"),
                    Client.Action.LanguageAction.Instance.BindLanguageTxt("CompleteRate"),
                Client.Action.LanguageAction.Instance.BindLanguageTxt("DefectCount"),
                     Client.Action.LanguageAction.Instance.BindLanguageTxt("DefectRate")
            //    "日期", "每日产量", "目标产量", "达成率", "不良数", "不良率"
            };
            for (var index=0;index<co.Length;index++) {
                worksheet.Cells[index, 0].Value =co[index];
                worksheet.Cells[index, 0].FillColor = Color.Silver;
            }
            //worksheet.Cells[1, 0].Value = "每日产量";
            //worksheet.Cells[2, 0].Value = "目标产量";
            //worksheet.Cells[3, 0].Value = "达成率";
            //worksheet.Cells[4, 0].Value = "不良数";
            //worksheet.Cells[5, 0].Value = "不良率";
        }

        private void susQueryControl1_Load(object sender, EventArgs e)
        {
            susQueryControl1.OnQuery += SusQueryControl1_OnQuery;

        }
        void InitData()
        {
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].IsVisibleInLegend = true;
            chart1.Series["Series2"].IsValueShownAsLabel = true;
            chart1.Series["Series2"].IsVisibleInLegend = true;
            chart1.Series["Series3"].IsValueShownAsLabel = true;
            chart1.Series["Series3"].IsVisibleInLegend = true;
            chart1.Series["Series1"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("EveryYield"); //"每日产量";
            chart1.Series["Series2"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("TargetYield");// "目标产量";
            chart1.Series["Series3"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("DefectCount");//"不良数";

            chart2.Series["Series1"].IsValueShownAsLabel = true;
            chart2.Series["Series1"].IsVisibleInLegend = true;
            chart2.Series["Series1"].LegendText = Client.Action.LanguageAction.Instance.BindLanguageTxt("DefectRate");//"达成率";
            //Query(queryModel);
        }
        QueryModel queryModel;
        private void SusQueryControl1_OnQuery(QueryModel _queryModel)
        {
            queryModel = _queryModel;
            Query(queryModel);

        }

        private void GroupCompetitionReport_Load(object sender, EventArgs e)
        {
            InitData();
        }
    }
}
