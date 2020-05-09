using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Domain;
using SuspeSys.Dao.Test;
using SuspeSys.Test.WinFrom;
using SuspeSys.Dao;
using SuspeSys.Service.Impl.ProcessOrder;

namespace SuspeSys.Test
{
    [TestClass]
    public class UnitTest1
    {
       // private CatDao catDao = CatDao.Instance;
        [TestMethod]
        public void TestMethod1()
        {
           var t= decimal.Parse("0.01");
            //catDao.AddCat();
            //var cardList= catDao.QueryOver().List();
            //foreach (var m in cardList)
            //{
            //    Console.WriteLine("Name:"+m.Name+" ID:"+m.Id);
            //}
            //var card = catDao.GetById("004af0c1008d47d48c2de6a1ba56066d");
            //Console.WriteLine(card);
            //catDao.Delete("004af0c1008d47d48c2de6a1ba56066d");
            //var cardId = "004af0c1008d47d48c2de6a1ba56066d";
            //catDao.Delete(cardId);
            //var card = catDao.GetById(cardId);
            //Console.WriteLine(card);

            //var cId = "0cab6f129fb347ed8afad8fa1a7ab913";
            //catDao.LogicDelete(cId);
            //var cat = catDao.LoadById(cId);
            //Console.WriteLine(cat);
        }
        [TestMethod]
        public void TestWinForm() {
            //Form1 from = new Form1();
            //from.ShowDialog();
            var orderQuery = new ProcessOrderQueryServiceImpl();
            var t=orderQuery.GetProcessOrder2("f213e03e3dfa4ec6b2add1381a3e2c0d");
        }
    }
}
