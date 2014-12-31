using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLORM.SQLite.Test
{
    /// <summary>
    /// Summary description for TestInsert
    /// </summary>
    [TestClass]
    public class TestTransaction
    {
        string connectionString;
        private static string filePath;
        public TestTransaction()
        {
            filePath = "C:\\test.sqlite";
            connectionString = "Data Source=" + filePath;
        }


        [TestInitialize()]
        public void TestInitialize()
        {
            try
            {
                File.Delete(filePath);
            }
            finally
            {

            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            try
            {
                File.Delete(filePath);
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
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();
            var trans = sqliteDbc.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            sqliteDbc.Insert<TestClassUser>(testObj);
            trans.Commit();
            sqliteDbc.Close();
            sqliteDbc = new NLORMSQLiteDb(connectionString);
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 1);

        }

        [TestMethod]
        public void TestTransactionRollback()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();
            var trans = sqliteDbc.BeginTransaction();
            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;
            sqliteDbc.Insert<TestClassUser>(testObj);
            trans.Rollback();
            sqliteDbc.Close();
            sqliteDbc = new NLORMSQLiteDb(connectionString);
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);

        }


        [TestMethod]
        public void TestInsertUserClassMutiCommit()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();
            var trans = sqliteDbc.BeginTransaction();
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
                sqliteDbc.Insert<TestClassUser>(user);
            }
            trans.Commit();
            sqliteDbc.Close();
            sqliteDbc = new NLORMSQLiteDb(connectionString);
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 30);
        }

        [TestMethod]
        public void TestInsertUserClassMutiRollback()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();
            var trans = sqliteDbc.BeginTransaction();
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
                sqliteDbc.Insert<TestClassUser>(user);
            }
            trans.Rollback();
            sqliteDbc.Close();
            sqliteDbc = new NLORMSQLiteDb(connectionString);
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 0);
        }
    }
}
