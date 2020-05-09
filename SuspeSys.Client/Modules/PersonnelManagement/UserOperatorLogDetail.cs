using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class UserOperatorLogDetail : DevExpress.XtraEditors.XtraForm
    {
        private IList<UserOperateLogDetailModel> _operateLogDetailModels { get; set; }
        private UserOperatorLogDetail()
        {
            InitializeComponent();
        }

        public UserOperatorLogDetail(IList<UserOperateLogDetailModel> operateLogDetailModels):this()
        {
            _operateLogDetailModels = operateLogDetailModels;
        }

        private void UserOperatorLogDetail_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = this._operateLogDetailModels;
        }
    }
}
