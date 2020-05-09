using SuspeSys.Domain;
using SuspeSys.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action
{
    public class TestAction
    {
        public void TestAddEmployee()
        {
            CatService.Instance.AddEmployee(new Employee()
            {
                RealName = "zhangsan",
                Code = "0001",
                Password = "0000",
                Mobile="111",
                Phone="22",
                InsertDateTime=DateTime.Now,
                StartingDate= DateTime.Now,
                InsertUser =Guid.NewGuid().ToString().Substring(0,32)
            });
        }
    }
}
