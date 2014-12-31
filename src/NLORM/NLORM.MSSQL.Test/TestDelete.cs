using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestDelete
    {
        public class TestClassOne
        {
            public string Id { get; set; }

            public int income { get; set; }
        }

        private string connectionString = NLORMSSQLDbTest.ConnectionString;

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

        private void insertdata(INLORMDb db)
        {
            db.CreateTable<TestClassOne>();
            db.Insert<TestClassOne>(new TestClassOne() { Id = "sssss", income = 123456 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "rrrrr", income = 789012 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "fffff", income = 345678 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "lllll", income = 901234 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "alber", income = 901234 });
        }

        [TestMethod]
        public void TestDeleteOneRecord()
        {
            var db = new NLORMMSSQLDb(connectionString);
            insertdata(db);
            int deletedcount = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Delete<TestClassOne>();
            Assert.AreEqual(deletedcount, 1);
        }

        [TestMethod]
        public void TestDeleteTwoRecord()
        {
            var db = new NLORMMSSQLDb(connectionString);
            insertdata(db);
            db.FilterBy(FilterType.EQUAL_AND, new { income = 901234 }).Delete<TestClassOne>();
            var items = db.Query<TestClassOne>();
            Assert.AreEqual(3, items.Count());
        }

        [TestMethod]
        public void TestDeleteAllRecords()
        {
            var db = new NLORMMSSQLDb(connectionString);
            insertdata(db);
            int totalcount = db.Query<TestClassOne>().Count();
            int dc = db.Delete<TestClassOne>();

            var items = db.Query<TestClassOne>();
            Assert.AreEqual(dc, totalcount);
        }
    }
}
