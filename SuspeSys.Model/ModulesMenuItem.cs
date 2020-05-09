using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 模块菜单
    /// </summary>
    public class ModulesMenuItem {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string MenuName { get; set; }
        /// <summary>
        /// 菜单Value
        /// </summary>
        public virtual string MenuValue { get; set; }
        /// <summary>
        /// 菜单Key
        /// </summary>
        public virtual string MenuKey { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public virtual DateTime InsertDateTime { get; set; }
        /// <summary>
        /// 插入用户
        /// </summary>
        public virtual string InsertUser { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateDateTime { get; set; }
        /// <summary>
        /// 更新用户
        /// </summary>
        public virtual string UpdateUser { get; set; }
    }
}
