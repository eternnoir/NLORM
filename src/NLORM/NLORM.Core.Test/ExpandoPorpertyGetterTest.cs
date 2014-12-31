using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Utility;
using System.Dynamic;

namespace NLORM.Core.Test
{
    /// <summary>
    /// Summary description for ExpandoPorpertyGetterTest
    /// </summary>
    [TestClass]
    public class ExpandoPorpertyGetterTest
    {
        public ExpandoPorpertyGetterTest()
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
        public void TestExpandoPorpertyGetter()
        {
            var expGetter = new ExpandoPorpertyGetter();
            dynamic tester = new ExpandoObject();
            tester.ID = "id";
            tester.T2 = "t2";
            IDictionary<string,object> retDic = expGetter.GetPropertyDic(tester);
            Assert.AreEqual(retDic.Keys.Count, 2);
        }
    }
}
