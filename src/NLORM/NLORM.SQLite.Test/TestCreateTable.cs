using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;

namespace NLORM.SQLite.Test
{
    /// <summary>
    /// Summary description for TestCreateTable
    /// </summary>
    [TestClass]
    public class TestCreateTable
    {
        string connectionString;
        private static string filePath;
        public TestCreateTable()
        {
            filePath = "C:\\test.sqlite";
            connectionString = "Data Source=" + filePath;
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

        class TestClassOnlyString
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyString()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyString>();
        }

        class TestClassOnlyStringWCfd
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }


        [TestMethod]
        public void TestCreateTableOnlyStringWithCfd()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyStringWCfd>();
        }

        class TestClassOnlyInt
        {
            public int ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyInt>();
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
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyIntWCfd>();
        }

        class TestClassOnlyDateTime
        {
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTime()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyDateTime>();
        }

        class TestClassOnlyDateTimeWCfd
        {
            [ColumnType(DbType.DateTime, "", false, "this is time comment")]
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTimeWcfd()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyDateTimeWCfd>();
        }

		class TestClassOnlyTimeWcfd
		{
			[ColumnType(DbType.Time, "", false, "this is time comment")]
			public TimeSpan maintaintime { get; set;}
		}

		[TestMethod]
		public void TestCreateTableOnlyTimeWcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb(connectionString);
			sqliteDbc.CreateTable<TestClassOnlyTimeWcfd>();
		}

        class TestClassOnlyTime
        {
            public TimeSpan maintaintime { get; set;}
        }

        [TestMethod]
        public void TestCreateTabkeOnlyTime()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestClassOnlyTime>();
        }

		class TestClassOnlyFloatWcfd
		{
			[ColumnType(DbType.Double,"", false, "this is double comment")]
			public Double revenue { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyFloatWcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb( connectionString);
			sqliteDbc.CreateTable<TestClassOnlyFloatWcfd>();
		}

        class TestClassOnlyFloat
        {
            public Double revenue { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyFloat()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestClassOnlyFloat>();
        }

		class TestClassOnlySingleWcfd
		{
			public Single reserve { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlySingleWcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb( connectionString);
			sqliteDbc.CreateTable<TestClassOnlySingleWcfd>();
		}

        class TestClassOnlySingle
        {
            public Single reserve { get; set; }
        }

        [TestMethod]
        public void TestCreatTableOnlySingle()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestClassOnlySingle>();
        }

		class TestClassOnlyInt64Wcfd
		{
            [ColumnType(DbType.Int64, "", false, " this is int64 comment")]
			public Int64 bigbrother{ get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyInt64Wcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb( connectionString);
			sqliteDbc.CreateTable<TestClassOnlyInt64Wcfd>();
		}

        class TestclassOnlyInt64
        {
            public Int64 bigbrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt64()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestclassOnlyInt64>();
        }

		class TestClassOnlyInt16Wcfd
		{
            [ColumnType(DbType.Int16, "", false, " this is int16 comment")] 
			public Int16 littlebrother { get; set;}
		}

		[TestMethod]
		public void TestCreateTableOnlyInt16Wcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb( connectionString);
			sqliteDbc.CreateTable<TestClassOnlyInt16Wcfd>();
		}

        class TestClassOnlyInt16
        {
            public Int16 littlebrother { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyInt16()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestClassOnlyInt16>();
        }

		class TestClassOnlyTinyintWcfd
		{
            [ColumnType(DbType.Byte, "", false, " this is tinyint comment")]
			public Byte tinyintcolumn { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyTinyintWcfd()
		{
			var sqliteDbc = new NLORMSQLiteDb( connectionString);
			sqliteDbc.CreateTable<TestClassOnlyTinyintWcfd>();
		}

        class TestClassOnlyTinyint
        {
            public Byte tinyintcolumn { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyTinyint()
        {
            var sqliteDbc = new NLORMSQLiteDb( connectionString);
            sqliteDbc.CreateTable<TestClassOnlyTinyint>();
        }
    }
}
