using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestCreateTable
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

        class TestClassOnlyString
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyString()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyString>();
        }

        class TestClassOnlyStringWCfd
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }


        [TestMethod]
        public void TestCreateTableOnlyStringWithCfd()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyStringWCfd>();
        }

        class TestClassOnlyInt
        {
            public int ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyInt>();
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
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyIntWCfd>();
        }

        class TestClassOnlyDateTime
        {
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTime()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyDateTime>();
        }

        class TestClassOnlyDateTimeWCfd
        {
            [ColumnType(DbType.DateTime, "", false, "this is time comment")]
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTimeWcfd()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyDateTimeWCfd>();
        }

		class TestClassOnlyTimeWcfd
		{
			[ColumnType(DbType.Time, "", false, "this is time comment")]
			public TimeSpan maintaintime { get; set;}
		}

		[TestMethod]
		public void TestCreateTableOnlyTimeWcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlyTimeWcfd>();
		}

        class TestClassOnlyTime
        {
            public TimeSpan maintaintime { get; set;}
        }

        [TestMethod]
        public void TestCreateTabkeOnlyTime()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyTime>();
        }

		class TestClassOnlyFloatWcfd
		{
			[ColumnType(DbType.Double,"", false, "this is double comment")]
			public Double revenue { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyFloatWcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlyFloatWcfd>();
		}

        class TestClassOnlyFloat
        {
            public Double revenue { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyFloat()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyFloat>();
        }

		class TestClassOnlySingleWcfd
		{
			public Single reserve { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlySingleWcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlySingleWcfd>();
		}

        class TestClassOnlySingle
        {
            public Single reserve { get; set; }
        }

        [TestMethod]
        public void TestCreatTableOnlySingle()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlySingle>();
        }

		class TestClassOnlyInt64Wcfd
		{
            [ColumnType(DbType.Int64, "", false, " this is int64 comment")]
			public Int64 bigbrother{ get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyInt64Wcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlyInt64Wcfd>();
		}

        class TestclassOnlyInt64
        {
            public Int64 bigbrother { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt64()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestclassOnlyInt64>();
        }

		class TestClassOnlyInt16Wcfd
		{
            [ColumnType(DbType.Int16, "", false, " this is int16 comment")] 
			public Int16 littlebrother { get; set;}
		}

		[TestMethod]
		public void TestCreateTableOnlyInt16Wcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlyInt16Wcfd>();
		}

        class TestClassOnlyInt16
        {
            public Int16 littlebrother { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyInt16()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyInt16>();
        }

		class TestClassOnlyTinyintWcfd
		{
            [ColumnType(DbType.Byte, "", false, " this is tinyint comment")]
			public Byte tinyintcolumn { get; set; }
		}

		[TestMethod]
		public void TestCreateTableOnlyTinyintWcfd()
		{
			var db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassOnlyTinyintWcfd>();
		}

        class TestClassOnlyTinyint
        {
            public Byte tinyintcolumn { get; set;}
        }

        [TestMethod]
        public void TestCreateTableOnlyTinyint()
        {
            var db = new NLORMMSSQLDb( ConnectionString);
            db.CreateTable<TestClassOnlyTinyint>();
        }
    }
}
