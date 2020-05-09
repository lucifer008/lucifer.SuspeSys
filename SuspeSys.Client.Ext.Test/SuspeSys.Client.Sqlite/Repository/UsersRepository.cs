using SuspeSys.Client.Sqlite.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Repository
{
    public class UsersRepository : Repository<Users>
    {
        private static UsersRepository _UsersRepository = new UsersRepository();
        private UsersRepository()
        {
        }

        public static UsersRepository Instance
        {
            get { return _UsersRepository; }
        }

        public void InsertOrUpdate(Users user)
        {
            //判断用户名是否存在，不存在添加，存在，更新
            var userDB = base.GetBySql("SELECT * FROM USERS WHERE UserName = @UserName", new { UserName = user.UserName });
            if (userDB == null)
            {
                //添加
                user.Id = Guid.NewGuid().ToString();
                user.CreatedDate = DateTime.Now;

                base.Insert(user);
            }
            else
            {
                user.Id = userDB.Id;
                user.ModifiedDate = DateTime.Now;

                //如果不记录用户名，删除当前记录
                if (user.SaveUserName == false)
                {
                    base.Delete(userDB);
                }
                else
                {
                    base.Update(user);
                }

            }
        }
    }
}
