
using Sus.Net.Common.Utils;
using SusNet.Common.Utils;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.Support;
using SuspeSys.Support.Enums;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.TestDataGeneratorTool
{
    public class Tools
    {
        static Tools()
        {
            SuspeApplication.Init();
        }

        public void Init()
        {
            SuspeApplication.Init();
        }

        Random rdom = new Random(100);
       
        public void GeneratorData()
        {
            var total = 5;
            var customerService = new CommonServiceImpl<SuspeSys.Domain.Customer>();
            for (var i = 0; i < total; i++)
            {
                SuspeSys.Domain.Customer cus = new Domain.Customer();
                //cus.Id = Guid.NewGuid().ToString().Substring(0, 32);
                cus.CusNo = "c0001" + i;
                cus.CusNo = "c0001" + i;
                cus.CusName = "测试客户" + i;
                cus.Address = "北京市朝阳区民族家园" + i;
                cus.Tel = "1831132901" + i;
                cus.LinkMan = "lucifer" + i;

                customerService.Save(cus);

                Console.WriteLine("客户测试数据生成");
            }
            var styleList = new List<string>() { "圆领T恤", "风衣", "连衣裙", "卫衣", "夹克", "西装", "衬衫" };
            var styleService = new CommonServiceImpl<SuspeSys.Domain.Style>();
            for (var i = 0; i < styleList.Count; i++)
            {
                var style = new DaoModel.Style();
                style.StyleNo = "s000" + i;
                style.StyleName = styleList[i];
                style.Rmark = styleList[i];
                styleService.Save(style);
            }
            Console.WriteLine("款式测试数据生成");

            var sizeService = new CommonServiceImpl<DaoModel.PSize>();
            var sizeList = new List<string>() { "S", "M", "L", "XL", "XXL" };
            for (var i = 0; i < sizeList.Count; i++)
            {
                var size = new DaoModel.PSize();
                size.PsNo = "s001" + i;
                size.Size = sizeList[i];
                size.SizeDesption = sizeList[i] + "Size备注" + i;
                sizeService.Save(size);
            }
            Console.WriteLine("尺寸测试数据生成");

            var colorService = new CommonServiceImpl<DaoModel.PoColor>();
            var colorList = new List<string>() { "red", "white", "black", "green", "yellow", "blue" };
            for (var i = 0; i < colorList.Count; i++)
            {
                var color = new DaoModel.PoColor();
                color.SNo = "s001" + i;
                color.ColorValue = colorList[i];
                color.ColorDescption = colorList[i] + "备注" + i;
                colorService.Save(color);
            }
            Console.WriteLine("颜色测试数据生成");

            var basiscProcessFlowSerivce = new CommonServiceImpl<DaoModel.BasicProcessFlow>();
            var basicProcessFlowList = new List<string>() { "挂片", "合肩", "上袖子", "合身", "上领", "坎下摆", "坎袖口", "品检" };
            for (var i = 0; i < basicProcessFlowList.Count; i++)
            {
                var basicProcessFlow = new DaoModel.BasicProcessFlow();
                basicProcessFlow.DefaultFlowNo = i++;
                basicProcessFlow.ProcessCode = (i + 1).ToString();
                basicProcessFlow.ProcessName = basicProcessFlowList[i];
                basicProcessFlow.Sam = "12" + i;
                basicProcessFlow.StandardPrice = (decimal)22.5 + i;
                basicProcessFlow.PrcocessRmark = basicProcessFlowList[i] + "工艺指导说明";
                basiscProcessFlowSerivce.Save(basicProcessFlow);
            }
            Console.WriteLine("颜色测试数据生成");




            var processFlowSectionService = new CommonServiceImpl<DaoModel.ProcessFlowSection>();
            var sectionList = new List<string>() { "裁床", "线外", "吊挂", "缝制", "后整", "仓储" };

            //var styleService = new CommonServiceImpl<SuspeSys.Domain.Style>();

            for (var i = 0; i < sectionList.Count; i++)
            {
                var processFlowSection = new DaoModel.ProcessFlowSection();
                //processFlowSection.Style = st;
                processFlowSection.ProSectionCode = "psc00" + i;
                processFlowSection.ProSectionName = sectionList[i];
                processFlowSection.ProSectionNo = i.ToString();
                processFlowSectionService.Save(processFlowSection);
            }
            Console.WriteLine("工序段测试数据生成");
            //var index = 1;
            //foreach (var st in styleService.GetList())
            //{

            //}
            TestGeneratorLanguage();
            TestGeneratorStatingDirection();

            TestGeneratorProdType();
            GeneratorStyleDataData();
            GenerantorSatingInfo();
            //GeneratorMainTrack();
            GeneratorOhterDataInfo();
            GeneratorIds();

            TeatGeneratorUserInfo();
            GeneratorEmployeeInfo();
            TestGeneratorModuleInfo();
            //TestClientMachines();
            //生成系统参数
            TestGeneratorSystemParamter();
            Console.WriteLine("测试数据生成完成!!!!!!");


        }
       
        public void GeneratorStyleDataData()
        {
            var styleProcessFlowSectionItemService = new CommonServiceImpl<DaoModel.StyleProcessFlowSectionItem>();
            var basicProcessFlowService = new CommonServiceImpl<DaoModel.BasicProcessFlow>();
            var processFlowSectionService = new CommonServiceImpl<DaoModel.ProcessFlowSection>();
            var basicProcessFlowList = basicProcessFlowService.GetList();
            var basiscProcessFlowSerivce = new CommonServiceImpl<DaoModel.BasicProcessFlow>();

            //给工序段追加工序明细
            //var pfSectionList = processFlowSectionService.GetList();

            var processFlowSectionList = processFlowSectionService.GetList();
            var basicFlowList = basiscProcessFlowSerivce.GetList();

            //生成款式工序段
            var index = 0;
            var styleService = new CommonServiceImpl<SuspeSys.Domain.Style>();
            foreach (var st in styleService.GetList())
            {
                // var processFlowSection = new DaoModel.ProcessFlowSection();
                //// processFlowSection.Style = st;
                // processFlowSection.ProSectionCode = "psc00"+index;
                // processFlowSection.ProSectionName = "工序段" + index;
                // processFlowSection.ProSectionNo = index.ToString();
                // if ((index++) == 5) {
                //     break;
                // }
                // processFlowSectionService.Save(processFlowSection);

                var i = 1;
                foreach (var pf in basicProcessFlowList)
                {

                    // if (i % 3 == 0) break;
                    var pfsItem = new DaoModel.StyleProcessFlowSectionItem();
                    pfsItem.BasicProcessFlow = null;
                    pfsItem.Style = st;
                    if (i >= processFlowSectionList.Count)
                    {
                        pfsItem.ProcessFlowSection = processFlowSectionList[0];
                    }
                    else
                    {
                        pfsItem.ProcessFlowSection = processFlowSectionList[i];
                    }
                    if (i >= basicFlowList.Count)
                    {
                        pfsItem.FlowNo = index++;
                        pfsItem.BasicProcessFlow = basicFlowList[0];
                        pfsItem.ProcessCode = basicFlowList[0].ProcessCode;
                        pfsItem.ProcessName = basicFlowList[0].ProcessName;
                        pfsItem.PrcocessRmark = basicFlowList[0].PrcocessRmark;
                        pfsItem.Sam = basicFlowList[0].Sam;
                        pfsItem.SortNo = basicFlowList[0].SortNo;
                        pfsItem.StanardHours = basicFlowList[0].StanardHours;
                        pfsItem.StandardPrice = basicFlowList[0].StandardPrice;
                    }
                    else
                    {
                        pfsItem.FlowNo = index++;
                        pfsItem.BasicProcessFlow = basicFlowList[i];
                        pfsItem.ProcessCode = basicFlowList[i].ProcessCode;
                        pfsItem.ProcessName = basicFlowList[i].ProcessName;
                        pfsItem.PrcocessRmark = basicFlowList[i].PrcocessRmark;
                        pfsItem.Sam = basicFlowList[i].Sam;
                        pfsItem.SortNo = basicFlowList[i].SortNo;
                        pfsItem.StanardHours = basicFlowList[i].StanardHours;
                        pfsItem.StandardPrice = basicFlowList[i].StandardPrice;
                    }
                    styleProcessFlowSectionItemService.Save(pfsItem);
                }
                i++;
            }
        }
       
        public void GenerantorSatingInfo()
        {
            var roles = new List<string>() { "车缝站", "多功能站", "储存站", "专用返工站", "挂片站", "超载站", "QC站", "桥接专用" };

            //var StatingRolesService = new CommonServiceImpl<DaoModel.StatingRoles>();
            //var rCode = "r0";
            //var rIndex = 1;
            //foreach (var r in roles)
            //{
            //    var sro = new DaoModel.StatingRoles();
            //    sro.RoleCode = string.Format(rCode+rIndex++);
            //    sro.RoleName = r;
            //    StatingRolesService.Save(sro);
            //}
            var factorySerivce = new CommonServiceImpl<Factory>();
            var facory = new Factory();
            facory.FacCode = "001";
            facory.FacName = "工厂1";
            factorySerivce.Save(facory);

            var workshopService = new CommonServiceImpl<Workshop>();
            var workshop = new Workshop();
            workshop.WorCode = "ws01";
            workshop.WorName = "车间1";
            workshop.Factory = facory;
            workshopService.Save(workshop);

            var total = 5;
            var siteGroupService = new CommonServiceImpl<DaoModel.SiteGroup>();
            for (var i = 0; i < total; i++)
            {
                var siteGroup = new DaoModel.SiteGroup();
                siteGroup.Workshop = workshop;
                siteGroup.WorkshopCode = workshop.WorCode;
                siteGroup.FactoryCode = facory.FacCode;
                siteGroup.GroupNo = "10" + i;
                siteGroup.GroupName = "10" + i + "组";
                siteGroup.MainTrackNumber = Convert.ToInt16(i + 1);
                siteGroupService.Save(siteGroup);
            }

            var statingService = new CommonServiceImpl<DaoModel.Stating>();
            var statingRolesService = new CommonServiceImpl<DaoModel.StatingRoles>();

            for (var index = 0; index < roles.Count; index++)
            {
                var statingRole = new DaoModel.StatingRoles();
                statingRole.RoleCode = "10" + index;
                statingRole.RoleName = roles[index];
                statingRolesService.Save(statingRole);
            }

            var languageeService = new CommonServiceImpl<DaoModel.SusLanguage>();
            var statlanguage = languageeService.GetList().Where(f => f.LanguageKey.Equals("zh-CHS")).SingleOrDefault();
            var statingDirectionService = new CommonServiceImpl<DaoModel.StatingDirection>();
            var statingDirectionList = statingDirectionService.GetList();

            var statingList = new List<string>() { "站1001", "站1002", "站1003", "站1004" };
            var groupList = siteGroupService.GetList();
            var catRandom = new Random(100);
            var rolesList = statingRolesService.GetList();
            var rolesIndexRandom = new Random();
            // var k = 0;
            var statingNoIndex = 1;
            foreach (var gr in groupList)
            {
                for (var i = 0; i < statingList.Count; i++)
                {
                    var rs = rolesList[rolesIndexRandom.Next(0, rolesList.Count - 1)];
                    var stating = new DaoModel.Stating();
                    stating.SiteGroup = gr;
                    stating.IsEnabled = true;
                    stating.IsLoadMonitor = true;
                    stating.IsPromoteTripCachingFull = true;
                    stating.IsChainHoist = false;
                    stating.StatingName = rs.RoleName;
                    stating.StatingNo = (statingNoIndex++).ToString();
                    stating.MainTrackNumber = gr.MainTrackNumber;
                    stating.Capacity = catRandom.Next();
                    stating.StatingRoles = rs;//rolesList[rolesIndexRandom.Next(0, rolesList.Count - 1)];
                    stating.SusLanguage = statlanguage;
                    stating.Language = statlanguage?.LanguageKey?.Trim();
                    stating.StatingDirection = statingDirectionList[0];

                    statingService.Save(stating);

                }
            }

            //生产线数据生成
            var PipeliningService = new CommonServiceImpl<DaoModel.Pipelining>();
            var SiteGroupService = new CommonServiceImpl<DaoModel.SiteGroup>();
            var siteGroupList = siteGroupService.GetAllList();
            var index1 = 1;
            foreach (var sg in siteGroupList)
            {
                var p = new DaoModel.Pipelining();
                p.SiteGroup = sg;
                p.PipeliNo = index1.ToString();
                p.PushRodNum = 10 + index1;
                PipeliningService.Save(p);
                index1++;
            }
        }

       
        public void GeneratorMainTrack()
        {
            var mainTrackService = new CommonServiceImpl<DaoModel.MainTrack>();
            var groups = new int[] { 100, 101, 102, 103, 104 };
            var mainNums = new short[] { 1, 2, 3, 4 };
            foreach (var g in groups)
            {
                foreach (var m in mainNums)
                {
                    var mainTrack = new MainTrack() { GroupNo = g.ToString(), Num = m, Status = 5 };
                    mainTrackService.Save(mainTrack);
                }
            }
        }
       
        public void GeneratorOhterDataInfo()
        {
            //"长短袖","漏打枣","口袋错位","内缝跳线","缝型错误","止口错误","底层暴露"
            //疵点名称
            var defectCodeTableService = new CommonServiceImpl<DaoModel.DefectCodeTable>();
            var defectList = new List<string>() { "长短袖", "漏打枣", "口袋错位", "内缝跳线", "缝型错误", "止口错误", "底层暴露" };
            for (var index = 0; index < defectList.Count; index++)
            {
                var dc = new DaoModel.DefectCodeTable();
                dc.DefectCode = (index + 1).ToString();
                dc.DefectName = defectList[index];
                dc.DefectNo = index.ToString();
                defectCodeTableService.Save(dc);
            }
            var lackMaterialsTableService = new CommonServiceImpl<DaoModel.LackMaterialsTable>();
            var lmList = new List<string>() { "面料", "辅料", "包装材料" };
            for (var index = 0; index < lmList.Count; index++)
            {
                var dc = new DaoModel.LackMaterialsTable();

                dc.LackMaterialsName = lmList[index];
                dc.LackMaterialsCode = index.ToString();
                lackMaterialsTableService.Save(dc);
            }


        }
       
        public void GeneratorEmployeeOrMembercardInfo()
        {
            var index = 1;
            var employeeList = new List<string>() { "张三00", "张三11", "张三22", "张三33" };
            foreach (var em in employeeList)
            {

                var employee = new DaoModel.Employee()
                {
                    Email = string.Format("zhangsan{0}@163.com", index++),
                    Code = "00" + index++,
                    StartingDate = DateTime.Now,
                    RealName = "zhangsan" + index++
                };
                EmployeeDao.Instance.Save(employee);
            }
            var cardList = new List<string>() { "89347733", "89347734", "89347735", "89347736" };
            foreach (var card in cardList)
            {
                var cInfo = new CardInfo() { CardDescription = "员工卡", CardNo = card, CardType = 4, IsEnabled = true, IsMultiLogin = true };
                CardInfoDao.Instance.Save(cInfo);
            }
            var employeeService = new CommonServiceImpl<DaoModel.Employee>();
            var eList = employeeService.GetAllList();
            var cardInfoService = new CommonServiceImpl<DaoModel.CardInfo>();
            var cardInfoList = cardInfoService.GetAllList();
            for (var i = 0; i < eList.Count; i++)
            {
                var ecRelation = new EmployeeCardRelation();
                ecRelation.CardInfo = cardInfoList[i];
                ecRelation.Employee = eList[i];
                ecRelation.Employee.CardNo = cardInfoList[i].CardNo;
                EmployeeCardRelationDao.Instance.Save(ecRelation);
                EmployeeDao.Instance.Update(ecRelation.Employee);
            }
        }
       
        public void GeneratorIds()
        {
            var idGeneratorService = new CommonServiceImpl<DaoModel.IdGenerator>();
            var processOrderModel = new DaoModel.IdGenerator();
            processOrderModel.BeginValue = 1;
            processOrderModel.EndValue = 99999;
            processOrderModel.CurrentValue = 0;
            processOrderModel.FlagNo = "ProcessOrder";
            idGeneratorService.Save(processOrderModel);

            var flowVersionModel = new DaoModel.IdGenerator();
            flowVersionModel.BeginValue = 1;
            flowVersionModel.EndValue = 99999;
            flowVersionModel.CurrentValue = 0;
            flowVersionModel.FlagNo = "ProcessFlowVersion";
            idGeneratorService.Save(flowVersionModel);

            var processFlowChartModel = new DaoModel.IdGenerator();
            processFlowChartModel.BeginValue = 1;
            processFlowChartModel.EndValue = 99999;
            processFlowChartModel.CurrentValue = 0;
            processFlowChartModel.FlagNo = "ProcessFlowChart";
            idGeneratorService.Save(processFlowChartModel);

            GeneratorIds2();
        }
       
        public void GeneratorIds2()
        {
            var idGeneratorService = new CommonServiceImpl<DaoModel.IdGenerator>();
            var Products = new DaoModel.IdGenerator();
            Products.BeginValue = 1;
            Products.EndValue = 255;
            Products.CurrentValue = 0;
            Products.FlagNo = "Products";
            idGeneratorService.Save(Products);
        }
       
        public void GeneratorEmployeeInfo()
        {
            var employeeService = new CommonServiceImpl<Employee>();
            for (var index = 0; index < 5; index++)
            {
                var employee = new Employee();
                employee.Code = "00" + (index+1).ToString() ;
                employee.Address = "北京市" + index;
                employee.RealName = "张三" + index;
                employeeService.Save(employee);
            }
            var cardService = new CommonServiceImpl<CardInfo>();
            var cardIndex = 1003479932;
            for (var index = 0; index < 5; index++)
            {
                var cardInfo = new CardInfo();
                cardInfo.CardNo = (cardIndex+1).ToString();
                cardInfo.CardDescription = "员工卡";
                cardInfo.CardType = 4;
                cardInfo.IsEnabled = true;
                cardService.Save(cardInfo);
            }
            var employeeCardRelationService = new CommonServiceImpl<EmployeeCardRelation>();
            var employeeList = employeeService.GetAllList();
            var cardList = cardService.GetAllList();
            for (var index = 0; index < 5; index++)
            {
                var employeeCardInfo = new EmployeeCardRelation();
                employeeCardInfo.Employee = employeeList[index];
                employeeCardInfo.CardInfo = cardList[index];
                employeeCardRelationService.Save(employeeCardInfo);
                EmployeeDao.Instance.Update(employeeCardInfo.Employee);
            }



        }
       
        public void TestGeneratorNo()
        {
            var ids = IdGeneratorSupport.Instance.NextId(typeof(DaoModel.ProcessFlowVersion));

        }
       
        public void TestGeneratorProdType()
        {
            var prodTypeService = new CommonServiceImpl<DaoModel.ProdType>();
            var prodTypeList = new List<string>() { "分炼线", "吊挂线", "仓储线" };
            for (var index = 0; index < prodTypeList.Count; index++)
            {
                var dc = new DaoModel.ProdType();
                dc.PorTypeCode = "10" + index;
                dc.PorTypeName = prodTypeList[index];
                prodTypeService.Save(dc);
            }
        }
        public void TestGeneratorLanguage()
        {
            var languageeService = new CommonServiceImpl<DaoModel.SusLanguage>();
            languageeService.Save(new Domain.SusLanguage()
            {
                LanguageKey = "zh-CHS",
                LanguageValue = "中文"
            });
            languageeService.Save(new Domain.SusLanguage()
            {
                LanguageKey = "en-US",
                LanguageValue = "English"
            });
        }
        public void TestGeneratorStatingDirection()
        {
            var languageeService = new CommonServiceImpl<DaoModel.StatingDirection>();
            languageeService.Save(new Domain.StatingDirection()
            {
                DirectionKey = "1",
                DirectionDesc = "出站,入站"
            });
            languageeService.Save(new Domain.StatingDirection()
            {
                DirectionKey = "2",
                DirectionDesc = "入站"
            });
            languageeService.Save(new Domain.StatingDirection()
            {
                DirectionKey = "2",
                DirectionDesc = "出战"
            });
        }

       
        public void TeatGeneratorUserInfo()
        {

            //添加超级管理员,管理员角色
            var roleService = new CommonServiceImpl<DaoModel.Roles>();

            var list = roleService.GetAllList();
            if (list != null && list.Count > 0)
                return;

            var superAdmin = new DaoModel.Roles()
            {
                ActionName = Support.Enums.Constant.Role_SuperAdmin,
                Description = "超级管理员",
            };

            roleService.Save(superAdmin);

            var employeeService = new CommonServiceImpl<Domain.Employee>();

            // 创建Employee
            var employee = new Employee() { Code = Guid.NewGuid().ToString().Substring(0, 6), RealName = "系统用户" };
            employeeService.Save(employee);



            var service = new CommonServiceImpl<DaoModel.Users>();
            var userRoleService = new CommonServiceImpl<DaoModel.UserRoles>();

            DaoModel.Users user = new DaoModel.Users();
            user.CardNo = "123456789";
            user.UserName = "admin";
            user.Password = SuspeSys.Utils.Security.MD5.Encrypt("admin");
            user.Employee = employee;

            service.Save(user);
            userRoleService.Save(new DaoModel.UserRoles()
            {
                Users = user,
                Roles = superAdmin
            });



            roleService.Save(new DaoModel.Roles()
            {
                ActionName = Support.Enums.Constant.Role_Admin,
                Description = "管理员",
            });

        }

       
        public void TestClientMachines()
        {
            var service = new CommonServiceImpl<Domain.ClientMachines>();

            service.Save(new ClientMachines() { ClientMachineName = "client1", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Manage });
            service.Save(new ClientMachines() { ClientMachineName = "client2", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Manage });
            service.Save(new ClientMachines() { ClientMachineName = "client3", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Search });
            service.Save(new ClientMachines() { ClientMachineName = "client4", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Manage });
        }

       
        public void TestGeneratorAdminUser()
        {
            var tt = HexHelper.TenToHexString10Len(0);
            var st = System.Guid.NewGuid();
        }
       
        public void TestGeneratorSQL()
        {
            var wde = new WaitProcessOrderHanger();
            object k = null;
            var sql = SqlMappingUtils<WaitProcessOrderHanger>.Instance.GetInsertSql(wde, out k);
            Console.WriteLine(sql);
        }

        #region 系统参数


        public void TestGeneratorSystemParamter()
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();

            var temp = service.GetAllList();
            if (temp.Any())
                return;

            //用户参数
            var smp = new SystemModuleParameter();
            smp.ModuleType = SusModuleType.UserParamter.Value;
            smp.ModuleText = SusModuleType.UserParamter.Desption;
            smp.SysNo = StringUtils.ToFixLen4StringFormat("1");
            smp.IsEnabled = true;
            service.Save(smp);

            //客户机
            var smpClientManche = new SystemModuleParameter();
            smpClientManche.ModuleType = SusModuleType.CustomerMancheParamter.Value;
            smpClientManche.ModuleText = SusModuleType.CustomerMancheParamter.Desption;
            smpClientManche.SysNo = StringUtils.ToFixLen4StringFormat("2");
            smpClientManche.IsEnabled = true;
            service.Save(smpClientManche);

            #region//吊挂线
            var smpHangUpLine = new SystemModuleParameter();
            smpHangUpLine.ModuleType = SusModuleType.HangUpLine.Value;
            smpHangUpLine.ModuleText = SusModuleType.HangUpLine.Desption;
            smpHangUpLine.SysNo = StringUtils.ToFixLen4StringFormat("3");
            smpHangUpLine.IsEnabled = true;
            service.Save(smpHangUpLine);

            //吊挂线-->挂片站
            //var smpHangUpLineHangerPiece = new SystemModuleParameter();
            //smpHangUpLineHangerPiece.SystemModuleParameterId = smpHangUpLine.Id;
            //smpHangUpLineHangerPiece.ModuleType = SecondLevelModuleType.HangerPiece.Value;
            //smpHangUpLineHangerPiece.ModuleText = SecondLevelModuleType.HangerPiece.Desption;
            //smpHangUpLineHangerPiece.SysNo = StringUtils.ToFixLen4StringFormat("3");
            //smpHangUpLineHangerPiece.IsEnabled = true;
            //service.Save(smpHangUpLineHangerPiece);

            var smpHangUpLineHangerPieceHangerRegisterUse = new SystemModuleParameter();
            smpHangUpLineHangerPieceHangerRegisterUse.SystemModuleParameterId = smpHangUpLine.Id;
            smpHangUpLineHangerPieceHangerRegisterUse.SecondModuleType = SecondLevelModuleType.HangerPiece.Value;
            smpHangUpLineHangerPieceHangerRegisterUse.SecondModuleText = SecondLevelModuleType.HangerPiece.Desption;
            smpHangUpLineHangerPieceHangerRegisterUse.SysNo = StringUtils.ToFixLen4StringFormat("3");
            smpHangUpLineHangerPieceHangerRegisterUse.IsEnabled = true;
            service.Save(smpHangUpLineHangerPieceHangerRegisterUse);
            smpHangUpLineHangerPieceHangerSub(smpHangUpLineHangerPieceHangerRegisterUse);


            //吊挂线-->生产线
            var smpHangUpProductsLine = new SystemModuleParameter();
            smpHangUpProductsLine.SystemModuleParameterId = smpHangUpLine.Id;
            smpHangUpProductsLine.SecondModuleType = SecondLevelModuleType.ProductsLine.Value;
            smpHangUpProductsLine.SecondModuleText = SecondLevelModuleType.ProductsLine.Desption;
            smpHangUpProductsLine.SysNo = StringUtils.ToFixLen4StringFormat("3");
            smpHangUpProductsLine.IsEnabled = true;
            service.Save(smpHangUpProductsLine);
            SystemParameterHangUpProductsLineSub(smpHangUpProductsLine);


            //吊挂线-->其他
            var smpHangUpOther = new SystemModuleParameter();
            smpHangUpOther.SystemModuleParameterId = smpHangUpLine.Id;
            smpHangUpOther.SecondModuleType = SecondLevelModuleType.Other.Value;
            smpHangUpOther.SecondModuleText = SecondLevelModuleType.Other.Desption;
            smpHangUpOther.SysNo = StringUtils.ToFixLen4StringFormat("3");
            smpHangUpOther.IsEnabled = true;
            service.Save(smpHangUpOther);
            SystemParameterHangUpOtherSub(smpHangUpOther.Id, smpHangUpOther.SysNo);
            #endregion


            //系统
            var smpSystem = new SystemModuleParameter();
            smpSystem.ModuleType = SusModuleType.System.Value;
            smpSystem.ModuleText = SusModuleType.System.Desption;
            smpSystem.SysNo = StringUtils.ToFixLen4StringFormat("4");
            smpSystem.IsEnabled = true;
            service.Save(smpSystem);

            //系统-->考勤
            var smpSystemAttendance = new SystemModuleParameter();
            smpSystemAttendance.SecondModuleType = SecondLevelModuleType.Attendance.Value;
            smpSystemAttendance.SecondModuleText = SecondLevelModuleType.Attendance.Desption;
            smpSystemAttendance.SysNo = StringUtils.ToFixLen4StringFormat("4");
            smpSystemAttendance.IsEnabled = true;
            smpSystemAttendance.SystemModuleParameterId = smpSystem.Id;
            service.Save(smpSystemAttendance);
            smpSystemAttendanceSub(smpSystemAttendance.Id, smpSystemAttendance.SysNo);

            //系统-->生产
            var smpSystemAttendanceProduct = new SystemModuleParameter();
            smpSystemAttendanceProduct.SecondModuleType = SecondLevelModuleType.AttendanceProduct.Value;
            smpSystemAttendanceProduct.SecondModuleText = SecondLevelModuleType.AttendanceProduct.Desption;
            smpSystemAttendanceProduct.SysNo = StringUtils.ToFixLen4StringFormat("4");
            smpSystemAttendanceProduct.IsEnabled = true;
            smpSystemAttendanceProduct.SystemModuleParameterId = smpSystem.Id;
            service.Save(smpSystemAttendanceProduct);
            smpSystemAttendanceProductSub(smpSystemAttendanceProduct);

            //考勤-其他
            var smpSystemAttendanceOther = new SystemModuleParameter();
            smpSystemAttendanceOther.SecondModuleType = SecondLevelModuleType.AttendanceOther.Value;
            smpSystemAttendanceOther.SecondModuleText = SecondLevelModuleType.AttendanceOther.Desption;
            smpSystemAttendanceOther.SysNo = StringUtils.ToFixLen4StringFormat("4");
            smpSystemAttendanceOther.IsEnabled = true;
            smpSystemAttendanceOther.SystemModuleParameterId = smpSystem.Id;
            service.Save(smpSystemAttendanceOther);
            smpSystemAttendanceOtherSub(smpSystemAttendanceOther.Id, smpSystemAttendanceOther.SysNo);
        }

        private void smpSystemAttendanceOtherSub(string id, string sysNo)
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();

            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemAttendanceOther));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = id,
                    IsEnabled = true,
                    SysNo = sysNo,
                    ParamterKey = item.Key,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };

                index++;

                service.Save(model);
            }
        }

        private void smpSystemAttendanceProductSub(SystemModuleParameter Parameter)
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();
            var serviceDomain = new CommonServiceImpl<Domain.SystemModuleParameterDomain>();
            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemAttendanceProduct));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = Parameter.Id,
                    IsEnabled = true,
                    ParamterKey = item.Key,
                    SysNo = Parameter.SysNo,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };

                service.Save(model);
                if (item.Key == typeof(EffType).Name)
                {
                    var diclist = EnumHelper.GetDictionary2(typeof(EffType));
                    foreach (var dicItem in diclist)
                    {
                        var domain = new Domain.SystemModuleParameterDomain()
                        {
                            Name = dicItem.Value,
                            Code = dicItem.Key,
                            SystemModuleParameter = model,
                            Enable = true
                        };

                        serviceDomain.Save(domain);
                    }

                }

                if (item.Key == typeof(ProcessSiteMode).Name)
                {
                    var diclist = EnumHelper.GetDictionary2(typeof(ProcessSiteMode));
                    foreach (var dicItem in diclist)
                    {
                        var domain = new Domain.SystemModuleParameterDomain()
                        {
                            Name = dicItem.Value,
                            Code = dicItem.Key,
                            SystemModuleParameter = model,
                            Enable = true
                        };

                        serviceDomain.Save(domain);
                    }

                }

                index++;


            }
        }

        private void smpSystemAttendanceSub(string id, string sysNo)
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();

            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemParameterSystemAttendance));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = id,
                    IsEnabled = true,
                    SysNo = sysNo,
                    ParamterKey = item.Key,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };

                index++;

                service.Save(model);
            }
        }

        /// <summary>
        /// 吊挂线  挂片站 详细信息
        /// </summary>
        private void smpHangUpLineHangerPieceHangerSub(SystemModuleParameter parameter)
        {

            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();
            var serviceDomain = new CommonServiceImpl<Domain.SystemModuleParameterDomain>();

            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemParameterHangUpLineHanger));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = parameter.Id,
                    IsEnabled = true,
                    SysNo = parameter.SysNo,
                    ParamterKey = item.Key,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };

                service.Save(model);
                if (item.Key == typeof(ToOriginWay).Name)
                {
                    var diclist = EnumHelper.GetDictionary2(typeof(ToOriginWay));
                    foreach (var dicItem in diclist)
                    {
                        var domain = new Domain.SystemModuleParameterDomain()
                        {
                            Name = dicItem.Value,
                            Code = dicItem.Key,
                            SystemModuleParameter = model,
                            Enable = true
                        };

                        serviceDomain.Save(domain);
                    }

                }

                index++;


            }

        }

        private void SystemParameterHangUpProductsLineSub(SystemModuleParameter parameter)
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();
            var serviceDomain = new CommonServiceImpl<Domain.SystemModuleParameterDomain>();

            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemParameterHangUpProductsLine));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = parameter.Id,
                    IsEnabled = true,
                    SysNo = parameter.SysNo,
                    ParamterKey = item.Key,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };
                service.Save(model);
                if (item.Key == typeof(SeqInfoType).Name)
                {
                    var diclist = EnumHelper.GetDictionary2(typeof(SeqInfoType));
                    foreach (var dicItem in diclist)
                    {
                        var domain = new Domain.SystemModuleParameterDomain()
                        {
                            Name = dicItem.Value,
                            Code = dicItem.Key,
                            SystemModuleParameter = model,
                            Enable = true
                        };

                        serviceDomain.Save(domain);
                    }

                }

                index++;


            }
        }

        private void SystemParameterHangUpOtherSub(string id, string sysNo)
        {
            var service = new CommonServiceImpl<Domain.SystemModuleParameter>();

            var dic = SystemPrarmeterAttributeHelp.GetList(typeof(SystemParameterHangUpOther));
            int index = 0;
            foreach (var item in dic)
            {
                string indexChar = $" {((char)(index + 65))}. ";
                var model = new SystemModuleParameter
                {
                    SystemModuleParameterId = id,
                    IsEnabled = true,
                    SysNo = sysNo,
                    ParamterKey = item.Key,
                    ParamterControlType = item.ControlType,
                    ParamterControlTitle = indexChar + item.Description,
                    UpdateDateTime = DateTime.Now
                };

                index++;

                service.Save(model);
            }
        }
        #endregion

        #region 生成菜单数据

        public void TestGeneratorModuleInfo()
        {
            Dao.RolesModulesDao.Instance.DeleteByHql("from RolesModules");
            Dao.ModulesDao.Instance.DeleteByHql("from Modules");
            var moduleService = new CommonServiceImpl<DaoModel.Modules>();

            //创建根节点
            DaoModel.Modules root = new DaoModel.Modules()
            {
                ActionKey = "root",
                ActionName = "厦门悬挂系统",
                Deleted = 0,
                Description = "根目录",
                ModuleLevel = 0,
                ModulesType = (int)ModulesType.Menu,
                OrderField = 0,
                ModulesVal = null,
            };
            moduleService.Save(root);

            #region 启动主轨 停止主轨 急停主轨
            //启动主轨
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "barBtn_RunMainTrack",
                ActionName = "启动主轨",
                Description = "启动主轨",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 1,
                ModulesVal = root,
            });


            //停止主轨
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "barBtn_StopMainTrack",
                ActionName = "停止主轨",
                Description = "停止主轨",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 2,
                ModulesVal = root,
            });

            //急停主轨
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "barBtn_EmergencyStopMainTrack",
                ActionName = "急停主轨",
                Description = "急停主轨",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 3,
                ModulesVal = root,
            });
            #endregion

            #region 实时信息 
            var info = new DaoModel.Modules()
            {
                ActionKey = "barBtn_RealTimeInformation",
                ActionName = "实时信息",
                Description = "实时信息",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 4,
                ModulesVal = root,
            };
            //实时信息 
            moduleService.Save(info);

            //产线实时信息
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "RealtimeInfo.ProductRealtimeInfoIndex",
                ActionName = "产线实时信息",
                Description = "产线实时信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = info,
            });

            //在制品信息
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "RealtimeInfo.ProductsingInfoIndex",
                ActionName = "在制品信息",
                Description = "在制品信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = info,
            });

            //工艺路线图
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "RealtimeInfo.ProcessFlowChartIndex",
                ActionName = "工艺路线图",
                Description = "工艺路线图",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = info,
            });

            //衣架信息
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = "RealtimeInfo.CoatHangerIndex",
                ActionName = "衣架信息",
                Description = "衣架信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = info,
            });
            #endregion

            #region 统计报表
            var infoReport = new DaoModel.Modules()
            {
                ActionKey = "barBtn_RealTimeInformation",
                ActionName = "统计报表",
                Description = "统计报表",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 5,
                ModulesVal = root,
            };
            moduleService.Save(infoReport);

            //产量汇总
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "产量汇总",
                Description = "产量汇总",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = infoReport,
            });

            //员工产量报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "员工产量报表",
                Description = "员工产量报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = infoReport,
            });

            //生产进度报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "生产进度报表",
                Description = "生产进度报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = infoReport,
            });

            //工时分析报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "工时分析报表",
                Description = "工时分析报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = infoReport,
            });

            //制单工序交叉报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "制单工序交叉报表",
                Description = "制单工序交叉报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 5,
                ModulesVal = infoReport,
            });

            //产出明细报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "产出明细报表",
                Description = "产出明细报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 6,
                ModulesVal = infoReport,
            });

            //返工详情报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "返工详情报表",
                Description = "返工详情报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 7,
                ModulesVal = infoReport,
            });

            //返工汇总&疵点分析报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "返工汇总&疵点分析报表",
                Description = "返工汇总&疵点分析报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 8,
                ModulesVal = infoReport,
            });

            //组别竞赛报表
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = " ",
                ActionName = "组别竞赛报表",
                Description = "组别竞赛报表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 9,
                ModulesVal = infoReport,
            });

            #endregion

            #region 订单信息
            var infoOrder = new DaoModel.Modules()
            {
                ActionKey = "barBtn_OrderInfo",
                ActionName = "订单信息",
                Description = "订单信息",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 4,
                ModulesVal = root,
            };
            //订单信息 
            moduleService.Save(infoOrder);

            //客户信息
            var customer = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.OrderInfo_CustomerInfo,
                ActionName = "客户信息",
                Description = "客户信息",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = infoOrder,
            };
            moduleService.Save(customer);

            //客户订单
            var customerOrderInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.OrderInfo_CustomerOrderInfo,
                ActionName = "客户订单",
                Description = "客户订单",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = infoOrder,
            };
            moduleService.Save(customerOrderInfo);
            #endregion

            #region 制单信息
            var infoPreOrder = new DaoModel.Modules()
            {
                ActionKey = "barBtn_billing",
                ActionName = "制单信息",
                Description = "制单信息",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 5,
                ModulesVal = root,
            };
            //制单信息 
            moduleService.Save(infoPreOrder);

            //生产制单
            var Billing_ProcessOrderIndex = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.Billing_ProcessOrderIndex,
                ActionName = "生产制单",
                Description = "生产制单",
                ModuleLevel = 3,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = infoPreOrder,
            };
            moduleService.Save(Billing_ProcessOrderIndex);
            #region 生产制单按钮
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnMax,
                ActionName = "全屏",
                Description = "生产制单.全屏",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 1,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnRefresh,
                ActionName = "刷新",
                Description = "生产制单.刷新",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 2,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnAdd,
                ActionName = "新增",
                Description = "生产制单.新增",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 3,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnClose,
                ActionName = "退出",
                Description = "生产制单.退出",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 4,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnDelete,
                ActionName = "删除",
                Description = "生产制单.删除",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 5,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            moduleService.Save(new Modules()
            {
                ActionKey = Billing_ProcessOrderIndex.ActionKey + "." + PermissionConstant.btnExport,
                ActionName = "导出",
                Description = "生产制单.",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Button,
                OrderField = 6,
                ModulesVal = Billing_ProcessOrderIndex,
            });
            #endregion


            //制单工序
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.Billing_ProcessFlowIndex,
                ActionName = "制单工序",
                Description = "制单工序",
                ModuleLevel = 4,
                ModulesType = (int)ModulesType.Page,
                OrderField = 7,
                ModulesVal = infoPreOrder,
            });



            //工艺路线图
            moduleService.Save(new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.Billing_ProcessFlowChartIndex,
                ActionName = "工艺路线图",
                Description = "工艺路线图",
                ModuleLevel = 3,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = infoPreOrder,
            });
            #endregion

            #region 裁床管理
            var root6 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_CuttingRoomManage",
                ActionName = "裁床管理",
                Description = "裁床管理",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 5,
                ModulesVal = root,
            };
            //裁床管理 
            moduleService.Save(root6);
            #endregion

            #region 产品基础数据
            var root8 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_ProductBaseData",
                ActionName = "产品基础数据",
                Description = "产品基础数据",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 6,
                ModulesVal = root,
            };

            moduleService.Save(root8);

            //产品部位
            var productPart = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductBaseData_ProductPart,
                ActionName = "产品部位",
                Description = "产品部位",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root8,
            };
            moduleService.Save(productPart);
            //基本尺码表
            var basicSizeTable = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductBaseData_BasicSizeTable,
                ActionName = "基本尺码表",
                Description = "基本尺码表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root8,
            };
            moduleService.Save(basicSizeTable);
            //基本颜色表
            var basicColorTable = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductBaseData_BasicColorTable,
                ActionName = "基本颜色表",
                Description = "基本颜色表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root8,
            };
            moduleService.Save(basicColorTable);
            //款式工艺表
            var style = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductBaseData_Style,
                ActionName = "款式工艺表",
                Description = "款式工艺表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root8,
            };
            moduleService.Save(style);
            #endregion

            #region 工艺基础数据
            var root9 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_ProcessBaseData",
                ActionName = "工艺基础数据",
                Description = "工艺基础数据",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 7,
                ModulesVal = root,
            };
            moduleService.Save(root9);

            //基本工序段
            var basicProcessSection = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProcessBaseData_BasicProcessSection,
                ActionName = "基本工序段",
                Description = "基本工序段",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root9,
            };
            moduleService.Save(basicProcessSection);

            //基本工序库
            var basicProcessLirbary = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProcessBaseData_BasicProcessLirbary,
                ActionName = "基本工序库",
                Description = "基本工序库",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root9,
            };
            moduleService.Save(basicProcessLirbary);

            //款式工序库
            var styleProcessLirbary = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProcessBaseData_StyleProcessLirbary,
                ActionName = "款式工序库",
                Description = "款式工序库",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root9,
            };
            moduleService.Save(styleProcessLirbary);

            //疵点代码
            var defectCode = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProcessBaseData_DefectCode,
                ActionName = "疵点代码",
                Description = "疵点代码",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root9,
            };
            moduleService.Save(defectCode);
            //缺料代码
            var lackOfMaterialCode = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProcessBaseData_LackOfMaterialCode,
                ActionName = "缺料代码",
                Description = "缺料代码",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 5,
                ModulesVal = root9,
            };
            moduleService.Save(lackOfMaterialCode);
            #endregion

            #region 生产线
            var root19 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_ProductionLine",
                ActionName = "生产线",
                Description = "生产线",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 8,
                ModulesVal = root,
            };
            moduleService.Save(root19);

            //控制端配置
            var controlSet = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_ControlSet,
                ActionName = "控制端配置",
                Description = "控制端配置",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root19,
            };
            moduleService.Save(controlSet);

            //流水线管理
            var pipelineMsg = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_PipelineMsg,
                ActionName = "流水线管理",
                Description = "流水线管理",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root19,
            };
            moduleService.Save(pipelineMsg);

            //桥接配置
            var bridgingSet = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_BridgingSet,
                ActionName = "桥接配置",
                Description = "桥接配置",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root19,
            };
            moduleService.Save(bridgingSet);

            //客户机信息
            var clientInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_ClientInfo,
                ActionName = "客户机信息",
                Description = "客户机信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root19,
            };
            moduleService.Save(clientInfo);

            //系统参数
            var systemMsg = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_SystemMsg,
                ActionName = "系统参数",
                Description = "系统参数",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 5,
                ModulesVal = root19,
            };
            moduleService.Save(systemMsg);

            //Tcp测试
            var tcpTest = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ProductionLine_TcpTest,
                ActionName = "Tcp测试",
                Description = "Tcp测试",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 6,
                ModulesVal = root19,
            };
            moduleService.Save(tcpTest);
            #endregion


            #region 衣车管理
            var root14 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_ClothingCarManagement",
                ActionName = "衣车管理",
                Description = "衣车管理",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 8,
                ModulesVal = root,
            };
            moduleService.Save(root14);

            //衣车类别表
            var sewingMachineType = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ClothingCarManagement_EwingMachineType,
                ActionName = "衣车类别表",
                Description = "衣车类别表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root14,
            };
            moduleService.Save(sewingMachineType);

            //故障代码表
            var falutCodeTable = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ClothingCarManagement_FalutCodeTable,
                ActionName = "故障代码表",
                Description = "故障代码表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root14,
            };
            moduleService.Save(falutCodeTable);
            //衣车资料
            var sewingMachineData = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ClothingCarManagement_SewingMachineData,
                ActionName = "衣车资料",
                Description = "衣车资料",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root14,
            };
            moduleService.Save(sewingMachineData);
            //机修人员表
            var mechanicEmployee = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.ClothingCarManagement_MechanicEmployee,
                ActionName = "机修人员表",
                Description = "机修人员表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root14,
            };
            moduleService.Save(mechanicEmployee);
            #endregion

            #region 人事管理
            var root11 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_PersonnelManagement",
                ActionName = "人事管理",
                Description = "人事管理",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 9,
                ModulesVal = root,
            };
            moduleService.Save(root11);

            //生产组别
            var productGroup = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_ProductGroup,
                ActionName = "生产组别",
                Description = "生产组别",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root11,
            };
            moduleService.Save(productGroup);
            //部门信息
            var departmentInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_DepartmentInfo,
                ActionName = "部门信息",
                Description = "部门信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root11,
            };
            moduleService.Save(departmentInfo);

            //工种信息
            var professionInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_ProfessionInfo,
                ActionName = "工种信息",
                Description = "工种信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root11,
            };
            moduleService.Save(professionInfo);

            //职务信息
            var positionInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_PositionInfo,
                ActionName = "职务信息",
                Description = "职务信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root11,
            };
            moduleService.Save(positionInfo);

            //员工资料
            var employeeInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_EmployeeInfo,
                ActionName = "员工资料",
                Description = "员工资料",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 5,
                ModulesVal = root11,
            };
            moduleService.Save(employeeInfo);

            //管理卡信息
            var msgCardInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.PersonnelManagement_MsgCardInfo,
                ActionName = "管理卡信息",
                Description = "管理卡信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 6,
                ModulesVal = root11,
            };
            moduleService.Save(msgCardInfo);
            #endregion

            #region 权限管理
            var root12 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_AuthorityManagement",
                ActionName = "权限管理",
                Description = "权限管理",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 10,
                ModulesVal = root,
            };
            moduleService.Save(root12);

            //菜单管理
            var moduleMsg = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AuthorityManagement_ModuleMsg,
                ActionName = "菜单管理",
                Description = "菜单管理",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root12,
            };
            moduleService.Save(moduleMsg);

            //角色管理
            var roleMsg = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AuthorityManagement_RoleMsg,
                ActionName = "角色管理",
                Description = "角色管理",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root12,
            };
            moduleService.Save(roleMsg);

            //用户管理
            var userMsg = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AuthorityManagement_UserMsg,
                ActionName = "用户管理",
                Description = "用户管理",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root12,
            };
            moduleService.Save(userMsg);

            //用户操作日志
            var userOperatorLog = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AuthorityManagement_UserOperatorLog,
                ActionName = "用户操作日志",
                Description = "用户操作日志",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 4,
                ModulesVal = root12,
            };
            moduleService.Save(userOperatorLog);
            #endregion

            #region 考勤管理
            var root13 = new DaoModel.Modules()
            {
                ActionKey = "barBtn_AttendanceManagement",
                ActionName = "考勤管理",
                Description = "考勤管理",
                ModuleLevel = 1,
                ModulesType = (int)ModulesType.Button,
                OrderField = 11,
                ModulesVal = root,
            };
            moduleService.Save(root13);

            //假日信息
            var holidayInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AttendanceManagement_HolidayInfo,
                ActionName = "用户管理",
                Description = "用户管理",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 1,
                ModulesVal = root13,
            };
            moduleService.Save(holidayInfo);

            //班次信息
            var classsesInfo = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AttendanceManagement_ClasssesInfo,
                ActionName = "班次信息",
                Description = "班次信息",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 2,
                ModulesVal = root13,
            };
            moduleService.Save(classsesInfo);

            //员工排班表
            var employeeScheduling = new DaoModel.Modules()
            {
                ActionKey = PermissionConstant.AttendanceManagement_EmployeeScheduling,
                ActionName = "员工排班表",
                Description = "员工排班表",
                ModuleLevel = 2,
                ModulesType = (int)ModulesType.Page,
                OrderField = 3,
                ModulesVal = root13,
            };
            moduleService.Save(employeeScheduling);
            #endregion

        }
        #endregion


        #region 生成系统参数

        #endregion
    }
}
