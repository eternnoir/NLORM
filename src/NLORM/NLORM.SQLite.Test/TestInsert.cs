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
    public class TestInsert
    {
        string connectionString;
        private static string filePath;
        public TestInsert()
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
        public void TestInsertUserClass()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();

            var testObj = new TestClassUser();
            testObj.ID = 1;
            testObj.Name = "Name " + 1;
            testObj.CreateTime = DateTime.Now;

            sqliteDbc.Insert<TestClassUser>(testObj);
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(),1);
            var selUser = selLis.ToArray()[0];
            Assert.AreEqual(testObj.ID,selUser.ID);
            Assert.AreEqual(testObj.Name,selUser.Name);
            Assert.AreEqual(testObj.CreateTime,selUser.CreateTime);
        }

        [TestMethod]
        public void TestInsertUserClassMuti()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassUser>();

            var insertList = new List<TestClassUser>();

            for (int i = 0; i < 30; i++)
            {
                var testObj = new TestClassUser {ID = i, Name = "Name " + i, 
                    CreateTime = DateTime.Now.AddDays(i)};
                insertList.Add(testObj);
            }
            foreach (TestClassUser user in insertList)
            {
                sqliteDbc.Insert<TestClassUser>(user); 
            }
            
            var selLis = sqliteDbc.Query<TestClassUser>();
            Assert.AreEqual(selLis.Count(), 30);
        }
    }
}
