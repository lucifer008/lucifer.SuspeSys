using Aspose.Cells;
using log4net;
using SuspeSys.Domain.Base;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.TestDataGeneratorTool
{
    public partial class frmToolMain : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(frmToolMain));
        public frmToolMain()
        {
            InitializeComponent();
        }
        static readonly string appId = "20190514000297454";
        static readonly string pwSc = "8HjZAp0M4STtTaljnXuK";
        private void btnGenerator_Click(object sender, EventArgs e)
        {
            //var sTxt = "生产完成";
            //MessageBox.Show(chs2cht(sTxt));
            string url = @"http://api.fanyi.baidu.com/api/trans/vip/translate?q=apple&from=en&to=zh&appid=2015063000000001&salt=1435660288&sign=f89f9594663708c1605f3d736d01d2d4";
            //var result = HttpsUtils.Get(url);
            var fResult = HttpsUtils.Translate("产量汇总", "vie");
            MessageBox.Show(fResult);
        }

        private void btnGenUserData_Click(object sender, EventArgs e)
        {
            try
            {
                Tools tool = new Tools();
                tool.GeneratorData();
                //tool.GeneratorEmployeeOrMembercardInfo();
                //tool.TeatGeneratorUserInfo();
                //tool.GenerantorSatingInfo();
                MessageBox.Show("生成完成");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }



        }
        LanguageServiceImpl languageService = new LanguageServiceImpl();
        private void btnGenSystemPara_Click(object sender, EventArgs e)
        {
            Tools tool = new Tools();

            tool.TestGeneratorSystemParamter();
            MessageBox.Show("生成完成");
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Cursor = Cursors.WaitCursor;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;

                var workbook = new Workbook(filePath);
                var sheets = workbook.Worksheets;
                IList<MulLanguage> mLanguageList = new List<MulLanguage>();
                foreach (Worksheet sheet in sheets)
                {
                    var cValue = sheet.Cells[0, 2];
                    //var rowInex = 1;
                    //var columnIndex = 2;
                    //var columnIndexMax = 8;
                    var columnKey = string.Empty;
                    //while (!"".Equals(columnKey = sheet.Cells[rowInex, columnIndex].Value.ToString()))
                    //{

                    //}
                    for (var row = 1; row < sheet.Cells.Rows.Count; row++)
                    {
                        var m = new MulLanguage();
                        try
                        {
                            m.ResKey = sheet.Cells[row, 2].Value?.ToString();
                            bool isExist = languageService.IsExist(m.ResKey);
                            if (!isAllUpdate && isExist) {
                                log.InfoFormat("增量更新!key={0} 不更新!",m.ResKey);
                                continue;
                            }
                            m.ResItem = sheet.Cells[row, 2].Value?.ToString();
                            m.SimplifiedChinese = sheet.Cells[row, 4].Value?.ToString();
                            m.TraditionalChinese = chs2cht(m.SimplifiedChinese);//sheet.Cells[row, 5].Value?.ToString();
                            m.English = sheet.Cells[row, 6].Value?.ToString();
                            if (isInvokeFanYiApi)
                            {
                                if (string.IsNullOrEmpty(m.English))
                                {
                                    var existEnglish = languageService.GetMulLanguageTxt(m.ResKey, 3);
                                    if (!string.IsNullOrEmpty(existEnglish))
                                    {
                                        m.English = existEnglish;
                                    }
                                    else
                                    {
                                        m.English = HttpsUtils.Translate(m.SimplifiedChinese, "en");
                                    }
                                }
                            }
                            if (isInvokeFanYiApi)
                                m.Vietnamese = HttpsUtils.Translate(m.SimplifiedChinese, "vie"); //sheet.Cells[row, 7].Value?.ToString();
                            m.Cambodia = sheet.Cells[row, 8].Value?.ToString();
                            var vietnamese = languageService.GetMulLanguageTxt(m.ResKey, 4);
                            if (string.IsNullOrEmpty(m.Vietnamese))
                            {
                                m.Vietnamese = vietnamese;
                            }
                            if (string.IsNullOrEmpty(m.ResKey))
                            {
                                break;
                            }
                            mLanguageList.Add(m);
                            log.InfoFormat("处理成功,key={0}", m.ResKey);
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }
                }
                //workbook.
                //var languageService = new LanguageServiceImpl();
                languageService.UploadLanguage(mLanguageList);
                MessageBox.Show("sucess");
                btnUpload.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 简/繁体转换
        /// </summary>
        /// <param name="input">输入中文文本</param>
        /// <param name="def">默认为简体转换为繁体</param>
        /// <returns>转换后的文本</returns>
        public static string chs2cht(string input, bool def = true)
        {
            return Microsoft.VisualBasic.Strings.StrConv(input,
                                                        (def) ? Microsoft.VisualBasic.VbStrConv.TraditionalChinese
                                                              : Microsoft.VisualBasic.VbStrConv.SimplifiedChinese,
                                                         0);
        }

        private void cbFanyiApi_CheckedChanged(object sender, EventArgs e)
        {
            isInvokeFanYiApi = cbFanyiApi.Checked;
        }
        bool isInvokeFanYiApi = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            isInvokeFanYiApi = cbFanyiApi.Checked;
        }
        bool isAllUpdate = true;
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            isAllUpdate= cbAll.Checked;
        }
    }
}
