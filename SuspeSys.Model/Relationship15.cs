using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// Relationship_15
    /// </summary>
    public class Relationship15 {
        /// <summary>
        /// 角色_Id
        /// </summary>
        public virtual string RolesId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Modules Modules { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as Relationship15;
			if (t == null) return false;
			if (RolesId == t.RolesId
			 && Id == t.Id)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ RolesId.GetHashCode();
			hash = (hash * 397) ^ Id.GetHashCode();

			return hash;
        }
        #endregion
    }
}
