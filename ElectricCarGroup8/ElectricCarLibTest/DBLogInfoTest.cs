using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBLogInfoTest
    /// </summary>
    [TestClass]
    public class DBLogInfoTest
    {
        IDLogInfo dbLogInfo;
        IDCustomer dbCustomer;
        IDDiscountGroup dbDiscountGroup;

        public DBLogInfoTest()
        {
            //
            // TODO: Add constructor logic here
            //
            dbLogInfo = new DLogInfo();
            dbCustomer = new DCustomer();
            dbDiscountGroup = new DDiscountGroup();
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void logInfoCRUD()
        {
            // prerequisites
            int dgId = dbDiscountGroup.addNewRecord("zidaci", -100);
            MDiscountGroup dg = dbDiscountGroup.getRecord(dgId, false);
            int custId = dbCustomer.addNewRecord("jozko", "mkrvicka", "nema", "vsade", "luxus", "bez", dg, "nikdaj");

            int liId = dbLogInfo.addNewRecord("jozin", "zaba", custId);
            List<MLogInfo> lis = dbLogInfo.getAllRecord();
            int last = lis.Count;
            MLogInfo li = dbLogInfo.getRecord(last, false);
            dbLogInfo.deleteRecord(li.ID);
            // testing if it has been deleted
            Assert.IsTrue(!dbLogInfo.getAllRecord().Contains(li));

            dbCustomer.deleteRecord(custId);
            dbDiscountGroup.deleteRecord(dgId);
        }
    }
}
