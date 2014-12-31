using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestUpdate
    {
        string connectionString = NLORMSSQLDbTest.ConnectionString;
        public TestUpdate()
        {
        }

        class TestClassOne
        {
            public string Id { get; set;}

            public int income { get; set;}
        }
        
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

        private void createtable(INLORMDb db)
        {
            try
            {
                //db.DropTable<TestClassOne>();
                db.CreateTable<TestClassOne>();
            }
            catch (Exception)
            {
            }
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
        public void TestUpdateOneRow()
        {
            var db = new NLORMMSSQLDb(connectionString);
            var oldobj = new TestClassOne(){ Id= "sssss", income = 20};
            var newobj = new TestClassOne { Id = "sssss", income = 100 };
            db.CreateTable<TestClassOne>();
            db.Insert<TestClassOne>(oldobj);
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
        }
    }
}
