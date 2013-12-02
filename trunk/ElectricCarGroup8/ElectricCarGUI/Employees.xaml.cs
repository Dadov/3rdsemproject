using ElectricCarGUI.ElectricCarService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        static ElectricCarService.IElectricCar serviceObj;

        public Employees()
        {
            serviceObj = new ElectricCarService.ElectricCarClient();
            InitializeComponent();
        }

        private void fillEmpTable()
        {
            List<Employee> emps = serviceObj.getAllEmployees().ToList();
            empTable.Items.Clear();
            foreach (Employee emp in emps)
            {
                empTable.Items.Add(emp);
            }
        }

        private void refreshEmps(object sender, RoutedEventArgs e)
        {
            fillEmpTable();
        }

        private void deleteEmp(object sender, RoutedEventArgs e)
        {
            if (delEmpField.Text != "")
            {
                serviceObj.deleteEmployee(Convert.ToInt32(delEmpField.Text));
                fillEmpTable();
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
        }

        private void delEmpField_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            delEmpField.Text = "";
        }

        private void searchEmp(object sender, RoutedEventArgs e)
        {
            if (searchEmpField.Text != "")
            {
                Employee emp = serviceObj.getEmployee(Convert.ToInt32(searchEmpField.Text));
                empFName.Text = emp.FName;
                empLName.Text = emp.LName;
                empAddress.Text = emp.Address;
                empCountry.Text = emp.Country;
                empPhone.Text = emp.Phone;
                empEmail.Text = emp.Email;
                // don't want to show password anyway, otherwise fetch it from LogInfo
                empPass.Text = "";
                empStationID.Text = Convert.ToString(emp.StationID);
                // TODO: employee position
                empPosition.DataContext = emp.Position;
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
        }

        private void searchEmpField_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            searchEmpField.Text = "";
        }

        private void addEmp(object sender, RoutedEventArgs e)
        {
            /*
            serviceObj.addEmployee(
                empFName.Text,
                empLName.Text,
                empAddress.Text,
                empCountry.Text,
                empPhone.Text,
                empEmail.Text,
                empPass.Text,
                empStationID.Text,
                empPosition
                );
             */
        }

        private void updateEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee()
            {
            };
            serviceObj.updateEmployee(emp);
        }


    }
}
