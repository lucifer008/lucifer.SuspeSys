using SuspeSys.Service.PersonnelManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Dao;
using NHibernate.Util;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Utils;

namespace SuspeSys.Service.Impl.PersonnelManagement
{
    public class PersonnelManagementServiceImpl : ServiceBase, IPersonnelManagementService
    {
        public IList<DepartmentModel> SearchDepartment(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Department where DELETED <>1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (DepName like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.DepartmentModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<PositionModel> SearchPosition(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Position where DELETED <>1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (PosName like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.PositionModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<ProductGroupModel> SearchProductGroup(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from ProductGroup where DELETED <>1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (ProductGroupName like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.ProductGroupModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<WorkTypeModel> SearchWorkType(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from WorkType where DELETED <>1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (WTypeName like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.WorkTypeModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<ClientMachinesModel> GetAllClientMachines()
        {

            var rslt1 = Query<Domain.ClientMachinesModel>("select a.* from ClientMachines a WHERE DELETED = 0", true);
            return rslt1;
        }

        public IList<SiteGroupModel> SearchSiteGroup(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from SiteGroup where DELETED <>1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (GroupName like ? or GroupNO like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.SiteGroupModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Factory> SearchFactory(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Factory where Deleted = 0";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (FacName like ? or FacCode like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.Factory>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<EmployeeModel> SearchEmployee(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"SELECT a.*
                                    ,b.WTypeName WorkTypeName
                                    ,c.GroupName SiteGroupName
                                    ,d.DepName DeptmentName
                                    ,p.ProvinceName
                                    ,f.CityName
                                    ,e.AreaName
                                    FROM Employee a
                                    LEFT JOIN WorkType  b ON a.WORKTYPE_Id = b.Id
                                    left join SiteGroup c on a.SITEGROUP_Id = c.Id
                                    left join Department d on a.DEPARTMENT_Id = d.Id
                                    left join Area e  on a.AREA_Id = e.id
                                    left join City f on e.CITY_Id = f.id
                                    left join Province p on f.PROVINCE_Id = p.Id
                                    where a.DELETED <>1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (Code like ? or RealName like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.EmployeeModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public void RemoveAllEmployeePositionByEmployeeId(string employeeId)
        {

            var dbModelList = this.GetPositionsByEmployeeId(employeeId);
            foreach (var item in dbModelList)
            {
                EmployeePositionsDao.Instance.Delete(item.Id);
            }
        }

        public IList<EmployeePositions> GetPositionsByEmployeeId(string employeeId)
        {
            string sql = "select *  from EmployeePositions where EMPLOYEE_Id = ?";

            return base.Query<Domain.EmployeePositions>(new StringBuilder(sql), null, false, new object[] { employeeId });

        }

        public IList<WorkshopModel> SearchWorkshop(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select T1.*,T2.FacName,T2.Id FacId from Workshop T1 inner join Factory T2 ON T1.FACTORY_Id=T2.Id  where T1.Deleted <> 1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (WorName like ? or WorCode like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.WorkshopModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            foreach (var w in rslt1)
            {
                if (!string.IsNullOrEmpty(w.FacId))
                {
                    w.Factory = FactoryDao.Instance.GetById(w.FacId);
                }
            }
            return rslt1;
        }

        public IList<UserOperateLogsModel> SearchUserOperatorLog(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from UserOperateLogs  where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (OpFormName like ? or OpFormCode like ? or OpTableCode like ? or OpTableName like ?)");
                //queryString += " order by InsertDateTime desc";
                paramValues = new string[] {
                    string.Format("%{0}%", searchKey),
                    string.Format("%{0}%", searchKey),
                    string.Format("%{0}%", searchKey),
                    string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.UserOperateLogsModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            foreach (var w in rslt1)
            {
                w.OperateLogDetailModels = GetOpLogDetailListById(w.Id);
            }
            return rslt1;
        }

        private List<UserOperateLogDetailModel> GetOpLogDetailListById(string opId)
        {
            string queryString = "select * from UserOperateLogDetail a where a.USEROPERATELOGS_Id = ?";

            return Query<UserOperateLogDetailModel>(queryString, true, opId).ToList();
        }

        public CardInfoModel GetEmployeeCardInfoBy(string cardInfoId)
        {
            var sql = new StringBuilder(@"select 
                T2.Id,ltrim(rtrim(T1.Code)) EmployeeCode,T1.RealName EmployeeName,T2.CardNo,T2.CardType,(case T2.CardType when 1 then '衣架卡' when 2 then '衣车卡' when 3 then '机修卡' when 4 then '员工卡' else '无' end) CardTypeTxt
                ,(case T2.IsEnabled when 1 then '是' else '否' end) EnText,(case T2.IsMultiLogin when 1 then '是' else '否' end) MulLoginText,T2.IsEnabled,T2.IsMultiLogin
                from Employee T1
                LEFT JOIN EmployeeCardRelation TR ON T1.Id=TR.EMPLOYEE_Id
                LEFT JOIN CardInfo T2 ON T2.Id=TR.CARDINFO_Id WHERE t2.Id = ?");

            return base.QueryForObject<CardInfoModel>(sql, null, true, cardInfoId);
        }

        public IList<CardInfoModel> SearchEmployeeCardInfo(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            var sql = new StringBuilder(@"select 
                T2.Id,ltrim(rtrim(T1.Code)) EmployeeCode,T1.RealName EmployeeName,T2.CardNo,T2.CardType,(case T2.CardType when 1 then '衣架卡' when 2 then '衣车卡' when 3 then '机修卡' when 4 then '员工卡' else '无' end) CardTypeTxt
                ,(case T2.IsEnabled when 1 then '是' else '否' end) EnText,(case T2.IsMultiLogin when 1 then '是' else '否' end) MulLoginText,T2.IsEnabled,T2.IsMultiLogin
                from Employee T1
                LEFT JOIN EmployeeCardRelation TR ON T1.Id=TR.EMPLOYEE_Id
                LEFT JOIN CardInfo T2 ON T2.Id=TR.CARDINFO_Id WHERE  T2.Id IS NOT NULL AND T2.CardType=4 ");
            string [] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                var sbWh= string.Format(@" AND (T1.RealName like ? or T1.Code like ? OR T2.CardNo like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
                sql.Append(sbWh);
            }
            var rslt1 = Query<CardInfoModel>(sql, currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        /// <summary>
        ///获取所有员工
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeList()
        {
            return DapperHelp.Query<Employee>("SELECT * FROM Employee WHERE Deleted = 0");
        }

        public void SaveEmployeeCardInfo(List<CardInfoModel> cardInfoModel)
        {
            UserOpLogServiceImpl logService = new UserOpLogServiceImpl();

            foreach (var item in cardInfoModel)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    string sql = " SELECT * FROM Employee WHERE Deleted = 0 AND Code = @Code";
                    Employee employee = DapperHelp.QueryForObject<Employee>(sql, new { Code = item.EmployeeCode });

                    if (employee == null)
                        throw new ArgumentException("未获取到员工信息，请核对");

                    CardInfo cardInfo = new CardInfo()
                    {
                        CardType = item.CardType,
                        CardNo = item.CardNo,
                        CardDescription = item.CardDescription,
                        IsEnabled = item.IsEnabled,
                        IsMultiLogin = item.IsMultiLogin
                    };

                    sql = "SELECT COUNT(*) FROM CardInfo WHERE Deleted = 0 AND CardNo = @CardNo";
                    int count = DapperHelp.ExecuteScalar<int>(sql, new { CardNo = item.CardNo });
                    if (count > 0)
                        throw new Exception("卡号已经存在！");

                    CardInfoDao.Instance.Save(cardInfo);

                    sql = @"INSERT INTO [EmployeeCardRelation]
                           ([Id]
                           ,[EMPLOYEE_Id]
                           ,[CARDINFO_Id])
                     VALUES
                           (@Id
                           ,@EMPLOYEE_Id
                           ,@CARDINFO_Id)";
                    DapperHelp.Execute(sql, new {
                           Id = GUIDHelper.GetGuidString(),
                           EMPLOYEE_Id = employee.Id,
                        CARDINFO_Id = cardInfo.Id
                    });


                    logService.InsertLog<CardInfo>(cardInfo.Id, "卡信息管理", "CardInfoIndex", cardInfo.GetType().Name);

                }
                else
                {
                    //修改
                    string sql = " SELECT *FROM Employee WHERE Deleted = 0 AND Code = @Code";
                    Employee employee = DapperHelp.QueryForObject<Employee>(sql, new { Code = item.EmployeeCode });

                    if (employee == null)
                        throw new ArgumentException("未获取到员工信息，请核对");

                    //获取CardInfo
                    sql = "SELECT * FROM CardInfo WHERE Id = @Id";
                    CardInfo cardInfo = DapperHelp.QueryForObject<CardInfo>(sql, new { Id = item.Id});

                    sql = "SELECT * FROM EmployeeCardRelation where CARDINFO_Id = @CardInfoId";
                    var relation = DapperHelp.QueryForObject<Domain.EmployeeCardRelationModel>(sql, new { CardInfoId = item.Id });
                    if (employee.Id != relation.EMPLOYEE_Id)
                    {
                        sql = "Update EmployeeCardRelation set EMPLOYEE_Id = @EmployeeId where Id = @Id";
                        DapperHelp.Execute(sql, new { EmployeeId = employee.Id, Id = relation.Id });
                    }

                    cardInfo.CardType = item.CardType;
                    cardInfo.CardNo = item.CardNo;
                    cardInfo.CardDescription = item.CardDescription;
                    cardInfo.IsEnabled = item.IsEnabled;
                    cardInfo.IsMultiLogin = item.IsMultiLogin;

                    sql = "SELECT COUNT(*) FROM CardInfo WHERE Deleted = 0 AND CardNo = @CardNo and Id <> @Id";
                    int count = DapperHelp.ExecuteScalar<int>(sql, new { CardNo = item.CardNo , Id  = cardInfo.Id});
                    if (count > 0)
                        throw new Exception("卡号已经存在！");

                    logService.UpdateLog(cardInfo.Id, cardInfo, "卡信息管理", "CardInfoIndex", cardInfo.GetType().Name);

                    CardInfoDao.Instance.Update(cardInfo);

                }
            }
        }
    }
}
