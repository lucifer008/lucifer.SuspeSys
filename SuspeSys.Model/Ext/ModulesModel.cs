using System;
using System.Text;
using System.Collections.Generic;
using SuspeSys.Domain.SusEnum;
using System.ComponentModel;

namespace SuspeSys.Domain {
    
    /// <summary>
    /// 菜单 模块表 扩展Model
    /// </summary>
    public class ModulesModel : Modules {

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string ModuleTypeName
        {
            get
            {
                ModulesType os = (ModulesType)base.ModulesType;
                return os.Description();
            }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId
        {
            get
            {
                return this.ModulesVal?.Id;
            }
        }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool Checked { get; set; } = false;
    }
}
