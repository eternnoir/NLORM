using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;
using System.Data;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestCreateTable
    /// </summary>
    [TestClass]
    public class TestCreateTable
    {
        static public string connectionString ;
        public TestCreateTable()
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        class TestClassOnlyString
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyString()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyString>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyString>();
        }

        class TestClassOnlyStringWCfd
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyStringWithCfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyStringWCfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyStringWCfd>();
        }

        class TestClassOnlyInt
        {
            public int ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyInt>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyInt>();
        }

        class TestClassOnlyIntWCfd
        {
            [ColumnType(DbType.Int32, "", false, "this is id comment")]
            public int ID { get; set; }

            [ColumnType(DbType.Int16, "", false, "this is id comment")]
            public int ID2 { get; set; }
        }


        [TestMethod]
        public void TestCreateTableOnlyIntWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyIntWCfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyIntWCfd>();
        }

        class TestClassOnlyDateTime
        {
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTime()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyDateTime>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyDateTime>();
        }

        class TestClassOnlyDateTimeWCfd
        {
            [ColumnType(DbType.DateTime, "", false, "this is time comment")]
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTimeWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyDateTimeWCfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyDateTimeWCfd>();
        }

        class TestClassOnlyTimeWcfd
        {
            [ColumnType(DbType.Time, "", false, "this is time comment")]
            public TimeSpan maintaintime { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyTimeWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyTimeWcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyTimeWcfd>();
        }

        class TestClassOnlyTime
        {
            public TimeSpan maintaintime { get; set; }
        }

        [TestMethod]
        public void TestCreateTabkeOnlyTime()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyTime>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyTime>();
        }

        class TestClassOnlyFloatWcfd
        {
            [ColumnType(DbType.Double, "", false, "this is double comment")]
            public Double revenue { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyFloatWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyFloatWcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyFloatWcfd>();
        }

        class TestClassOnlyFloat
        {
            public Double revenue { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyFloat()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyFloat>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyFloat>();
        }

        class TestClassOnlySingleWcfd
        {
            public Single reserve { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlySingleWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlySingleWcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlySingleWcfd>();
        }

        class TestClassOnlySingle
        {
            public Single reserve { get; set; }
        }

        [TestMethod]
        public void TestCreatTableOnlySingle()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlySingle>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlySingle>();
        }

        class TestClassOnlyInt64Wcfd
        {
            [ColumnType(DbType.Int64, "", false, " this is int64 comment")]
            public Int64 bigbrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt64Wcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyInt64Wcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyInt64Wcfd>();
        }

        class TestclassOnlyInt64
        {
            public Int64 bigbrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt64()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestclassOnlyInt64>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestclassOnlyInt64>();
        }

        class TestClassOnlyInt16Wcfd
        {
            [ColumnType(DbType.Int16, "", false, " this is int16 comment")]
            public Int16 littlebrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt16Wcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyInt16Wcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyInt16Wcfd>();
        }

        class TestClassOnlyInt16
        {
            public Int16 littlebrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt16()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyInt16>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyInt16>();
        }

        class TestClassOnlyTinyintWcfd
        {
            [ColumnType(DbType.Byte, "", false, " this is tinyint comment")]
            public Byte tinyintcolumn { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyTinyintWcfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyTinyintWcfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyTinyintWcfd>();
        }

        class TestClassOnlyTinyint
        {
            public Byte tinyintcolumn { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyTinyint()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyTinyint>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyTinyint>();
        }

    }
}
