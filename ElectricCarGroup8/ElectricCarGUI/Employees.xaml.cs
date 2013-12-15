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
        private RegexChecker regCheck;

        public Employees()
        {
            InitializeComponent();
            regCheck = new RegexChecker();
            serviceObj = new ElectricCarService.ElectricCarClient();
            fillEmpTable();
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

        private void rowSelected(object sender, RoutedEventArgs e)
        {
            if (empTable.SelectedItem != null)
            {
                Employee emp = (Employee)empTable.SelectedItem;
                empFName.Text = emp.FName;
                empLName.Text = emp.LName;
                empAddress.Text = emp.Address;
                empCountry.Text = emp.Country;
                empPhone.Text = emp.Phone;
                empEmail.Text = emp.Email;
                empPass.Text = emp.LogInfos
                    .Where(li => li.LoginName == emp.Email)
                    .FirstOrDefault().Password;
                empStationID.Text = Convert.ToString(emp.StationID);
                empPosition.Text = emp.Position;
            }
        }

        private void clearFields(object sender, RoutedEventArgs e)
        {
            empFName.Text = "";
            empLName.Text = "";
            empAddress.Text = "";
            empCountry.Text = "";
            empPhone.Text = "";
            empEmail.Text = "";
            empPass.Text = "";
            empStationID.Text = "";
            empPosition.Text = "";
        }

        private void addEmp(object sender, RoutedEventArgs e)
        {
            bool email = regCheck.checkEmail(empEmail.Text);
            bool pass = regCheck.checkPassword(empPass.Text);
            if (regCheck.checkEmail(empEmail.Text) && regCheck.checkPassword(empPass.Text))
            {
                serviceObj.addEmployee(
                    empFName.Text,
                    empLName.Text,
                    empAddress.Text,
                    empCountry.Text,
                    empPhone.Text,
                    empEmail.Text,
                    empPass.Text,
                    Convert.ToInt32(empStationID.Text),
                    empPosition.Text
                    );
            }
            else
            {
                MessageBox.Show("Please enter valid Email address and Password have to cointain letters and numbers.");
            };
        }

        private void updateEmp(object sender, RoutedEventArgs e)
        {
            if (regCheck.checkEmail(empEmail.Text) && regCheck.checkPassword(empPass.Text))
            {
                Employee emp = (Employee)empTable.SelectedItem;
                LogInfo logInfo = emp.LogInfos
                   .Where(li => li.LoginName == emp.Email)
                   .FirstOrDefault();
                emp.FName = empFName.Text;
                emp.LName = empLName.Text;
                emp.Address = empAddress.Text;
                emp.Country = empCountry.Text;
                emp.Phone = empPhone.Text;
                emp.Email = empEmail.Text;
                // not adding login info on update, just updating current
                if (logInfo.LoginName == emp.Email)
                {
                    emp.LogInfos.Where(li => li.LoginName == emp.Email)
                        .FirstOrDefault().Password = empPass.Text;
                }
                emp.StationID = Convert.ToInt32(empStationID.Text);
                emp.Position = empPosition.Text;
                serviceObj.updateEmployee(emp);
            }
            else
            {
                MessageBox.Show("Please enter valid Email address and Password have to cointain letters and numbers.");
            }
        }

        private void deleteEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee)empTable.SelectedItem;
            serviceObj.deleteEmployee(emp.ID);
            fillEmpTable();
        }
    }
}
