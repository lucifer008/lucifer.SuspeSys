using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using SuspeSys.CustomerControls.Sqlite.Entity;
using SuspeSys.CustomerControls.Sqlite.Repository;

namespace SuspeSys.CustomerControls.Common
{

    //public delegate void ResetAdminPasswordEvent(object sender, EventArgs e);
    public partial class ChangeDatabase : Form
    {
        

        public event EventHandler ResetAdminPasswordEvent;

        //frmLoginAction _action = new frmLoginAction();
        protected GridView MainView { get { return this.gridView1; } }

        public ChangeDatabase()
        {
            InitializeComponent();



            MainView.OptionsView.ShowGroupPanel = false;
            this.StartPosition = FormStartPosition.CenterParent;


            //DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
            //DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();
            //DevExpress.XtraEditors.WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("Office 2013 Light Gray");
            //DevExpress.XtraEditors.WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            //DevExpress.XtraEditors.WindowsFormsSettings.ScrollUIMode = DevExpress.XtraEditors.ScrollUIMode.Touch;
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", 10);//AppHelper.GetDefaultSize());
            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //DevExpress.Mvvm.CommandBase.DefaultUseCommandManager = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            base.DialogResult = DialogResult.Cancel;
        }

        private void ChangeDatabase_Load(object sender, EventArgs e)
        {
            this.gdDataBase.DataSource = DatabaseConnectionRepository.Instance.GetList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dataSource = this.gdDataBase.DataSource as List<DatabaseConnection>;

            dataSource.Add(new DatabaseConnection());

            this.gridView1.RefreshData();
        }

        const string connectionFormat = "Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}";
        const string querySql = "select name from sys.databases order by database_id desc";
        private void repositoryItemComboBox1_Click(object sender, EventArgs e)
        {
            ComboBoxEdit comboBox = sender as ComboBoxEdit;
            if (comboBox.Properties.Items == null || comboBox.Properties.Items.Count == 0)
            {
                //获取当前行
                DatabaseConnection db = ((ColumnView)MainView).GetFocusedRow() as DatabaseConnection;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = string.Format(connectionFormat, db.ServerIP, db.UserId, db.Password);
                SqlCommand cmd = new SqlCommand(querySql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    comboBox.Properties.Items.Add(item.Field<string>("name"));
                }

            }

            //数据库点击按钮
            //CboItemEntity item = new CboItemEntity();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dataSource = this.gdDataBase.DataSource as List<DatabaseConnection>;

            if (dataSource.Count(o => o.IsDefault == true) > 1)
            {
                XtraMessageBox.Show("只能有一个默认数据库");
                return;
            }

            if (dataSource.Select(o => o.Alias).Distinct().Count() != dataSource.Count())
            {
                XtraMessageBox.Show("别名不能重复");
                return;
            }

            DatabaseConnectionRepository.Instance.InsertOrUpdate(dataSource);

            this.DialogResult = DialogResult.OK;


        }

        private void repositoryItemComboBox1_BeforeShowMenu(object sender, DevExpress.XtraEditors.Controls.BeforeShowMenuEventArgs e)
        {

        }

        /// <summary>
        /// 重置管理员密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetAdmin_Click(object sender, EventArgs e)
        {
            //string password = "123";
            try
            {
                if (ResetAdminPasswordEvent != null)
                    ResetAdminPasswordEvent(sender, e);
                //_action.ResetPassword("admin", password);
                //XtraMessageBox.Show($"密码已经重置为 {password}", "提示信息");
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 测试连接有效性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            var selected = ((ColumnView)gdDataBase.MainView).GetFocusedRow();
            if (selected == null)
            {
                XtraMessageBox.Show("请选择要测试测数据库连接");
                return;
            }

            var db = selected as DatabaseConnection;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = string.Format(connectionFormat, db.ServerIP, db.UserId, db.Password);

            try
            {
                conn.Open();
                XtraMessageBox.Show("数据库连接正确");
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("数据库连接不正确" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 删除连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            var selected = ((ColumnView)gdDataBase.MainView).GetFocusedRow();
            if (selected == null)
            {
                XtraMessageBox.Show("请选择要测试测数据库连接");
                return;
            }

            var db = selected as DatabaseConnection;

            var dataSource = this.gdDataBase.DataSource as List<DatabaseConnection>;

            this.gdDataBase.DataSource = dataSource.Remove(db);

            gdDataBase.MainView.RefreshData();
        }
    }



}
