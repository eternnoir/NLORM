using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.MySql;
using NLORM.Core;
using NLORM.Core.Attributes;


namespace NLORM.MySql.Test
{
    class TestClass
    {
        [ColumnType(DbType.String, "30", false, "this is id comment")]
        public string ID { get; set; }

    }

    class TestClass2
    {
        public string ID { get; set; }

    }

    /// <summary>
    /// Summary description for MySqlDbTest
    /// </summary>
    [TestClass]
    public class MySqlDbTest
    {
        static public string connectionString ;
        public MySqlDbTest()
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.DropTable<TestClass>();
                db.DropTable<TestClass2>();
                db.Close();
            }
            catch { }
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.DropTable<TestClass>();
                db.DropTable<TestClass2>();
                db.Close();
            }
            catch { }
        }

        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup() {}
        #endregion

        [TestMethod]
        public void TestConnect()
        {
            
            var db = new NLORMMySqlDb( connectionString);
            try
            {
                db.Open();
            }
            catch ( MySqlException  ex)
            {
                Assert.IsTrue( ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestCreateTable()
        {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.CreateTable<TestClass>();
                db.Close();
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }


        [TestMethod]
        public void TestDropTable()
        {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.DropTable<TestClass>();
                db.Close();
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestCreateTableWithoutAttr()
        {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.CreateTable<TestClass2>();
                db.Close();
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestDropTableWithoutAttr()
        {
            try
            {
                var db = new NLORMMySqlDb(connectionString);
                db.DropTable<TestClass2>();
                db.Close();
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestInsertClass1()
        {
            try
            {
                TestDropTable();
            }
            catch { }
            var c1 = new TestClass();
            c1.ID = "5555";
            try
            {
                TestCreateTable();
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.Insert<TestClass>(c1);
                var result = Dbc.Query<TestClass>("SELECT * FROM  TestClass");
                Assert.AreEqual(result.Count(), 1);
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestInsertClass1Muti()
        {
            try
            {
                TestDropTable();
            }
            catch { }
            TestCreateTable();
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                for (int i = 0; i < 10; i++)
                {
                    var c1 = new TestClass();
                    c1.ID = "id" + i.ToString();
                    Dbc.Insert<TestClass>(c1);
                }
                var result = Dbc.Query<TestClass>("SELECT * FROM  TestClass");
                Assert.AreEqual(result.Count(), 10);
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestInsertClass2()
        {
            try
            {
                TestDropTableWithoutAttr();
            }
            catch { }
            var c1 = new TestClass2();
            c1.ID = "5555";
            try
            {
                TestCreateTableWithoutAttr();
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.Insert<TestClass2>(c1);
                var result = Dbc.Query<TestClass2>("SELECT * FROM  TestClass2");
                Assert.AreEqual(result.Count(), 1);
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }

        [TestMethod]
        public void TestInsertClass2Muti()
        {
            try
            {
                TestDropTableWithoutAttr();
            }
            catch { }
            try
            {
                TestCreateTableWithoutAttr();
                var Dbc = new NLORMMySqlDb(connectionString);
                for (int i = 0; i < 10; i++)
                {
                    var c1 = new TestClass2();
                    c1.ID = "id" + i.ToString();
                    Dbc.Insert<TestClass2>(c1);
                }
                var result = Dbc.Query<TestClass2>("SELECT * FROM  TestClass2");
                Assert.AreEqual(result.Count(), 10);
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex is MySqlException);
            }
        }
    }
}
