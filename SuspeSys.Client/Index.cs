using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;
using SuspeSys.Client.Modules;
using SuspeSys.Client.Modules.Products;
using SuspeSys.Client.Modules.BasicInfo;
using SuspeSys.Client.Modules.CuttingBed;
using SuspeSys.Client.Modules.SewingMachine;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.RealtimeInfo;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Client.Modules.ProductionLineSet;
using SuspeSys.Client.Modules.TcpTest;
using SuspeSys.Client.Modules.Permission;
using SuspeSys.Client.Common;
using SuspeSys.Client.Modules.PersonnelManagement;
using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Client.Action;
using SuspeSys.Client.Modules.Reports;
using SuspeSys.Domain;
using DevExpress.XtraTreeList.Demos;
using SuspeSys.Client.Modules.Attendance;
using SuspeSys.Domain.Common;
using SusNet.Common.Utils;
using System.IO;
using SuspeSys.Client.Modules.SusDialog;
using log4net;
using SuspeSys.Client.Modules.LED;
using FangteCommon;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Client.Sqlite.Repository;
using SuspeSys.Client.Action.Common;
using System.Linq;
using SuspeSys.Client.Modules.MultipleLanguage;

namespace SuspeSys.Client
{
    public partial class Index : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        // public event EventHandler<SusEventArgs> SusMaxFormEvent;
        public Index()
        {
            InitializeComponent();
            xtraUserControl11.MainRibbonControl = ribbon;

            if (CurrentUser.Instance.IsAuthorization)
                barBtnClientMachine.Caption = "客户机：" + Domain.Common.CurrentUser.Instance.CurrentClientMachines?.ClientMachineName;
            else
            {
                //throw new AuthorizationException("软件未授权");
                //MessageBox.Show("软件未授权");
                //this.Close();
                //Application.Exit();
            }
            //barBtnClientMachine.Caption = "软件未授权";

            InitLanguage();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            Common.Utils.Permission.RibbonControlPermission.Process(ribbon);

            //new MainRibbonForm1().ShowDialog();
        
            InitData();
            new BaseAction();
            barEditItem6.EditValue = 15;
            DefaultMenu();
            InitCardReaderPort();
            // repositoryItemZoomTrackBar2.OwnerEdit.EditValue = 15;
        }

        private void InitCardReaderPort()
        {

            for (var p = 1; p < 11; p++)
            {
                repositoryItemComboBox2.Items.Add(p);
            }

            repositoryItemComboBox2.SelectedValueChanged += RepositoryItemComboBox2_SelectedValueChanged;
            barEditItem5.EditValue = 1;
            CardReaderHander("1");
        }
        private FangteCommon.FMCardReader comm;
        private void RepositoryItemComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {

            var selComb = sender as ComboBoxEdit;
            var portTxt = selComb.Text;
            CardReaderHander(portTxt);
        }
        private void CardReaderHander(string portTxt)
        {
            try
            {
                int port = 0;
                try
                {
                    port = Int32.Parse(portTxt);
                }
                catch
                {
                    port = 0;
                    //throw ex;
                }
                comm = FangteCommon.FMCardReader.GetPortInstance(FangteCommon.CardReaderTypeEnum.USB, port);
                comm.ReceiveData += new FangteCommon.ReceiveDataEventHandle(comm_USB_ReceiveData);//USB输出卡号事件
                if (comm.IsClosed)
                {
                    if (!comm.Open())
                    { }
                    barStaticItem4.Name = "beItemReaderPortDesc";
                    barStaticItem4.Caption = string.Format("读卡器端口:{0} 已打开!请刷卡!", portTxt);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                barStaticItem4.Caption = string.Format("读卡器端口:{0} 打开异常:{1}", portTxt, ex.Message);
            }
        }
        //刷卡跳转
        private void comm_USB_ReceiveData(object sender, ReceiveDataEventArgs e)
        {
            if (e != null)
            {
                //此处添加输出卡号后的事件
                //MessageBox.Show(e.CommData);

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    var cardNo = e.CommData;

                    var ext = CommonAction.GetList<SuspeSys.Domain.Hanger>().Where(f => f.HangerNo == long.Parse(cardNo));

                    if (0 == ext.Count())
                    {
                        var cardExit = CommonAction.GetList<SuspeSys.Domain.CardInfo>().Where(f => f.CardNo.Equals(int.Parse(cardNo).ToString()));
                        if (0 == cardExit.Count())
                        {
                            XtraMessageBox.Show(string.Format(LanguageAction.Instance.BindLanguageTxt("promptNotFind"), LanguageAction.Instance.BindLanguageTxt("CardNo")));//string.Format("卡未找到!"), "错误");
                            return;
                        }
                        var cardItem = cardExit.First();
                        switch (cardItem.CardType.Value)
                        {
                            case 4:
                                XtraMessageBox.Show(LanguageAction.Instance.BindLanguageTxt("promptEmployeeCard"), LanguageAction.Instance.BindLanguageTxt("promptTips"));//string.Format("员工卡!"), "温馨提示");
                                break;
                            case 2:
                                XtraMessageBox.Show(LanguageAction.Instance.BindLanguageTxt("promptClothesCardCard"), LanguageAction.Instance.BindLanguageTxt("promptTips"));//string.Format("衣车卡!"), "温馨提示");
                                break;
                            case 3:
                                XtraMessageBox.Show(LanguageAction.Instance.BindLanguageTxt("promptMechineRepairCard"), LanguageAction.Instance.BindLanguageTxt("promptTips"));//string.Format("机修卡!"), "温馨提示");
                                break;
                            default:
                                XtraMessageBox.Show(LanguageAction.Instance.BindLanguageTxt("promptNoknownCardType"), LanguageAction.Instance.BindLanguageTxt("promptTips"));//string.Format("未知卡类型!"), "温馨提示");
                                break;
                        }

                        return;
                    }

                    XtraTabPage tab = new SusXtraTabPage();
                    tab.Text = LanguageAction.Instance.BindLanguageTxt("Billing_CoatHanger"); //"衣架信息";
                    tab.Name = string.Format("{0}", LanguageAction.Instance.BindLanguageTxt("Billing_CoatHanger"));//"衣架信息");
                    if (xtraUserControl11.MainTabControl.TabPages.Contains(tab))
                        xtraUserControl11.MainTabControl.TabPages.Remove(tab);
                    XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new CoatHangerIndex(xtraUserControl11, e.CommData));
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    //XtraMessageBox.Show(ex.Message, "错误");
                    XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        // AccordionControlElementCollection acecProductData =null;
        AccordionControlElement[] acElementArr = null;
        void InitData()
        {
            var ldPanelControls = xtraUserControl11.LeftDockPanel.Controls;
            foreach (Control c in ldPanelControls)
            {
                if (c is ControlContainer)
                {
                    foreach (Control xc in c.Controls)
                    {
                        if (xc is AccordionControl)
                        {
                            var acc = xc as AccordionControl;
                            acElementArr = new AccordionControlElement[acc.Elements.Count];
                            acc.Elements.CopyTo(acElementArr, 0);
                            // acc.Elements.Clear();
                            // acc.Refresh();
                        }
                    }
                    //MessageBox.Show(c.ToString());
                }
            }
            var siteGroupList = CurrentUser.Instance?.UserSiteGroupList;
            if (null != siteGroupList)
            {

                foreach (var sg in siteGroupList)
                {
                    var bbItem = new BarStaticItem()
                    {
                        Caption = sg.GroupNo?.Trim(),
                        Tag = sg
                    };
                    bbItem.ItemClick += BbItem_ItemClick;
                    //  bbItem.CloseOnMouseOuterClick = DevExpress.Utils.DefaultBoolean.True;
                    bSubItemSiteGroup.AddItem(bbItem);
                }
                if (siteGroupList.Count > 0)
                {
                    CurrentUser.Instance.CurrentSiteGroup = siteGroupList[0];
                    barsItemCurrentSiteGroup.Tag = siteGroupList[0];
                    barsItemCurrentSiteGroup.Caption = string.Format("当前组:{0}", siteGroupList[0]?.GroupNo?.Trim());
                }
            }
        }

        /// <summary>
        /// 切换生产组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BbItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var selectedSiteGroup = e.Item.Tag as SiteGroup;
                if (null != selectedSiteGroup)
                {
                    CurrentUser.Instance.CurrentSiteGroup = selectedSiteGroup;
                    CurrentUser.Instance.OnGroupChange(selectedSiteGroup);
                }
                barsItemCurrentSiteGroup.Caption = string.Format("当前组:{0}", e.Item.Caption);
                //this.Focus();
                //var links=bSubItemSiteGroup.Links;
                //foreach (BarItemLink iks in links) { iks.Focus(); }


            }
            catch (Exception ex)
            {
                log.Error(ex);
                // LanguageAction.Instance.BindLanguageTxt("errorInfo");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            // e.Item
        }

        //制单信息
        private void barBtn_billing_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                xtraUserControl11.LeftAccordionControl.Elements.AddRange(acElementArr);
                xtraUserControl11.LeftAccordionControl.Refresh();
                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //裁床管理
        private void barBtn_CuttingRoomManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var goodCardInfo = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("goodCardInfoIndex"),// "货卡信息",
                    Expanded = true
                };
                goodCardInfo.Tag = new TagExt(PermissionConstant.goodCardInfoIndex) { Name = "goodCardInfoIndex" };
                //goodCardInfo.Tag = new TagExt(PermissionConstant.CuttingRoomManage_GoodCardInfo);
                //var customerInfo = new AccordionControlElement(ElementStyle.Item) { Text = "客户信息" };
                //customerInfo.Click += CustomerInfo_Click;
                //var customerOrderInfo = new AccordionControlElement(ElementStyle.Item) { Text = "客户订单" };
                //goodCardInfo.Elements.Add(goodCardInfo);
                //goodCardInfo.Elements.Add(customerOrderInfo);

