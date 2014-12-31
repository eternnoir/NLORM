using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace NLORM.Core.Test
{
    /// <summary>
    /// Summary description for TestException
    /// </summary>
    [TestClass]
    public class TestException
    {
        public TestException()
        {
            //
            // TODO: Add constructor logic here
            //
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

        [TestMethod]
        public void TestDBTypeGuidGen()
        {
            var cfd = createFakeCfdByDbType(DbType.Guid);
            var cfdDic = new Dictionary<string, BasicDefinitions.ColumnFieldDefinition>();
            cfdDic.Add("test1", cfd);
            var md = new NLORM.Core.BasicDefinitions.ModelDefinition("Test", cfdDic);
            var sqlGen = new BaseSqlGenerator();
            try
            {
                sqlGen.GenCreateTableSql(md);
            }
            catch (NLORM.Core.Exceptions.NLORMException nle)
            {
                Assert.AreEqual(nle.ErrorCode, "SG");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestDBTypeUInt16Gen()
        {
            var cfd = createFakeCfdByDbType(DbType.UInt16);
            var cfdDic = new Dictionary<string, BasicDefinitions.ColumnFieldDefinition>();
            cfdDic.Add("test1", cfd);
            var md = new NLORM.Core.BasicDefinitions.ModelDefinition("Test", cfdDic);
            var sqlGen = new BaseSqlGenerator();
            try
            {
                sqlGen.GenCreateTableSql(md);
            }
            catch (NLORM.Core.Exceptions.NLORMException nle)
            {
                Assert.AreEqual(nle.ErrorCode, "SG");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        private BasicDefinitions.ColumnFieldDefinition createFakeCfdByDbType(DbType dbType)
        {
            var cfd = new NLORM.Core.BasicDefinitions.ColumnFieldDefinition();
            cfd.FieldType = dbType;
            return cfd;
        }
    }
}
