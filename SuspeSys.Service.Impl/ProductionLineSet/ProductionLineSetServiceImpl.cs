using NHibernate.Type;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Service.ProductionLineSet;
using SuspeSys.Service.SusTcp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.ProductionLineSet
{
    public class ProductionLineSetServiceImpl : ServiceBase, IProductionLineSetService
    {
        public ProductionLineSetServiceImpl() { }
        public static ProductionLineSetServiceImpl Instance
        {
            get
            {
                return new ProductionLineSetServiceImpl();
            }
        }
        void CheckValidata(IList<DaoModel.Stating> statingList, DaoModel.SiteGroup siteGroup, bool isUpdate = false)
        {
            if (!isUpdate)
            {
                foreach (var stating in statingList)
                {
                    string sql = string.Format("select count(*) from Stating where SITEGROUP_Id=@SiteGroupId AND StatingNo=@StatingNo AND MainTrackNumber=@MainTrackNumber AND Deleted=0");
                    object para = new { SiteGroupId = siteGroup.Id, StatingNo = stating.StatingNo?.Trim(), MainTrackNumber = stating.MainTrackNumber.Value };
                    var rList = DapperHelp.Query<int>(sql, para);
                    var rowCount = rList.Count()>0? rList.SingleOrDefault():0;
                    if (rowCount > 0)
                    {
                        string err = string.Format("组:{0} 已存在主轨:{1} 站点:{2}", siteGroup.GroupNo?.Trim(), stating.MainTrackNumber.Value, stating.StatingNo?.Trim());
                        throw new ApplicationException(err);
                    }
                }
                return;
            }
            foreach (var stating in statingList)
            {
                string sql = string.Format("select *,SITEGROUP_Id SiteGroupId  from Stating where StatingNo=@StatingNo AND MainTrackNumber=@MainTrackNumber AND ID!=@Id AND Deleted=0");
                object para = new { StatingNo = stating.StatingNo?.Trim(), MainTrackNumber = stating.MainTrackNumber.Value, Id = stating.Id };
                var eqStating = DapperHelp.Query<StatingModel>(sql, para);
                if (eqStating.Count() > 0)
                {
                    var exStating = eqStating.First();
                    var sGoup = SiteGroupDao.Instance.GetById(exStating.SiteGroupId);
                    string err = string.Format("组:{0} 已存在主轨:{1} 站点:{2}", sGoup.GroupNo?.Trim(), exStating.MainTrackNumber.Value, exStating.StatingNo?.Trim());
                    throw new ApplicationException(err);
                }
            }
        }
        public List<StatingMsg> AddPipelining(DaoModel.Pipelining pipelining, IList<DaoModel.Stating> statingList, DaoModel.SiteGroup siteGroup)
        {
            CheckValidata(statingList, siteGroup);
            var statingRoleList = StatingRolesDao.Instance.GetAll();
            var statingDirectionList = StatingDirectionDao.Instance.GetAll();
            List<StatingMsg> statingMsg = new List<StatingMsg>();
            var defaultStatingRole = statingRoleList.Where(f => f.RoleName.Equals("车缝站")).First();
            var defaultDirection = statingDirectionList.Where(f => f.DirectionKey.Equals("1")).First();
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var _siteGroup = SiteGroupDao.Instance.GetById(siteGroup.Id);
                    pipelining.SiteGroup = _siteGroup;
                    PipeliningDao.Instance.Save(pipelining, true);
                    foreach (var s in statingList)
                    {
                        s.SiteGroup = siteGroup;
                        //s.Pipelining = pipelining;
                        s.IsReceivingHanger = true;
                        if (null == s.IsEnabled)
                        {
                            s.IsEnabled = true;
                        }
                        if (null == s.Capacity)
                        {
                            s.Capacity = 20;
                        }
                        if (null == s.StatingRoles)
                        {
                            s.StatingRoles = defaultStatingRole;
                            s.StatingName = defaultStatingRole?.RoleName?.Trim();
                        }
                        if (null == s.StatingDirection)
                        {
                            s.Direction = short.Parse(defaultDirection.DirectionKey);
                            s.StatingDirection = defaultDirection;
                            s.DirectionTxt = defaultDirection.DirectionDesc?.Trim();
                        }
                        StatingDao.Instance.Save(s, true);

                        statingMsg.Add(new StatingMsg
                        {
                            IsNew = true,
                            MainTrackNumber = s.MainTrackNumber.Value,
                            StatingNo = s.StatingNo,
                            StatingName = s.StatingName,
                            Capacity = s.Capacity,
                            IsEnabled = s.IsEnabled,
                            IsLoadMonitor = null != s.IsLoadMonitor ? s.IsLoadMonitor.Value : false
                        });
                    }
                    //var pipeliningSiteGroupRelation = new DaoModel.PipeliningSiteGroupRelation();
                    //pipeliningSiteGroupRelation.SiteGroup = SiteGroup;
                    //pipeliningSiteGroupRelation.Pipelining = pipelining;
                    //PipeliningSiteGroupRelationDao.Instance.Save(pipeliningSiteGroupRelation, true);
                    trans.Commit();
                    session.Flush();
                }
            }

            return statingMsg;

        }
        public List<StatingMsg> UpdatePipelining(DaoModel.Pipelining pipelining, IList<DaoModel.Stating> statingList, DaoModel.SiteGroup siteGroup)
        {
            CheckValidata(statingList, siteGroup, true);
            List<StatingMsg> statingMsg = new List<StatingMsg>();
            var statList = QueryForList<DaoModel.Stating>("select * from Stating");
            try
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        session.Clear();
                        var _siteGroup = SiteGroupDao.Instance.GetById(siteGroup.Id);
                        pipelining.SiteGroup = _siteGroup;
                        PipeliningDao.Instance.Update(pipelining, true);

                        //有依赖数据时不能删除
                        //session.Delete("from Stating where SITEGROUP_Id=?",
                        //  new object[] { _siteGroup.Id }, new IType[] { NHibernate.NHibernateUtil.String });


                        // session.Delete("from PipeliningSiteGroupRelation where PIPELINING_Id=?",
                        // new object[] { pipelining.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                        foreach (var s in statingList)
                        {
                            if (s.Id != null)
                            {
                                var ss = StatingDao.Instance.GetById(s.Id);

                                if (ss.StatingName == null)
                                    ss.StatingName = "";

                                //if (ss.StatingName.Trim() != (s.StatingRoles?.RoleName == null ? "" : s.StatingRoles?.RoleName) ||
                                //    ss.Capacity != s.Capacity)
                                //{
                                statingMsg.Add(new StatingMsg
                                {
                                    IsNew = false,
                                    Capacity = s.Capacity != ss.Capacity ? s.Capacity : null,
                                    MainTrackNumber = s.MainTrackNumber.Value,
                                    StatingNo = s.StatingNo,
                                    StatingName = s.StatingRoles?.RoleName.Trim() != ss.StatingName.Trim() ? s.StatingName : null,
                                    IsEnabled = s.IsEnabled,
                                    IsLoadMonitor = null != s.IsLoadMonitor ? s.IsLoadMonitor.Value : false
                                });
                                //}
                                if (statList.Where(f => f.MainTrackNumber.Value == s.MainTrackNumber.Value
                                && f.StatingNo.Equals(s.StatingNo)
                                && !f.Id.Equals(s.Id)
                                ).Count() > 0)
                                {
                                    throw new ApplicationException(string.Format("主轨:{0} 站点:{1} 已存在！", s.MainTrackNumber, s.StatingNo));
                                }
                                ss.MainTrackNumber = s.MainTrackNumber;
                                ss.SiteGroup = _siteGroup;
                                ss.StatingDirection = s.StatingDirection;
                                ss.StatingRoles = s.StatingRoles;
                                ss.IsPromoteTripCachingFull = s.IsPromoteTripCachingFull;
                                ss.IsLoadMonitor = s.IsLoadMonitor;
                                ss.IsEnabled = s.IsEnabled;
                                ss.IsChainHoist = s.IsChainHoist;
                                ss.Memo = s.Memo;
                                ss.SiteBarCode = s.SiteBarCode;
                                ss.StatingName = s.StatingRoles?.RoleName;
                                ss.Capacity = s.Capacity;
                                ss.ColorValue = s.StatingRoles?.RoleCode;
                                ss.StatingNo = s.StatingNo;
                                ss.Direction = s.Direction;
                                StatingDao.Instance.Update(ss, true);


                            }
                            else
                            {
                                s.SiteGroup = _siteGroup;
                                //s.Pipelining = pipelining;
                                s.IsReceivingHanger = true;
                                StatingDao.Instance.Save(s, true);

                                statingMsg.Add(new StatingMsg
                                {
                                    IsNew = true,
                                    Capacity = s.Capacity,
                                    MainTrackNumber = s.MainTrackNumber.Value,
                                    StatingNo = s.StatingNo,
                                    StatingName = s.StatingName,
                                    IsEnabled = s.IsEnabled,
                                    IsLoadMonitor = null != s.IsLoadMonitor ? s.IsLoadMonitor.Value : false
                                });
                            }
                        }
                        //var pipeliningSiteGroupRelation = new DaoModel.PipeliningSiteGroupRelation();
                        //pipeliningSiteGroupRelation.SiteGroup = siteGroup;
                        //pipeliningSiteGroupRelation.Pipelining = pipelining;
                        //PipeliningSiteGroupRelationDao.Instance.Save(pipeliningSiteGroupRelation, true);
                        trans.Commit();
                        session.Flush();
                    }
                }
            }
            finally
            {

            }
            return statingMsg;
        }

        public IList<StatingModel> GetStatingList()
        {

            string sql = @"SELECT a.*,(select groupNo from SiteGroup sg where sg.Id=a.SITEGROUP_Id) GroupNO from Stating a where  Deleted=0";

            var statingModel = DapperHelp.Query<StatingModel>(sql);

            if (statingModel != null)
            {
                foreach (var model in statingModel)
                {
                    sql = "select * from StatingRoles A WHERE  Id = @Id";
                    model.StatingRoles = DapperHelp.QueryForObject<StatingRoles>(sql, new { Id = model.STATINGROLES_Id });
                }
            }

            return statingModel.ToList();
        }

        public IList<StatingModel> GetStatingList(int mainTrackNumber, int statingNo)
        {

            string sql = @"SELECT a.*,(select groupNo from SiteGroup sg where sg.Id=a.SITEGROUP_Id) GroupNO from Stating a WHERE a.MainTrackNumber=@MainTrackNumber AND a.StatingNo=@StatingNo AND Deleted=0";

            var statingModel = DapperHelp.Query<StatingModel>(sql, new { MainTrackNumber = mainTrackNumber, StatingNo = statingNo });

            if (statingModel != null)
            {
                foreach (var model in statingModel)
                {
                    sql = "select * from StatingRoles A WHERE  Id = @Id";
                    model.StatingRoles = DapperHelp.QueryForObject<StatingRoles>(sql, new { Id = model.STATINGROLES_Id });
                }
            }

            return statingModel.ToList();
        }

        /// <summary>
        /// 添加或更新客户端授权信息
        /// </summary>
        /// <param name="clientName">客户端信息</param>
        /// <param name="grant">授权信息</param>
        public void AddOrUpdateClientMachine(string clientName, string grant)
        {

            string sql = "select * from ClientMachines where  ClientMachineName = @Name";
            var client = DapperHelp.QueryForObject<ClientMachines>(sql, new { Name = clientName });
            if (client == null)
            {
                client = new ClientMachines();
                client.ClientMachineName = clientName;
                client.ClientMachineType = 21;
                client.AuthorizationInformation = grant;

                DapperHelp.Add(client);
            }
            else
            {
                client.AuthorizationInformation = grant;

                DapperHelp.Edit(client);
            }
        }

        public IList<BridgeSet> SearchBridgeSet(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string v)
        {
            string queryString = "select * from BridgeSet where 1=1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(v))
            {
                queryString += string.Format(@" AND (AMainTrackNumber like ? or ASiteNo like ? or BMainTrackNumber like ? or BSiteNo like ?)");
                paramValues = new string[] { string.Format("%{0}%", v), string.Format("%{0}%", v), string.Format("%{0}%", v), string.Format("%{0}%", v) };
            }
            var rslt1 = Query<Domain.BridgeSet>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }
    }
}
