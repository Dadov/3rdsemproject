using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBEmployeeTest
    /// </summary>
    [TestClass]
    public class DBEmployeeTest
    {
        IDEmployee dbEmployee;
        IDStation dbStation;

        public DBEmployeeTest()
        {
            //
            // TODO: Add constructor logic here
            //
            dbEmployee = new DEmployee();
            dbStation = new DStation();
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
        public void employeeCRUD()
        {
            int stationId = dbStation.addNewRecord("AarhusStation", "Aarhus", "Denmark", "Open"); 

            // create
            int empID = dbEmployee.addNewRecord("pistek", "baci", "tryskacova", "kokotlina", "chujovina",
                "ale picu", stationId, EmployeePosition.LiftBoy);
            // get all
            List<MEmployee> emps = dbEmployee.getAllRecord();
            // int last = emps.Count;
            // get
            MEmployee emp = dbEmployee.getRecord(empID, false);
            Assert.IsNotNull(emp);
            // delete
            dbEmployee.deleteRecord(emp.ID);
            // testing if it has been deleted
            Assert.IsTrue(!dbEmployee.getAllRecord().Contains(emp));

            dbStation.deleteRecord(stationId);
        }
    }
}
