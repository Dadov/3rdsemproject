using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBCustomerTest
    /// </summary>
    [TestClass]
    public class DBCustomerTest
    {
        IDCustomer dbCustomer;
        IDDiscountGroup dbDiscountGroup;

        public DBCustomerTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void customerCRUD()
        {
            // prerequisites
            int dgId = dbDiscountGroup.addNewRecord("zidaci", -100);
            MDiscountGroup dg = dbDiscountGroup.getRecord(dgId, false);

            // create
            int custID = dbCustomer.addNewRecord("jozko", "mkrvicka", "nema", "vsade", "luxus", "bez", dg, "nikdaj");
            // get all
            List<MCustomer> custs = dbCustomer.getAllRecord();
            // int last = custs.FindLast();
            // get
            MCustomer cust = dbCustomer.getRecord(custID, false);
            Assert.IsNotNull(cust);
            // delete
            dbCustomer.deleteRecord(cust.ID);
            // testing if it has been deleted
            Assert.IsTrue(!dbCustomer.getAllRecord().Contains(cust));

            dbDiscountGroup.deleteRecord(dgId);
        }
    }
}
