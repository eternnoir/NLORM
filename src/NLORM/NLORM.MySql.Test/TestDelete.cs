using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestDelete
    /// </summary>
    [TestClass]
    public class TestDelete
    {
        static public string connectionString ;
        public TestDelete()
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

        class TestClassOne
        {
            public string Id { get; set; }

            public int income { get; set; }
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var dbc = new NLORMMySqlDb(connectionString);
            createtable(dbc);
            insertdata(dbc);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }


        private void createtable(INLORMDb db)
        {
            try
            {
                db.DropTable<TestClassOne>();
            }
            catch { }
            db.CreateTable<TestClassOne>();
        }
        private void insertdata(INLORMDb db)
        {
            db.Insert<TestClassOne>(new TestClassOne() { Id = "sssss", income = 123456 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "rrrrr", income = 789012 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "fffff", income = 345678 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "lllll", income = 901234 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "alber", income = 901234 });
        }

        [TestMethod]
        public void TestDeleteOneRecord()
        {
            var db = new NLORMMySqlDb(connectionString);
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Delete<TestClassOne>();
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>();
            Assert.AreEqual(0, items.Count());
        }

        [TestMethod]
        public void TestDeleteTwoRecord()
        {
            var db = new NLORMMySqlDb(connectionString);
            db.FilterBy(FilterType.EQUAL_AND, new { income = 901234 }).Delete<TestClassOne>();
            var items = db.Query<TestClassOne>();
            Assert.AreEqual(3, items.Count());
        }

        [TestMethod]
        public void TestDeleteAllRecords()
        {
            var db = new NLORMMySqlDb(connectionString);
            int totalcount = db.Query<TestClassOne>().Count();
            int dc = db.Delete<TestClassOne>();

            var items = db.Query<TestClassOne>();
            Assert.AreEqual(dc, totalcount);
        }
    }
}
