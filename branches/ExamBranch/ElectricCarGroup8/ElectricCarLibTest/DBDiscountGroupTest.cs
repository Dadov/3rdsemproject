using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBDiscountGroup
    /// </summary>
    [TestClass]
    public class DBDiscountGroupTest
    {
        IDDiscountGroup dbDiscountGroup;

        public DBDiscountGroupTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void discountGroupCRUD()
        {
            // create
            int dgID = dbDiscountGroup.addNewRecord("zidaci", -100);
            // get all
            List<MDiscountGroup> dgs = dbDiscountGroup.getAllRecord();
            // int last = dgs.Count;
            // get
            MDiscountGroup dg = dbDiscountGroup.getRecord(dgID, false);
            Assert.IsNotNull(dg);
            // delete
            dbDiscountGroup.deleteRecord(dg.ID);
            // testing if it has been deleted
            Assert.IsTrue(!dbDiscountGroup.getAllRecord().Contains(dg));
        }
    }
}
