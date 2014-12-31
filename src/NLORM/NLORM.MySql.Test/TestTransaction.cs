using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestTransaction
    /// </summary>
    [TestClass]
    public class TestTransaction
    {
        static public string connectionString ;
        public TestTransaction()
        {
            connectionString = "Server=test.mysql.nlorm;Database=nlorm;uid=admin;pwd=1qaz;";
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


        [TestInitialize()]
        public void TestInitialize()
        {
            var dbc = new NLORMMySqlDb(connectionString);
            Init(dbc);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            try
            {
            }
            finally
            {

            }
        }

        private void Init(INLORMDb db)
        {
            try
            {
                db.DropTable<TestClassUser>();
            }
            catch { }
            db.CreateTable<TestClassUser>();
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
            var mySqlDbc = new NLORMMySqlDb(connectionString);
            var trans = mySqlDbc.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            mySqlDbc.Insert<TestClassUser>(testObj);
            trans.Commit();
            mySqlDbc.Close();
            mySqlDbc = new NLORMMySqlDb(connectionString);
            var selLis = mySqlDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 1);

        }

        [TestMethod]
        public void TestTransactionRollback()
        {
            var mySqlDbc = new NLORMMySqlDb(connectionString);
            var trans = mySqlDbc.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            mySqlDbc.Insert<TestClassUser>(testObj);
            trans.Rollback();
            mySqlDbc.Close();
            mySqlDbc = new NLORMMySqlDb(connectionString);
            var selLis = mySqlDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);

        }


        [TestMethod]
        public void TestInsertUserClassMutiCommit()
        {
            var mySqlDbc = new NLORMMySqlDb(connectionString);
            var trans = mySqlDbc.BeginTransaction();
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
                mySqlDbc.Insert<TestClassUser>(user);
            }
            trans.Commit();
            mySqlDbc.Close();
            mySqlDbc = new NLORMMySqlDb(connectionString);
            var selLis = mySqlDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 30);
        }

        [TestMethod]
        public void TestInsertUserClassMutiRollback()
        {
            var mySqlDbc = new NLORMMySqlDb(connectionString);
            var trans = mySqlDbc.BeginTransaction();
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
                mySqlDbc.Insert<TestClassUser>(user);
            }
            trans.Rollback();
            mySqlDbc.Close();
            mySqlDbc = new NLORMMySqlDb(connectionString);
            var selLis = mySqlDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);
        }
    }
}
