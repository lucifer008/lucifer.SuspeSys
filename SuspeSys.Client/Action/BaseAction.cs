using log4net;
using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Service;
using SuspeSys.Service.Attendance;
using SuspeSys.Service.BasicInfo;
using SuspeSys.Service.Common;
using SuspeSys.Service.Customer;
using SuspeSys.Service.CustPurchaseOrder;
using SuspeSys.Service.Impl;
using SuspeSys.Service.Impl.Attendance;
using SuspeSys.Service.Impl.BasicInfo;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.Customer;
using SuspeSys.Service.Impl.CustPurchaseOrder;
using SuspeSys.Service.Impl.LED;
using SuspeSys.Service.Impl.Permisson;
using SuspeSys.Service.Impl.PersonnelManagement;
using SuspeSys.Service.Impl.ProcessOrder;
using SuspeSys.Service.Impl.ProductionLineSet;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Report;
using SuspeSys.Service.Impl.TcpTest;
using SuspeSys.Service.LED;
using SuspeSys.Service.Permisson;
using SuspeSys.Service.PersonnelManagement;
using SuspeSys.Service.ProcessOrder;
using SuspeSys.Service.ProductionLineSet;
using SuspeSys.Service.Products;
using SuspeSys.Service.Report;
using SuspeSys.Service.TcpTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action
{
    public class BaseAction
    {

        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BaseAction));
        protected static readonly ILog cacheInfo = LogManager.GetLogger("CacheInfo");
        protected ICommonService commonService = new CommonServiceImpl();
        protected IProductsQueryService pQueryService = new ProductsQueryServiceImpl();

        protected IProcessOrderQueryService processOrderQuery = new ProcessOrderQueryServiceImpl();
        protected IProcessOrderService processOrderSerice = new ProcessOrderServiceImpl();
        protected IProcessFlowChartService ProcessFlowChartSercice = new ProcessFlowChartServiceImpl();
        protected ICustPurchaseOrderService custPurchaseOrderService = new CustPurchaseOrderServiceImpl();
        protected ICustomerService customerService = new CustomerServiceImpl();
        protected IPermissionService permissionService = new PermissionServiceImpl();
        protected IProductionLineSetService ProductionLineSetService = new ProductionLineSetServiceImpl();
        protected IProductionLineSetQueryService ProductionLineSetQueryService = new ProductionLineSetQueryServiceImpl();
        protected IBasicInfoService BasicInfoService = new BasicInfoServiceImpl();
        protected IPersonnelManagementService _PersonnelManagementService = new PersonnelManagementServiceImpl();
        protected IReportQueryService ReportQueryService = new ReportQueryServiceImpl();
        protected IAttendanceService AttendanceService = new AttendanceServiceImpl();

        protected ISystemParameterService systemPapameterService = new SystemParameterServiceImpl();
        protected ILEDService ledService = new LEDServiceImpl();
        #region remoting service
        //protected IProductsService productService = SuspeRemotingService.pcpProductsService; //new ProductsServiceImpl();

        protected IProductsService productService
        {
            get
            {
                //if (null == SuspeRemotingService.pcpProductsService) {
                //    SuspeRemotingService.Instance.Init();
                //}
                return SuspeRemotingService.pcpProductsService;
            }
        }
        #endregion
        protected ITcpTestService TcpTestService
        {
            get
            {
                return new TcpTestServiceImpl();
            }
        }
        protected ITcpTestQueryService TcpTestQueryService
        {
            get
            {
                return new TcpTestQueryServiceImpl();
            }
        }
        static BaseAction()
        {
            try
            {
                SuspeRemotingService.Instance.Init();
                log.Info(string.Format("remoting服务注册成功!"));
            }
            catch (Exception ex)
            {
                log.Error("【remoting注册失败!】", ex);
            }
        }
    }
}
