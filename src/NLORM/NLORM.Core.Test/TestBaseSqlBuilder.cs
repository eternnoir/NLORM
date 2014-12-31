using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using NLORM.Core.Attributes;
using System.Data;

namespace NLORM.Core.Test
{
    /// <summary>
    /// Summary description for TestBaseSqlBuilder
    /// </summary>
    [TestClass]
    public class TestBaseSqlBuilder
    {
        public TestBaseSqlBuilder()
        {

        }

        public class TBaseSqlBuilder : BaseSqlBuilder
        {
            public TBaseSqlBuilder()
            {
                SqlGen = new TBaseSqlGenerator();
            }

            public override ISqlBuilder CreateOne()
            {
                return new TBaseSqlBuilder();
            }
        }

        public class TBaseSqlGenerator : BaseSqlGenerator
        {
        }

        private TestContext testContextInstance;

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

        class TestClass
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableSqlString()
        {
            var builder = new TBaseSqlBuilder();
            var resultStr =removeDoubleSpace( builder.GenCreateTableSql<TestClass>()).ToUpper();
            Assert.AreEqual(resultStr, "CREATE TABLE TESTCLASS ( ID VARCHAR(255) NOT NULL)");
        }

        class TestClassFixLen
        {
             [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableSqlString2()
        {
            var builder = new TBaseSqlBuilder();
            var resultStr = removeDoubleSpace(builder.GenCreateTableSql<TestClassFixLen>()).ToUpper();
            Assert.AreEqual(resultStr, "CREATE TABLE TESTCLASSFIXLEN ( ID VARCHAR(30) NOT NULL)");
        }

        private string removeDoubleSpace(string oriString)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            oriString = regex.Replace(oriString, @" ");
            return oriString;
        }
    }
}
