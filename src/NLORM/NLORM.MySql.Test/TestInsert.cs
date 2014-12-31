using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestInsert
    /// </summary>
    [TestClass]
    public class TestInsert
    {
        static public string connectionString ;
        public TestInsert()
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

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var dbc = new NLORMMySqlDb(connectionString);
            Init(dbc);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        private void Init(INLORMDb db)
        {
            try
            {
                db.DropTable<TestClassUser>();
            }
            catch { }
            db.CreateTable<TestClassUser>();
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
             var MySqlDbc = new NLORMMySqlDb(connectionString);

             var testObj = new TestClassUser();
             testObj.ID = 1;
             testObj.Name = "Name " + 1;
             testObj.CreateTime = DateTime.Now;

             MySqlDbc.Insert<TestClassUser>(testObj);
             var selLis = MySqlDbc.Query<TestClassUser>();
             Assert.AreEqual(selLis.Count(), 1);
             var selUser = selLis.ToArray()[0];
             Assert.AreEqual(testObj.ID, selUser.ID);
             Assert.AreEqual(testObj.Name, selUser.Name);
             Assert.AreEqual(testObj.CreateTime.ToString(), selUser.CreateTime.ToString());
         }

         [TestMethod]
         public void TestInsertUserClassMuti()
         {
             var MySqlDbc = new NLORMMySqlDb(connectionString);

             var insertList = new List<TestClassUser>();

             for (int i = 0; i < 30; i++)
             {
                 var testObj = new TestClassUser
                 {
                     ID = i,
                     Name = "Name " + i,
                     CreateTime = DateTime.Now.AddDays(i)
                 };
                 insertList.Add(testObj);
             }
             foreach (TestClassUser user in insertList)
             {
                 MySqlDbc.Insert<TestClassUser>(user);
             }

             var selLis = MySqlDbc.Query<TestClassUser>();
             Assert.AreEqual(selLis.Count(), 30);
         }
    }
}
