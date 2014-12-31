using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestInsert
    {
        private string ConnectionString = NLORMSSQLDbTest.ConnectionString;

        [TestInitialize()]
        public void TestInitialize()
        {
            var db = new NLORMMSSQLDb(NLORMSSQLDbTest.masterdb);
            IDbCommand cmd = db.GetDbConnection().CreateCommand();
            cmd.CommandText = @"CREATE DATABASE TestORM";
            try
            {
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
            finally
            {

            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            var db = new NLORMMSSQLDb(NLORMSSQLDbTest.masterdb);
            IDbCommand cmd = db.GetDbConnection().CreateCommand();
            cmd.CommandText = @"DROP DATABASE TestORM";
            IDbCommand closecmd = db.GetDbConnection().CreateCommand();
            closecmd.CommandText = @"alter database TestORM set single_user with rollback immediate";

            try
            {
                db.Open();
                closecmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                db.Close();
            }
            finally
            {

            }
        }

        private class TestClassUser
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime CreateTime { get; set; }
        }

        [TestMethod]
        public void TestInsertUserClass()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();

            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;

            db.Insert<TestClassUser>(testObj);
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(),1);
            var selUser = selLis.ToArray()[0];
            Assert.AreEqual(testObj.ID,selUser.ID);
            Assert.AreEqual(testObj.Name,selUser.Name);
            //Assert.AreEqual(testObj.CreateTime,selUser.CreateTime);
        }

        [TestMethod]
        public void TestInsertUserClassMuti()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();

            var insertList = new List<TestClassUser>();

            for (int i = 0; i < 30; i++)
            {
                var testObj = new TestClassUser {ID = i, Name = "Name " + i, 
                    CreateTime = DateTime.Now.AddDays(i)};
                insertList.Add(testObj);
            }
            foreach (TestClassUser user in insertList)
            {
                db.Insert<TestClassUser>(user); 
            }
            
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 30);
        }
    }
}
