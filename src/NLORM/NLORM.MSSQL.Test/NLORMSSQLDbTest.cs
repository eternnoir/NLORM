using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using NLORM.Core.Attributes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace NLORM.MSSQL.Test
{

	[TestClass]
	public class NLORMSSQLDbTest
    {
        class TestClass01
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }

            public string name { get; set; }
        }

        class TestClassDecimal
        {
            [ColumnType(DbType.Decimal, "20,10", false, "decimal test")]
            public Int32 DID { get; set; }

            [ColumnType(DbType.Decimal, "10", true, "decimal test")]
            public Int32 DDID { get; set; }
        }
        class TestClass2
        {
            public string ID { get; set; }
        }

        class TestClassbit
        {
            [ColumnType(DbType.Boolean, "1", false, "bit test")]
            public Boolean BTAG { get; set; }

            [ColumnType(DbType.Boolean, "1", true, "bit test")]
            public Boolean BBTAG { get; set; }
        }
        public static string ConnectionString = @"";
        public static string masterdb = @"";

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

		[TestMethod]
        public void TestCreateTableWithoutDef()
        {
			INLORMDb mssqlDb = null;
			mssqlDb = new NLORMMSSQLDb( ConnectionString);
			mssqlDb.CreateTable<TestClass2>();
        }

		[TestMethod]
		public void TestCreateTable()
		{
			INLORMDb db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClass01>();
		}

		[TestMethod]
		public void TestInsertAlotItems()
		{
			INLORMDb db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClass01>();
			for ( int i = 0; i < 1000; i++)
			{
				db.Insert<TestClass01>( new TestClass01(){ ID = @"0" + i.ToString(), name = @"00" + i.ToString() });
			}
			
			var result = db.Query<TestClass01>(@"SELECT * FROM TestClass01");
			Assert.AreEqual( result.Count(), 1000);
		}

		[TestMethod]
		public void TestDropTable()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClass01>();
			db.DropTable<TestClass01>();
		}

		[TestMethod]
		public void TestQueryMethod1()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( ConnectionString);
			this.TestInsertAlotItems();
			var result = db.Query<TestClass01>( @"SELECT * FROM TestClass01 where ID = @ID", new TestClass01(){ ID = @"01"});
			Assert.AreEqual( 1, result.Count());
		}

		[TestMethod]
		public void TestQueryMethod2()
		{
			INLORMDb db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClass01>();
			db.Insert<TestClass01>( new TestClass01(){ ID = @"11", name = @"albert"});
			db.Insert<TestClass01>( new TestClass01(){ ID = @"22", name = @"star"});
			var result = db.FilterBy( FilterType.EQUAL_AND, new { name = "albert"} ).Query<TestClass01>();

			Assert.AreEqual( 1, result.Count() );
		}

		[TestMethod]
		public void TestCreatDecimalType()
		{
			INLORMDb db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassDecimal>();

			db.DropTable<TestClassDecimal>();
		}

		[TestMethod]
		public void TestCreatbitType()
		{
			INLORMDb db = new NLORMMSSQLDb( ConnectionString);
			db.CreateTable<TestClassbit>();

			db.DropTable<TestClassbit>();
		}
	}
}