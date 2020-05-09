using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 站点:
    ///   即一个包括进站、出站功能，有终端显示操作面板的集合，一般安装一名生产员工及一台衣车 扩展Model
    /// </summary>
    public class StatingModel : Stating {

        //[Newtonsoft.Json.JsonIgnore()]
        public virtual string GroupNO { set; get; }
        [Newtonsoft.Json.JsonIgnore()]
        public virtual string RoleName { set; get; }
        [Newtonsoft.Json.JsonIgnore()]
        public virtual string SiteGroupId { set; get; }

        [Newtonsoft.Json.JsonIgnore()]
        public string STATINGROLES_Id { get; set; }

        /// <summary>
        /// 是否是挂片站
        /// </summary>
        public virtual bool IsHangUp
        {
            get
            {
                if (base.StatingRoles == null)
                    return false;
                else
                    return base.StatingRoles.RoleCode.Equals("104");
            }
        }
    }
}
