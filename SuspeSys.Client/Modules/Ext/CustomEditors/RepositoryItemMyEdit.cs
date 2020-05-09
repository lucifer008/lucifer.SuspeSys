using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.Ext.CustomEditors
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemMyEdit : RepositoryItemTextEdit
    {
        static RepositoryItemMyEdit()
        {
            Register();
        }
        public RepositoryItemMyEdit() { }

        internal const string EditorName = "MyEdit";

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MyEdit),
                typeof(RepositoryItemMyEdit), typeof(DevExpress.XtraEditors.ViewInfo.TextEditViewInfo),
                new DevExpress.XtraEditors.Drawing.TextEditPainter(), true, null, typeof(DevExpress.Accessibility.TextEditAccessible)));
        }
        public override string EditorTypeName
        {
            get { return EditorName; }
        }
    }
    public class MyEdit : TextEdit
    {
        static MyEdit()
        {
            RepositoryItemMyEdit.Register();
        }
        public MyEdit() { }

        public string DisplayText { set; get; }

        public override string EditorTypeName
        {
            get { return RepositoryItemMyEdit.EditorName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMyEdit Properties
        {
            get { return base.Properties as RepositoryItemMyEdit; }
        }

        //protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)  
        //{  
        //    ShowPopupForm();  
        //    base.OnClickButton(buttonInfo);  
        //}  
        //protected virtual void ShowPopupForm()  
        //{  
        //    using (Form form = new Form())  
        //    {  
        //        form.StartPosition = FormStartPosition.Manual;  
        //        form.Location = this.PointToScreen(new Point(0, Height));  
        //        form.ShowDialog();  
        //    }  
        //}  
    }


}
