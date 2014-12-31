using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;

namespace NLORM.Core.Test
{
    [TableName("TestTableA")]
    class TestClassA
    {
        [ColumnName("COLID")]
        [ColumnType(DbType.String,"30",false,"this is id comment")]
        public string ID { get; set; }
    }

    class TestClassB
    {
        public string ID { get; set; }

    }

	class TestClassC
	{
		[NotGenColumn()]
		public string HiddenColumn { get; set;}

		[ColumnName("COLID")]
        [ColumnType(DbType.String,"30",false,"this is id comment")]
		[NotGenColumn()]
		public string MultiAttribute { get; set;}
	}

    /// <summary>
    /// Summary description for ModelDefinitionConverterTest
    /// </summary>
    [TestClass]
    public class ModelDefinitionConverterTest
    {
        public ModelDefinitionConverterTest()
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
        public void TestGetTableName()
        {
            var mdc = new ModelDefinitionConverter();
            var md = mdc.ConverClassToModelDefinition<TestClassA>();
            string tableName = md.TableName;
            Assert.AreEqual("TestTableA", tableName);
        }

        [TestMethod]
        public void TestGetTableNameWithoutAttr()
        {
            var mdc = new ModelDefinitionConverter();
            var md = mdc.ConverClassToModelDefinition<TestClassB>();
            string tableName = md.TableName;
            Assert.AreEqual("TestClassB", tableName);
        }

        [TestMethod]
        public void TestGetColNameWithAttr()
        {
            var mdc = new ModelDefinitionConverter();
            var md = mdc.ConverClassToModelDefinition<TestClassA>();
            string idColName = md.PropertyColumnDic["ID"].ColumnName;
            Assert.AreEqual("COLID", idColName);
        }

        [TestMethod]
        public void TestGetColNameWithoutAttr()
        {
            var mdc = new ModelDefinitionConverter();
            var md = mdc.ConverClassToModelDefinition<TestClassB>();
            string idColName = md.PropertyColumnDic["ID"].ColumnName;
            Assert.AreEqual("ID", idColName);
        }

        [TestMethod]
        public void TestDoNotGetHiddenColumn()
        {
            var mdc = new ModelDefinitionConverter();
            NLORM.Core.BasicDefinitions.ModelDefinition md = mdc.ConverClassToModelDefinition<TestClassC>();
            Assert.AreEqual<int>( 0,md.PropertyColumnDic.Count);
        }

    }
}