                var goodCardInfoIndex = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("goodCardInfoIndex") };//Text = "货卡信息" };
                goodCardInfoIndex.Tag = new TagExt(PermissionConstant.CuttingRoomManage_GoodCardInfo) { Name = "goodCardInfoIndex" };

                goodCardInfo.Elements.Add(goodCardInfoIndex);
                goodCardInfoIndex.Click += GoodCardInfo_Click;
                xtraUserControl11.LeftAccordionControl.Elements.Add(goodCardInfo);
                xtraUserControl11.LeftAccordionControl.Refresh();
                //  LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //货卡信息
        private void GoodCardInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = LanguageAction.Instance.BindLanguageTxt("goodCardInfoIndex");//"货卡信息";
                tab.Name = string.Format("{0}", LanguageAction.Instance.BindLanguageTxt("goodCardInfoIndex"));
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new GoodCardInfoIndex());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //客户信息
        private void CustomerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = LanguageAction.Instance.BindLanguageTxt("menuCustomerInfo");//"客户信息"; 
                tab.Name = LanguageAction.Instance.BindLanguageTxt("menuCustomerInfo");// string.Format("{0}", "客户信息");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new CustomerInfoIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //  XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //产品基础数据
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var productBasicData = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("gpOrderInfo"),//"订单信息",
                    Expanded = true
                };
                productBasicData.Tag = new TagExt(PermissionConstant.gpOrderInfo) { Name = "gpOrderInfo" };

                var productPart = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("productPart") };//Text = "产品部位" };
                productPart.Tag = new TagExt(PermissionConstant.ProductBaseData_ProductPart) { Name = "productPart" };
                productPart.Click += ProductPart_Click;

                var basicSizeTable = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("basicSizeTable") }; //"基本尺码表" };
                basicSizeTable.Tag = new TagExt(PermissionConstant.ProductBaseData_BasicSizeTable) { Name = "basicSizeTable" };

                var basicColorTable = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("basicColorTable") };// "基本颜色表" };
                basicColorTable.Tag = new TagExt(PermissionConstant.ProductBaseData_BasicColorTable) { Name = "basicColorTable" };

                //var style = new AccordionControlElement(ElementStyle.Item) { Text = "款式工艺表" };
                //style.Tag = new TagExt(PermissionConstant.ProductBaseData_Style);

                productBasicData.Elements.Add(productPart);
                productBasicData.Elements.Add(basicSizeTable);
                productBasicData.Elements.Add(basicColorTable);
                //productBasicData.Elements.Add(style);

                basicSizeTable.Click += BasicSizeTable_Click;
                basicColorTable.Click += BasicColorTable_Click;
                //style.Click += Style_Click;

                xtraUserControl11.LeftAccordionControl.Elements.Add(productBasicData);
                xtraUserControl11.LeftAccordionControl.Refresh();
                //LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //款式工艺表
        private void Style_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = "款式工艺表";
                tab.Name = string.Format("{0}", "款式工艺表");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new StyleIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //基本颜色表
        private void BasicColorTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = LanguageAction.Instance.BindLanguageTxt("basicColorTable");//"基本颜色表";
                tab.Name = string.Format("{0}", LanguageAction.Instance.BindLanguageTxt("basicColorTable"));//"基本颜色表");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new BasicColorTableIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //基本尺码表
        private void BasicSizeTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = LanguageAction.Instance.BindLanguageTxt("basicSizeTable");// "基本尺码表";
                tab.Name = string.Format("{0}", LanguageAction.Instance.BindLanguageTxt("basicSizeTable"));//"基本尺码表");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new BasicSizeTableIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //产品部位
        private void ProductPart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                AccordionControlElement aElement = sender as AccordionControlElement;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = aElement.Text;// "产品部位";
                tab.Name = string.Format("{0}", aElement.Text);//"产品部位");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProductPartIndex());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
        //工艺基础数据
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var craftworkBasicData = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = "工艺基础数据",
                    Expanded = true
                };
                craftworkBasicData.Tag = new TagExt(PermissionConstant.barBtn_ProcessBaseData) { Name = "barBtn_ProcessBaseData" };

                var basicProcessSection = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("basicProcessSection") };//"基本工序段" };
                basicProcessSection.Tag = new TagExt(PermissionConstant.ProcessBaseData_BasicProcessSection) { Name = "basicProcessSection" };

                var basicProcessLirbary = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("basicProcessLirbary") }; //"基本工序库" };
                basicProcessLirbary.Tag = new TagExt(PermissionConstant.ProcessBaseData_BasicProcessLirbary) { Name = "basicProcessLirbary" };

                var styleProcessLirbary = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("styleProcessLirbary") };// "款式工序库" };
                styleProcessLirbary.Tag = new TagExt(PermissionConstant.ProcessBaseData_StyleProcessLirbary) { Name = "styleProcessLirbary" };

                var defectCode = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("defectCode") };//"疵点代码" };
                defectCode.Tag = new TagExt(PermissionConstant.ProcessBaseData_DefectCode) { Name = "defectCode" };

                var lackOfMaterialCode = new AccordionControlElement(ElementStyle.Item) { Text = LanguageAction.Instance.BindLanguageTxt("lackOfMaterialCode") };//"缺料代码" };
                lackOfMaterialCode.Tag = new TagExt(PermissionConstant.ProcessBaseData_LackOfMaterialCode) { Name = "lackOfMaterialCode" };

                craftworkBasicData.Elements.Add(basicProcessSection);
                craftworkBasicData.Elements.Add(basicProcessLirbary);
                craftworkBasicData.Elements.Add(styleProcessLirbary);
                craftworkBasicData.Elements.Add(defectCode);
                craftworkBasicData.Elements.Add(lackOfMaterialCode);


                basicProcessSection.Click += BasicProcessSection_Click;
                basicProcessLirbary.Click += BasicProcessLirbary_Click;
                styleProcessLirbary.Click += StyleProcessLirbary_Click;
                defectCode.Click += DefectCode_Click;
                lackOfMaterialCode.Click += LackOfMaterialCode_Click;

                xtraUserControl11.LeftAccordionControl.Elements.Add(craftworkBasicData);
                xtraUserControl11.LeftAccordionControl.Refresh();
                //  LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //缺料代码
        private void LackOfMaterialCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //throw new NotImplementedException();
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"缺料代码";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text);//"缺料代码");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new LackOfMaterialCodeIndex());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //疵点代码
        private void DefectCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //throw new NotImplementedException();
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"疵点代码";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text);//"疵点代码");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new DefectCodeIndex());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                // XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //款式工艺库
        private void StyleProcessLirbary_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"款式工序库";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text);//"款式工艺库");
                // XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new StyleProcessLirbaryIndex(xtraUserControl11));
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new StyleProcessLirbaryInput(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //基本工序库
        private void BasicProcessLirbary_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"基本工序库";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text);//"基本工序库");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new BasicProcessLirbaryIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //基本工序段
        private void BasicProcessSection_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"基本工序段";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text);// "基本工序段");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new BasicProcessSectionIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //订单信息
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var orderInfo = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = "订单信息",
                    Expanded = true
                };
                var customerInfo = new AccordionControlElement(ElementStyle.Item) { Text = "客户信息" };
                customerInfo.Tag = new TagExt(PermissionConstant.OrderInfo_CustomerInfo);
                customerInfo.Click += CustomerInfo_Click;

                var customerOrderInfo = new AccordionControlElement(ElementStyle.Item) { Text = "客户订单" };
                customerOrderInfo.Tag = new TagExt(PermissionConstant.OrderInfo_CustomerOrderInfo);
                // var backageCustomerInfo = new AccordionControlElement(ElementStyle.Item) { Text = "新制单录入" };

                customerOrderInfo.Click += CustomerOrderInfo_Click;
                //backageCustomerInfo.Click += BackageCustomerInfo_Click;
                orderInfo.Elements.Add(customerInfo);
                orderInfo.Elements.Add(customerOrderInfo);
                //orderInfo.Elements.Add(backageCustomerInfo);

                xtraUserControl11.LeftAccordionControl.Elements.Add(orderInfo);
                xtraUserControl11.LeftAccordionControl.Refresh();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BackageCustomerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = "新制单录入";
                tab.Name = string.Format("{0}", "新制单录入");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new PurchaseProcessOrdersInput());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //客户订单信息
        private void CustomerOrderInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = "客户订单信息";
                tab.Name = string.Format("{0}", "客户订单信息");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new CustomerOrderIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //人事管理
        private void barBtn_PersonnelManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var personnelMattersMsg = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("barBtn_PersonnelManagement"),//"人事管理",
                    Expanded = true
                };
                personnelMattersMsg.Tag = new TagExt(PermissionConstant.barBtn_PersonnelManagement) { Name = "barBtn_PersonnelManagement" };

                var factory = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("factory"),//"工厂信息",
                    Tag = "factory"
                };
                factory.Tag = new TagExt(PermissionConstant.factory) { Name = "factory" };

                var workshop = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("workshopInfo")//"车间信息"
                    ,
                    Tag = "workshopInfo"
                };
                workshop.Tag = new TagExt(PermissionConstant.workshop) { Name = "workshopInfo" };

                var productGroup = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("productGroup") //"生产组别"
                };
                productGroup.Tag = new TagExt(PermissionConstant.PersonnelManagement_ProductGroup) { Name = "productGroup" };

                var departmentInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("departmentInfo")//"部门信息"
                };
                departmentInfo.Tag = new TagExt(PermissionConstant.PersonnelManagement_DepartmentInfo) { Name = "departmentInfo" };

                var professionInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("professionInfo")//"工种信息"
                };
                professionInfo.Tag = new TagExt(PermissionConstant.PersonnelManagement_ProfessionInfo) { Name = "professionInfo" };

                var positionInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("positionInfo")//"职务信息"
                };
                positionInfo.Tag = new TagExt(PermissionConstant.PersonnelManagement_PositionInfo) { Name = "positionInfo" };

                var employeeInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("employeeInfo")//"员工资料" 
                };
                employeeInfo.Tag = new TagExt(PermissionConstant.PersonnelManagement_EmployeeInfo) { Name = "employeeInfo" };

                var msgCardInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("msgCardInfo") //"管理卡信息"
                };
                msgCardInfo.Tag = new TagExt(PermissionConstant.PersonnelManagement_MsgCardInfo) { Name = "msgCardInfo" };

                personnelMattersMsg.Elements.Add(factory);
                personnelMattersMsg.Elements.Add(workshop);
                personnelMattersMsg.Elements.Add(productGroup);
                personnelMattersMsg.Elements.Add(departmentInfo);
                personnelMattersMsg.Elements.Add(professionInfo);
                personnelMattersMsg.Elements.Add(positionInfo);
                personnelMattersMsg.Elements.Add(employeeInfo);
                personnelMattersMsg.Elements.Add(msgCardInfo);


                productGroup.Click += ProductGroup_Click;
                departmentInfo.Click += DepartmentInfo_Click;
                professionInfo.Click += ProfessionInfo_Click;
                positionInfo.Click += PositionInfo_Click;
                employeeInfo.Click += EmployeeInfo_Click;
                msgCardInfo.Click += MsgCardInfo_Click;
                factory.Click += Factory_Click;
                workshop.Click += Workshop_Click;
                xtraUserControl11.LeftAccordionControl.Elements.Add(personnelMattersMsg);
                xtraUserControl11.LeftAccordionControl.Refresh();

                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Workshop_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"车间信息"
                , typeof(WorkshopIndex), sender);
        }

        private void Factory_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"工厂信息"
                , typeof(FactoryIndex), sender);
        }

        //管理卡信息
        private void MsgCardInfo_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"管理卡信息"
                , typeof(CardInfoIndex), sender);

        }
        //员工资料
        private void EmployeeInfo_Click(object sender, EventArgs e)
        {
            string txt = ((AccordionControlElement)sender).Text;
            this.MenuOpenForm(txt, typeof(EmployeeIndex), sender);
        }
        //职务信息
        private void PositionInfo_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text
                //"职务信息"
                , typeof(PositionIndex), sender);
        }
        //工种信息
        private void ProfessionInfo_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"工种信息"
                , typeof(WorkTypeIndex), sender);
        }
        //部门信息
        private void DepartmentInfo_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"部门信息"
                , typeof(DepartmentIndex), sender);
        }
        //生产组别
        private void ProductGroup_Click(object sender, EventArgs e)
        {
            this.MenuOpenForm((sender as AccordionControlElement).Text//"生产组别"
                , typeof(SiteGroupIndex), sender);
            //this.MenuOpenForm("生产组别")
        }

        private void MenuOpenForm(string formName, Type type, object sender = null)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = formName;
                tab.Name = string.Format("{0}", formName);

                Assembly assembly = Assembly.GetExecutingAssembly();

                object[] parameters = new object[1];
                parameters[0] = xtraUserControl11;

                //反射实例化页面，参数传参
                object obj = assembly.CreateInstance(type.FullName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);// 创建类的实例 

                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, (XtraUserControl)obj);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //权限管理
        private void barBtn_AuthorityManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var permissionMsg = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("barBtn_AuthorityManagement") //"权限管理"
                    ,
                    Expanded = true
                };

                permissionMsg.Tag = new TagExt(PermissionConstant.barBtn_AuthorityManagement) { Name = "barBtn_AuthorityManagement" };

                var moduleMsg = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("MenuMgt")//"菜单管理"
                    ,
                };
                moduleMsg.Tag = new TagExt(PermissionConstant.AuthorityManagement_ModuleMsg) { Name = "MenuMgt" };

                var roleMsg = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("roleMsg") //"角色管理"
                };
                roleMsg.Tag = new TagExt(PermissionConstant.AuthorityManagement_RoleMsg) { Name = "roleMsg" };

                var userMsg = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("userMsg") //"用户管理"
                };
                userMsg.Tag = new TagExt(PermissionConstant.AuthorityManagement_UserMsg) { Name = "userMsg" };

                var userOperatorLog = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("userOperatorLog")  //"用户操作日志"
                };
                userOperatorLog.Tag = new TagExt(PermissionConstant.AuthorityManagement_UserOperatorLog) { Name = "userOperatorLog" };


                permissionMsg.Elements.Add(moduleMsg);
                permissionMsg.Elements.Add(roleMsg);
                permissionMsg.Elements.Add(userMsg);
                permissionMsg.Elements.Add(userOperatorLog);

                xtraUserControl11.LeftAccordionControl.Elements.Add(permissionMsg);
                xtraUserControl11.LeftAccordionControl.Refresh();

                //事件
                moduleMsg.Click += ModuleMsg_Click;
                roleMsg.Click += RoleMsg_Click;
                userMsg.Click += UserMsg_Click;
                userOperatorLog.Click += UserOperatorLog_Click;
                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 用户操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserOperatorLog_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"用户操作日志";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"用户操作日志"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new UserOperatorLogIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //用户管理
        private void UserMsg_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"用户管理";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"用户管理"
                    );

                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new UserIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);

                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //角色管理
        private void RoleMsg_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"角色管理";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"角色管理"
                    );

                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new RoleIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// 模块信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModuleMsg_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"菜单管理";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"菜单管理"
                    );

                //获取特性方法
                Type t = typeof(ModuleIndex);
                ////查找该方法
                //MethodInfo mi = t.GetMethod(method);
                FormPermissionAttribute attr = t.GetCustomAttribute(typeof(FormPermissionAttribute), true) as FormPermissionAttribute;

                if (attr == null)
                {
                    throw new Exception(string.Format("{0}未设置权限属性", t.Name));
                }

                if (!attr.hasPermission)
                {
                    throw new Exception(string.Format("{0}没有权限访问", t.Name));

                }

                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ModuleIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //考勤管理
        private void barBtn_AttendanceManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var checkAttendance = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("barBtn_AttendanceManagement")//"考勤管理"
                    ,
                    Expanded = true
                };
                checkAttendance.Tag = new TagExt(PermissionConstant.barBtn_AttendanceManagement) { Name = "barBtn_AttendanceManagement" };

                var holidayInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("holidayInfo")//"假日信息"
                };
                holidayInfo.Tag = new TagExt(PermissionConstant.AttendanceManagement_HolidayInfo) { Name = "holidayInfo" };

                var classsesInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("classsesInfo")//"班次信息"
                };
                classsesInfo.Tag = new TagExt(PermissionConstant.AttendanceManagement_ClasssesInfo) { Name = "classsesInfo" };

                var employeeScheduling = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("employeeScheduling")//"员工排班表"
                };
                employeeScheduling.Tag = new TagExt(PermissionConstant.AttendanceManagement_EmployeeScheduling) { Name = "employeeScheduling" };


                checkAttendance.Elements.Add(holidayInfo);
                checkAttendance.Elements.Add(classsesInfo);
                checkAttendance.Elements.Add(employeeScheduling);
                holidayInfo.Click += HolidayInfo_Click;
                classsesInfo.Click += ClasssesInfo_Click;
                employeeScheduling.Click += EmployeeScheduling_Click;
                xtraUserControl11.LeftAccordionControl.Elements.Add(checkAttendance);
                xtraUserControl11.LeftAccordionControl.Refresh();

                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 假日信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HolidayInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;// "假日信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"假日信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new HolidayIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //员工排班表
        private void EmployeeScheduling_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;// "员工排班表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"员工排班表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ClassesEmployeesIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //班次信息
        private void ClasssesInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"班次信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"班次信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ClassesInfoIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //衣车管理
        private void barBtn_ClothingCarManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var sewingMachineBasic = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("sewingMachineBasic")//"基础信息"
                    ,
                    Expanded = true
                };
                sewingMachineBasic.Tag = new TagExt(PermissionConstant.sewingMachineBasic) { Name = "sewingMachineBasic" };

                var sewingMachineType = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("sewingMachineType")//"衣车类别表"
                };
                sewingMachineType.Tag = new TagExt(PermissionConstant.ClothingCarManagement_EwingMachineType) { Name = "sewingMachineType" };

                var falutCodeTable = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("falutCodeTable")//"故障代码表"
                };
                falutCodeTable.Tag = new TagExt(PermissionConstant.ClothingCarManagement_FalutCodeTable) { Name = "falutCodeTable" };

                var sewingMachineData = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("sewingMachineData") //"衣车资料"
                };
                sewingMachineData.Tag = new TagExt(PermissionConstant.ClothingCarManagement_SewingMachineData) { Name = "sewingMachineData" };

                var sewingMachineLoginLog = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("sewingMachineLoginLog") //"衣车登录日志"
                };
                sewingMachineLoginLog.Tag = new TagExt(PermissionConstant.ClothingCarManagement_SewingMachineData) { Name = "sewingMachineLoginLog" };

                var sewingMachineRepairLog = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("sewingMachineRepairLog") //"衣车维修日志"
                };
                sewingMachineRepairLog.Tag = new TagExt(PermissionConstant.ClothingCarManagement_SewingMachineData) { Name = "sewingMachineRepairLog" };


                var mechanicEmployee = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("mechanicEmployee")//"机修人员表"
                };
                mechanicEmployee.Tag = new TagExt(PermissionConstant.ClothingCarManagement_MechanicEmployee) { Name = "mechanicEmployee" };

                sewingMachineBasic.Elements.Add(sewingMachineType);
                sewingMachineBasic.Elements.Add(falutCodeTable);
                sewingMachineBasic.Elements.Add(sewingMachineData);
                sewingMachineBasic.Elements.Add(mechanicEmployee);
                sewingMachineBasic.Elements.Add(sewingMachineLoginLog);
                sewingMachineBasic.Elements.Add(sewingMachineRepairLog);

                sewingMachineType.Click += SewingMachineType_Click;
                falutCodeTable.Click += FalutCodeTable_Click;
                sewingMachineData.Click += SewingMachineData_Click;
                mechanicEmployee.Click += MechanicEmployee_Click;
                sewingMachineLoginLog.Click += SewingMachineLoginLog_Click;
                sewingMachineRepairLog.Click += SewingMachineRepairLog_Click;

                xtraUserControl11.LeftAccordionControl.Elements.Add(sewingMachineBasic);
                xtraUserControl11.LeftAccordionControl.Refresh();
                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SewingMachineRepairLog_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"衣车维修日志";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣车维修日志"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new SewingMachineRepairLogIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SewingMachineLoginLog_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"衣车登录日志";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣车登录日志"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new SewingMachineLoginLogIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //机修人员表
        private void MechanicEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"机修人员表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"机修人员表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new MechanicEmployeeIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //衣车资料
        private void SewingMachineData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"衣车资料";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣车资料"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new SewingMachineDataIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //故障代码表
        private void FalutCodeTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"故障代码表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"故障代码表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new FalutCodeTableIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //衣车类别表
        private void SewingMachineType_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"衣车类别表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣车类别表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new SewingMachineTypeIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //实时信息-->变更为生产管理
        private void barBtn_RealTimeInformation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DefaultMenu();
                //LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DefaultMenu()
        {
            xtraUserControl11.LeftAccordionControl.Elements.Clear();
            var aceProductMsg = new AccordionControlElement(ElementStyle.Group)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("barBtn_RealTimeInformation")//"生产管理"
                ,
                Expanded = true
            };
            aceProductMsg.Tag = new TagExt(PermissionConstant.Prodcut_Msg) { Name = "barBtn_RealTimeInformation" };

            var processOrder = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("ManufactoryOrder")//"生产制单"
            };
            processOrder.Tag = new TagExt(PermissionConstant.Billing_ProcessOrderIndex) { Name = "ManufactoryOrder" };

            var processFlow = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("Billing_ProcessFlow")//"制单工序"
            };
            processFlow.Tag = new TagExt(PermissionConstant.Billing_ProcessFlowIndex) { Name = "Billing_ProcessFlow" };

            var processFlowChart = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("Billing_ProcessFlowChart")//"工艺路线图"
            };
            processFlowChart.Tag = new TagExt(PermissionConstant.Billing_ProcessFlowChartIndex) { Name = "Billing_ProcessFlowChart" };

            var productRealtimeInfo = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("Billing_ProductRealtimeInfo")//"产线实时信息"
            };
            productRealtimeInfo.Tag = new TagExt(PermissionConstant.Billing_ProductRealtimeInfo) { Name = "Billing_ProductRealtimeInfo" };

            var productsingInfo = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("Billing_ProductsingInfo")//"在制品信息"
            };
            productsingInfo.Tag = new TagExt(PermissionConstant.Billing_ProductsingInfo) { Name = "Billing_ProductsingInfo" };

            //var routeChart = new AccordionControlElement(ElementStyle.Item) { Text = "工艺路线图" };
            var coatHanger = new AccordionControlElement(ElementStyle.Item)
            {
                Text = LanguageAction.Instance.BindLanguageTxt("Billing_CoatHanger")//"衣架信息"
            };
            coatHanger.Tag = new TagExt(PermissionConstant.Billing_ProductsingInfo) { Name = "Billing_CoatHanger" };

            aceProductMsg.Elements.Add(processOrder);
            aceProductMsg.Elements.Add(processFlow);
            aceProductMsg.Elements.Add(processFlowChart);

            aceProductMsg.Elements.Add(productRealtimeInfo);
            aceProductMsg.Elements.Add(productsingInfo);
            // realTimeInfo.Elements.Add(routeChart);
            aceProductMsg.Elements.Add(coatHanger);

            processOrder.Click += ProcessOrder_Click; ;
            processFlow.Click += ProcessFlow_Click;
            processFlowChart.Click += ProcessFlowChart_Click;

            productRealtimeInfo.Click += ProductRealtimeInfo_Click;
            productsingInfo.Click += ProductsingInfo_Click;
            coatHanger.Click += CoatHanger_Click;
            // routeChart.Click += RouteChart_Click;

            xtraUserControl11.LeftAccordionControl.Elements.Add(aceProductMsg);
            xtraUserControl11.LeftAccordionControl.Refresh();
        }

        private void ProcessFlowChart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"工艺路线图";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"工艺路线图"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProcessFlowChartIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ProcessFlow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();
                tab.Text = (sender as AccordionControlElement).Text;//"制单工序";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"制单工序"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProcessFlowIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ProcessOrder_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"生产制单";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"生产制单"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProcessOrderIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //工艺路线图
        private void RouteChart_Click(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"工艺路线图";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"工艺路线图"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProcessFlowChartIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //衣架信息
        private void CoatHanger_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"衣架信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"衣架信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new CoatHangerIndex(xtraUserControl11, true));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //在制品信息
        private void ProductsingInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"在制品信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"在制品信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProductsingInfoIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //产线实时信息
        private void ProductRealtimeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"产线实时信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"产线实时信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProductRealtimeInfoIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                log.Error(ex);
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //统计报表/实时报表
        private void barBtn_StatisticalReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var reportInfo = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("reportInfo")//"报表信息"
                    ,
                    Expanded = true
                };
                reportInfo.Tag = new TagExt(PermissionConstant.report_Info) { Name = "reportInfo" };

                var yieldCollect = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("yieldCollect")//"产量汇总"
                    ,
                    Tag = "yieldCollect"
                };
                yieldCollect.Tag = new TagExt(PermissionConstant.yieldCollect) { Name = "yieldCollect" };

                var employeeYieldReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("employeeYieldReport")//"员工产量报表"
                    ,
                    Tag = "employeeYieldReport"
                };
                employeeYieldReport.Tag = new TagExt(PermissionConstant.employeeYieldReport) { Name = "employeeYieldReport" };

                //   var productProgressReport = new AccordionControlElement(ElementStyle.Item) { Text = "生产进度报表" };
                var workingHoursAnalysisReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("workingHoursAnalysisReport")//"工时分析报表"
                    ,
                    Tag = "workingHoursAnalysisReport"
                };
                workingHoursAnalysisReport.Tag = new TagExt(PermissionConstant.workingHoursAnalysisReport) { Name = "workingHoursAnalysisReport" };

                // var processOrderOverlappingReport = new AccordionControlElement(ElementStyle.Item) { Text = "制单工序交叉报表" };
                var productItemReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("productItemReport")//"产出明细报表"
                    ,
                    Tag = "productItemReport"
                };
                productItemReport.Tag = new TagExt(PermissionConstant.productItemReport) { Name = "productItemReport" };

                var reworkDetailReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("reworkDetailReport")//"返工详情报表"
                    ,
                    Tag = "reworkDetailReport"
                };
                reworkDetailReport.Tag = new TagExt(PermissionConstant.reworkDetailReport) { Name = "reworkDetailReport" };

                var reworkCollAndDefectAnalysisReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("reworkCollAndDefectAnalysisReport")///"返工汇总"
                    ,
                    Tag = "reworkCollAndDefectAnalysisReport"
                };
                reworkCollAndDefectAnalysisReport.Tag = new TagExt(PermissionConstant.reworkCollAndDefectAnalysisReport) { Name = "reworkCollAndDefectAnalysisReport" };

                var groupCompetitionReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("groupCompetitionReport")//"产量达标详情表"
                    ,
                    Tag = "groupCompetitionReport"
                };
                groupCompetitionReport.Tag = new TagExt(PermissionConstant.groupCompetitionReport) { Name = "groupCompetitionReport" };

                var flowBalanceTableReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("flowBalanceTableReport")//"工序平衡表"
                    ,
                    Tag = "flowBalanceTableReport"
                };
                flowBalanceTableReport.Tag = new TagExt(PermissionConstant.flowBalanceTableReport) { Name = "flowBalanceTableReport" };

                var defectAnalysisReport = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("defectAnalysisReport")//"疵点分析图"
                    ,
                    Tag = "defectAnalysisReport"
                };
                defectAnalysisReport.Tag = new TagExt(PermissionConstant.defectAnalysisReport) { Name = "defectAnalysisReport" };

                yieldCollect.Click += YieldCollect_Click;
                employeeYieldReport.Click += EmployeeYieldReport_Click;
                workingHoursAnalysisReport.Click += WorkingHoursAnalysisReport_Click;
                productItemReport.Click += ProductItemReport_Click;
                reworkDetailReport.Click += ReworkDetailReport_Click;
                groupCompetitionReport.Click += GroupCompetitionReport_Click;
                flowBalanceTableReport.Click += FlowBalanceTableReport_Click;
                defectAnalysisReport.Click += DefectAnalysisReport_Click;
                reworkCollAndDefectAnalysisReport.Click += ReworkCollAndDefectAnalysisReport_Click;
                reportInfo.Elements.Add(yieldCollect);
                reportInfo.Elements.Add(employeeYieldReport);
                //    reportInfo.Elements.Add(productProgressReport);
                reportInfo.Elements.Add(workingHoursAnalysisReport);
                //    reportInfo.Elements.Add(processOrderOverlappingReport);
                reportInfo.Elements.Add(productItemReport);
                reportInfo.Elements.Add(reworkDetailReport);
                reportInfo.Elements.Add(reworkCollAndDefectAnalysisReport);
                reportInfo.Elements.Add(groupCompetitionReport);
                reportInfo.Elements.Add(flowBalanceTableReport);
                reportInfo.Elements.Add(defectAnalysisReport);

                xtraUserControl11.LeftAccordionControl.Elements.Add(reportInfo);
                xtraUserControl11.LeftAccordionControl.Refresh();
                //  LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //返工汇总
        private void ReworkCollAndDefectAnalysisReport_Click(object sender, EventArgs e)
        {
            try

            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"返工汇总";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"返工汇总"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ReworkCollAndDefectAnalysisReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        static ILog log = LogManager.GetLogger(typeof(Index));
        //疵点分析图
        private void DefectAnalysisReport_Click(object sender, EventArgs e)
        {
            //

            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;// "疵点分析图";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text///"疵点分析图"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new DefectAnalysisReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //工序平衡
        private void FlowBalanceTableReport_Click(object sender, EventArgs e)
        {
            //  
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;// "工序平衡表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"工序平衡表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new FlowBalanceTableReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //产量达标详情表
        private void GroupCompetitionReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"产量达标详情表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"产量达标详情表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new GroupCompetitionReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //返工详情报表
        private void ReworkDetailReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text; //"返工详情报表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"返工详情报表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ReworkDetailReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //产出明细报表
        private void ProductItemReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"产出明细报表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"产出明细报表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProductItemReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //工时分析报表
        private void WorkingHoursAnalysisReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"工时分析报表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"工时分析报表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new WorkingHoursAnalysisReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //员工产量报表
        private void EmployeeYieldReport_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"员工产量报表";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"员工产量报表"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new EmployeeYieldReport(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //产量汇总
        private void YieldCollect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"产量汇总";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"产量汇总"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new YieldCollect(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //生产线
        private void barBtn_ProductionLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                xtraUserControl11.LeftAccordionControl.Elements.Clear();
                var realTimeInfo = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("productLine")//"生产线"
                    ,
                    Expanded = true
                };
                realTimeInfo.Tag = new TagExt(PermissionConstant.productLine) { Name = "productLine" };

                var controlSet = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("controlSet")//"控制端配置"
                };
                controlSet.Tag = new TagExt(PermissionConstant.ProductionLine_ControlSet) { Name = "controlSet" };

                var pipelineMsg = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("pipelineMsg")//"流水线管理"
                };
                pipelineMsg.Tag = new TagExt(PermissionConstant.ProductionLine_PipelineMsg) { Name = "pipelineMsg" };

                var bridgingSet = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("bridgingSet")//"桥接配置"
                };
                bridgingSet.Tag = new TagExt(PermissionConstant.ProductionLine_BridgingSet) { Name = "bridgingSet" };

                var clientInfo = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("clientInfo")//"客户机信息"
                };
                clientInfo.Tag = new TagExt(PermissionConstant.ProductionLine_ClientInfo) { Name = "clientInfo" };

                var systemMsg = new AccordionControlElement(ElementStyle.Group)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("systemMsg")//"系统参数"
                };
                systemMsg.Tag = new TagExt(PermissionConstant.ProductionLine_SystemMsg) { Name = "systemMsg" };
                //用户参数
                var userParamter = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("userParamter")//"用户参数"
                    ,
                    Expanded = false
                };
                userParamter.Tag = new TagExt(PermissionConstant.userParamter) { Name = "userParamter" };
                //客户机参数
                var customerMancheParamter = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("customerMancheParamter")//"客户机参数"
                    ,
                    Expanded = false
                };
                customerMancheParamter.Tag = new TagExt(PermissionConstant.customerMancheParamter) { Name = "customerMancheParamter" };
                //吊挂线
                var hangUpLine = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("hangUpLine")//"吊挂线"
                    ,
                    Expanded = false
                };
                hangUpLine.Tag = new TagExt(PermissionConstant.hangUpLine) { Name = "hangUpLine" };
                //系统
                var system = new AccordionControlElement(ElementStyle.Item)
                {
                    Text = LanguageAction.Instance.BindLanguageTxt("system")//"系统"
                    ,
                    Expanded = false
                };
                system.Tag = new TagExt(PermissionConstant.system) { Name = "system" };

                systemMsg.Elements.Add(userParamter);
                systemMsg.Elements.Add(customerMancheParamter);
                systemMsg.Elements.Add(hangUpLine);
                systemMsg.Elements.Add(system);
                //var processFlowHangingPiece = new AccordionControlElement(ElementStyle.Item) { Text = "工序挂衣架" };
                //var tcpTest = new AccordionControlElement(ElementStyle.Item) { Text = "Tcp测试" };
                //tcpTest.Tag = new TagExt(PermissionConstant.ProductionLine_TcpTest);

                realTimeInfo.Elements.Add(controlSet);
                realTimeInfo.Elements.Add(pipelineMsg);
                realTimeInfo.Elements.Add(bridgingSet);
                realTimeInfo.Elements.Add(clientInfo);
                realTimeInfo.Elements.Add(systemMsg);

                // realTimeInfo.Elements.Add(processFlowHangingPiece);
                // realTimeInfo.Elements.Add(tcpTest);

                controlSet.Click += ControlSet_Click;
                pipelineMsg.Click += PipelineMsg_Click;
                bridgingSet.Click += BridgingSet_Click;
                clientInfo.Click += ClientInfo_Click;
                system.Click += SystemMsg_Click;
                hangUpLine.Click += HangUpLine_Click;
                userParamter.Click += UserParamter_Click;
                customerMancheParamter.Click += CustomerMancheParamter_Click;
                //processFlowHangingPiece.Click += ProcessFlowHangingPiece_Click;
                //   tcpTest.Click += TcpTest_Click;
                xtraUserControl11.LeftAccordionControl.Elements.Add(realTimeInfo);
                xtraUserControl11.LeftAccordionControl.Refresh();
                // LanguageAction.Instance.ChangeLanguage(this.Controls);
                ChangeLanguageAction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void CustomerMancheParamter_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text; //"客户机参数";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"客户机参数"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ClientMancheParamterSet(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UserParamter_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"用户参数";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"用户参数"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new UserParamterSet(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void HangUpLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"吊挂线";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"吊挂线"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new HangUpLineSet(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ProcessFlowHangingPiece_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"工序挂衣架";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"工序挂衣架"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ProcessFlowHangingPiece(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void TcpTest_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //var tcpConnected = new TcpConected();
                //tcpConnected.ShowDialog();
                //if (tcpConnected.IsConnect)
                //{
                //    ProcessFlowHangingPiece_Click(sender, e);
                //    XtraTabPage tab = new SusXtraTabPage();
                //    tab.Text = "Tcp测试";
                //    tab.Name = string.Format("{0}", "Tcp测试");
                //    XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new TcpTestMain(xtraUserControl11));
                //}
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = "Tcp测试";
                tab.Name = string.Format("{0}", "Tcp测试");
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new TcpTestMain(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SystemMsg_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;// "系统参数";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"系统参数"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new SystemMsg(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ClientInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text; //"客户机信息";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"客户机信息"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new ClientInfo(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BridgingSet_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"桥接配置";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"桥接配置"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new BridgeSetIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PipelineMsg_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((AccordionControlElement)sender)?.Tag };
                tab.Text = (sender as AccordionControlElement).Text;//"流水线管理";
                tab.Name = string.Format("{0}", (sender as AccordionControlElement).Text//"流水线管理"
                    );
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new PipelineIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ControlSet_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var cConfiguration = new ControlConfiguration();
                cConfiguration.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //启动主轨
        private void barBtn_RunMainTrack_ItemClick(object sender, ItemClickEventArgs e)
        {
            string errMsg = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var groupNo = CurrentUser.Instance.CurrentSiteGroup?.GroupNo;
                var listMainTrackNumber = SuspeRemotingService.statingService.GetMainTrackNumberList(groupNo);
                foreach (var mn in listMainTrackNumber)
                {
                    var hexMainTrackNumber = HexHelper.TenToHexString2Len(mn.MainTrackNumber.Value);
                    bool sucess = SuspeRemotingService.mainTrackService.StartMainTrack(groupNo, hexMainTrackNumber, ref errMsg);
                    if (!sucess)
                    {
                        MessageBox.Show(string.Format("启动失败!失败原因:【{0}】", errMsg));
                        return;
                    }
                }

                MessageBox.Show("启动指令发送成功!");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //停止主轨
        private void bbarItemStopMainTrack_ItemClick(object sender, ItemClickEventArgs e)
        {
            string errMsg = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var groupNo = CurrentUser.Instance.CurrentSiteGroup?.GroupNo;
                var listMainTrackNumber = SuspeRemotingService.statingService.GetMainTrackNumberList(groupNo);
                foreach (var mn in listMainTrackNumber)
                {
                    var hexMainTrackNumber = HexHelper.TenToHexString2Len(mn.MainTrackNumber.Value);
                    bool sucess = SuspeRemotingService.mainTrackService.StopMainTrack(groupNo, hexMainTrackNumber, ref errMsg);
                    if (!sucess)
                    {
                        MessageBox.Show(string.Format("停止失败!失败原因:【{0}】", errMsg));
                        return;
                    }
                }
                MessageBox.Show("停止指令发送成功!");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //急停主轨
        private void barBtn_EmergencyStopMainTrack_ItemClick(object sender, ItemClickEventArgs e)
        {
            string errMsg = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var groupNo = CurrentUser.Instance.CurrentSiteGroup?.GroupNo;
                var listMainTrackNumber = SuspeRemotingService.statingService.GetMainTrackNumberList(groupNo);
                foreach (var mn in listMainTrackNumber)
                {
                    var hexMainTrackNumber = HexHelper.TenToHexString2Len(mn.MainTrackNumber.Value);
                    bool sucess = SuspeRemotingService.mainTrackService.EmergencyStopMainTrack(groupNo, hexMainTrackNumber, ref errMsg);
                    if (!sucess)
                    {
                        MessageBox.Show(string.Format("急停失败!失败原因:【{0}】", errMsg));
                        return;
                    }
                }
                MessageBox.Show("急停指令发送成功!");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        bool closeTag = false;
        private void Index_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Index_FormClosing(object sender, FormClosingEventArgs e)
        {

            var dr = XtraMessageBox.Show(
                LanguageAction.Instance.BindLanguageTxt("promptExitSystem"), LanguageAction.Instance.BindLanguageTxt("promptTips"),
                //"确认退出系统吗?", "温馨提示", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                new frmLogin().Show();
                closeTag = true;

            }
            //closeTag = false;
            if (!closeTag)
            {
                e.Cancel = true;
            }
        }

        private void repositoryItemZoomTrackBar2_EditValueChanged(object sender, EventArgs e)
        {

            var ztbc = sender as DevExpress.XtraEditors.ZoomTrackBarControl;

            bbItemRefsh.Caption = string.Format("{0}秒", ztbc.EditValue);
        }

        /// <summary>
        /// 锁定屏幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbItemLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            var lockScreen = new LockScreen(this);
            lockScreen.ShowDialog();
        }

        //切换用户
        private void bbItemChangeUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var changeUser = new frmLogin();
            //changeUser.ShowDialog();

            ChangeUser change = new ChangeUser(this);
            var dialogResult = change.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.Index_Load(null, null);
            }
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAuthorization_ItemClick(object sender, ItemClickEventArgs e)
        {
            var companyName = new SuspeSys.Client.Action.Common.CommonAction().GetApplicationProfileByName(ApplicationProfileEnum.CustomerName);
            var clientName = BasicInfoRepository.Instance.GetByName(Sqlite.Entity.BasicInfoEnum.DefaultClient);

            Modules.AuthorizationManagement.RegIndex form = new Modules.AuthorizationManagement.RegIndex(companyName, clientName);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        //设置刷新频率
        private void barEditItem6_EditValueChanged(object sender, EventArgs e)
        {
            //barEditItem6.EditValue
            CurrentUser.Instance.RefreshFrequency = int.Parse(barEditItem6.EditValue.ToString());
            CurrentUser.Instance.Timer.Interval = CurrentUser.Instance.RefreshFrequency * 1000;

        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            string wordPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "系统操作说明.docx");
            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "系统操作说明.pdf");

            if (File.Exists(pdfPath))
            {
                System.Diagnostics.Process.Start(pdfPath);
                return;
            }

            if (File.Exists(wordPath))
            {
                System.Diagnostics.Process.Start(wordPath);
                return;
            }

            XtraMessageBox.Show(LanguageAction.Instance.BindLanguageTxt("promptNoProductApi"));//"目录中没有产品文档，请联系管理员");
        }
        //看板配置
        private void barBtn_DisplayBoardConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((DevExpress.XtraBars.BarButtonItem)e.Item)?.Tag };
                tab.Text = (e.Item as DevExpress.XtraBars.BarButtonItem).Caption;//"看板配置";
                tab.Name = string.Format("{0}", (e.Item as DevExpress.XtraBars.BarButtonItem).Caption//"看板配置"
                    );
                tab.Tag = "LEDKanbanConfiguration";
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new LEDBoardSet(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //小时计划
        private void barBtn_HourlyPlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage() { Tag = ((DevExpress.XtraBars.BarButtonItem)e.Item)?.Tag };
                tab.Text = (e.Item as DevExpress.XtraBars.BarButtonItem).Caption;//"小时计划";
                tab.Name = string.Format("{0}", (e.Item as DevExpress.XtraBars.BarButtonItem).Caption//"小时计划"
                    );
                tab.Tag = "HourlyPlan";
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new HourlyPlan(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonChangePwd_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangePwd change = new ChangePwd();
            change.ShowDialog();
        }

        #region 多语言
        ResourceManager rm = null;
        void InitLanguage()
        {
            try
            {

                SusTransitionManager.StartTransition(this, "loading....");
                rLanguageItemComboList.Items.AddRange(new object[] { SusLanguageCons.SimplifiedChinese, SusLanguageCons.TraditionalChinese, SusLanguageCons.English, SusLanguageCons.Cambodia, SusLanguageCons.Vietnamese });
                //rLanguageItemComboList
                barEditItem2.EditValue = SusLanguageCons.CurrentLanguageTxt;
                ChangeLanguage(SusLanguageCons.CurrentLanguageTxt);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
            //rm = new ResourceManager("SuspeSys.Client.Language.Resource", Assembly.GetExecutingAssembly());
            //var ci = Thread.CurrentThread.CurrentCulture;
            //var key = rm.GetString("MenuProcessOrder", ci);
            //btnHelp.Caption = key;

            //barBtnRealName.Caption = Domain.Common.CurrentUser.Instance.EmployeeName;
            //barBtnUserName.Caption = Domain.Common.CurrentUser.Instance.UserName;
            //if (CurrentUser.Instance.IsAuthorization)
            //    barBtnClientMachine.Caption = "客户机：" + Domain.Common.CurrentUser.Instance.CurrentClientMachines?.ClientMachineName;
            //else
            //    barBtnClientMachine.Caption = "软件未授权";

            //barButtonMachineType.Caption = "客户机类型：" + Domain.Common.CurrentUser.Instance.CurrentClientTypeName;
        }
        //语言切换
        private void rLanguageItemComboList_SelectedValueChanged(object sender, EventArgs e)
        {
            var selComb = sender as ComboBoxEdit;
            var language = selComb.Text;
            var oldLanguage = selComb.OldEditValue;

            //if ("zh-CHS".Equals(language))
            //{
            //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CHS");
            //    barButtonItem2.Caption = rm.GetString("MenuProcessOrder", Thread.CurrentThread.CurrentCulture); ;
            //}
            //else
            //{
            //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //    barButtonItem2.Caption = rm.GetString("MenuProcessOrder", Thread.CurrentThread.CurrentCulture); ;
            //}

            //var cultureInfo = new System.Globalization.CultureInfo(language);
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;


            //var culture = CultureInfo.CreateSpecificCulture(language);

            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;

            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;

            //btnHelp.Caption = rm.GetString("MenuProcessOrder", culture);
            //ComponentResourceManager resources = new ComponentResourceManager(typeof(frmTest3));
            //resources.ApplyResources(myButton, "myButton");
            //resources.ApplyResources(this, "$this");

            //Thread.CurrentThread.de
            //this.Refresh();
            //labelControl1.Text=
            ChangeLanguage(language);
            var rf = ribbon;
            var lTxt = LanguageAction.Instance.BindLanguageTxt(rf.Name);
            if (!string.IsNullOrEmpty(lTxt))
                rf.ApplicationCaption = lTxt;
        }
        void ChangeLanguage(string language, string oldLanguage = null)
        {
            try
            {
                SusTransitionManager.StartTransition(this, "loading....");
                //var culture = CultureInfo.CreateSpecificCulture(language);
                //CultureInfo.DefaultThreadCurrentCulture = culture;
                //CultureInfo.DefaultThreadCurrentUICulture = culture;
                //Thread.CurrentThread.CurrentCulture = culture;
                //Thread.CurrentThread.CurrentUICulture = culture;
                //btnHelp.Caption = rm.GetString("MenuProcessOrder", culture); ;
                switch (language)
                {
                    case "中文(简体)":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.SimplifiedChinese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "中文(繁体)":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.TraditionalChinese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "English":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.English;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "Cambodia":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.Cambodia;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                    case "Vietnamese":
                        SusLanguageCons.CurrentLanguage = SusLanguageCons.Vietnamese;
                        SusLanguageCons.LastLanguage = oldLanguage;
                        break;
                }
                LanguageAction.Instance.ChangeLanguage(this.Controls);
                this.Refresh();
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }

        }
        void ChangeLanguageAction()
        {
            try
            {
                SusTransitionManager.StartTransition(this, "loading....");
                LanguageAction.Instance.ChangeLanguage(this.Controls);

            }
            finally
            {
                SusTransitionManager.EndTransition();
            }

        }
        private void barSubItemChinese_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeLanguage(e.Item.Tag.ToString());
        }

        private void barSubItemEnglish_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeLanguage(e.Item.Tag.ToString());

        }
        private void barButtonItemManyLanguage_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    XtraTabPage tab = new SusXtraTabPage();
            //    tab.Text = "多语言管理";
            //    tab.Tag = "MultLanguageMsg";
            //    tab.Name = string.Format("{0}", "多语言管理");
            //    tab.Tag = new TagExt(PermissionConstant.MultLanguageMsg) { Name = "MultLanguageMsg" };
            //    XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new MulLanguageIndex(xtraUserControl11));
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "错误");
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageAction.Instance.ChangeLanguage(this.Controls);
        }

        private void barBtn_Language_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();
                tab.Text = LanguageAction.Instance.BindLanguageTxt("menuMultLanguageMsg");//"多语言管理";
                tab.Tag = "MultLanguageMsg";
                tab.Name = string.Format("{0}", LanguageAction.Instance.BindLanguageTxt("menuMultLanguageMsg")//"多语言管理"
                    );
                tab.Tag = new TagExt(PermissionConstant.MultLanguageMsg) { Name = "MultLanguageMsg" };
                XtraTabPageHelper.AddTabPage(xtraUserControl11.MainTabControl, tab, new MulLanguageIndex(xtraUserControl11));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}