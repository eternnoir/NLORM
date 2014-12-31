using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestTransaction
    {
        private string ConnectionString = NLORMSSQLDbTest.ConnectionString;

        [TestInitialize()]
        public void TestInitialize()
        {
            var db = new NLORMMSSQLDb(NLORMSSQLDbTest.masterdb);
            System.Data.IDbCommand cmd = db.GetDbConnection().CreateCommand();
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
            System.Data.IDbCommand cmd = db.GetDbConnection().CreateCommand();
            cmd.CommandText = @"DROP DATABASE TestORM";
            System.Data.IDbCommand closecmd = db.GetDbConnection().CreateCommand();
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


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private class TestClassUser
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime CreateTime { get; set; }
        }

        [TestMethod]
        public void TestTransactionCommit()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();
            var trans = db.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            db.Insert<TestClassUser>(testObj);
            trans.Commit();
            db.Close();
            db = new NLORMMSSQLDb( ConnectionString);
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 1);

        }

        [TestMethod]
        public void TestTransactionRollback()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();
            var trans = db.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            db.Insert<TestClassUser>(testObj);
            trans.Rollback();
            db.Close();
            db = new NLORMMSSQLDb( ConnectionString);
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);

        }


        [TestMethod]
        public void TestInsertUserClassMutiCommit()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();
            var trans = db.BeginTransaction();
            var insertList = new List<TestClassUser>();

            for (int i = 0; i < 30; i++)
            {
                var testObj = new TestClassUser
                {
                    ID = i,
                    Name = "Name " + i,
                    CreateTime = DateTime.Now.AddDays(i)
                };
                insertList.Add(testObj);
            }
            foreach (TestClassUser user in insertList)
            {
                db.Insert<TestClassUser>(user);
            }
            trans.Commit();
            db.Close();
            db = new NLORMMSSQLDb( ConnectionString);
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 30);
        }

        [TestMethod]
        public void TestInsertUserClassMutiRollback()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassUser>();
            var trans = db.BeginTransaction();
            var insertList = new List<TestClassUser>();

            for (int i = 0; i < 30; i++)
            {
                var testObj = new TestClassUser
                {
                    ID = i,
                    Name = "Name " + i,
                    CreateTime = DateTime.Now.AddDays(i)
                };
                insertList.Add(testObj);
            }
            foreach (TestClassUser user in insertList)
            {
                db.Insert<TestClassUser>(user);
            }
            trans.Rollback();
            db.Close();
            db = new NLORMMSSQLDb( ConnectionString);
            var selLis = db.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);
        }
    }
}
