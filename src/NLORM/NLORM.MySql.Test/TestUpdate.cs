using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using NLORM.Core;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestUpdate
    /// </summary>
    [TestClass]
    public class TestUpdate
    {
        static public string connectionString;
        public TestUpdate()
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

        private void insertdata(NLORMMySqlDb db)
        {
            db.Insert<TestClassOne>(new TestClassOne() { Id = "sssss", income = 123456 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "rrrrr", income = 789012 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "fffff", income = 345678 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "lllll", income = 901234 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "alber", income = 901234 });
        }

        private void createtable(NLORMMySqlDb db)
        {
            try
            {
                db.DropTable<TestClassOne>();
            }
            catch { }
            db.CreateTable<TestClassOne>();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {

        }

        [TestMethod]
        public void TestUpdateOneRow()
        {
            var db = new NLORMMySqlDb(connectionString);
            var newobj = new TestClassOne { Id = "sssss", income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
        }

        [TestMethod]
        public void TestUpdateWithOr()
        {
            var db = new NLORMMySqlDb(connectionString);
            var newobj = new TestClassOne { income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" })
                .Or().FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" })
                .Update<TestClassOne>(new { income = 100 });
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            var itemsr = db.FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
            Assert.AreEqual(100, itemsr.income);
        }

        [TestMethod]
        public void TestUpdateOneRowUesExpando()
        {
            var db = new NLORMMySqlDb(connectionString);
            var newobj = new TestClassOne { Id = "sssss", income = 100 };
            dynamic filterObj = new ExpandoObject();
            filterObj.Id = "sssss";
            int i = db.FilterBy(FilterType.EQUAL_AND, filterObj).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, filterObj).Query<TestClassOne>()[0];
            Assert.AreEqual(100, items.income);
        }

        [TestMethod]
        public void TestUpdateWithOrUesExpando()
        {
            var db = new NLORMMySqlDb(connectionString);
            var newobj = new TestClassOne { income = 100 };

            dynamic filterObjA = new ExpandoObject();
            dynamic filterObjB = new ExpandoObject();
            filterObjA.Id = "sssss";
            filterObjB.Id = "rrrrr";

            int i = db.FilterBy(FilterType.EQUAL_AND, filterObjA)
                .Or().FilterBy(FilterType.EQUAL_AND, filterObjB)
                .Update<TestClassOne>(new { income = 100 });
            var items = db.FilterBy(FilterType.EQUAL_AND, filterObjA).Query<TestClassOne>()[0];
            var itemsr = db.FilterBy(FilterType.EQUAL_AND, filterObjB).Query<TestClassOne>()[0];
            Assert.AreEqual(100, items.income);
            Assert.AreEqual(100, itemsr.income);
        }

        [TestMethod]
        public void TestUpdateOneRowUseExpandoToUpdate()
        {
            var db = new NLORMMySqlDb(connectionString);
            dynamic newobj = new ExpandoObject();
            newobj.income = 100;
            //dynamic newobj = new TestClassOne { Id = "sssss", income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
        }

        [TestMethod]
        public void TestUpdateWithOrUseExpandoToUpdate()
        {
            var db = new NLORMMySqlDb(connectionString);
            dynamic newobjc = new ExpandoObject();
            newobjc.income = 100;
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" })
                .Or().FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" })
                .Update<TestClassOne>(newobjc);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            var itemsr = db.FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
            Assert.AreEqual(100, itemsr.income);
        }


    }
}
