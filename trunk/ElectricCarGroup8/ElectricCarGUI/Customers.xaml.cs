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
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        static ElectricCarService.IElectricCar serviceObj;
        private List<DiscountGroup> discoGroups { get; set; }
        private RegexChecker regCheck;

        public Customers()
        {
            InitializeComponent();
            regCheck = new RegexChecker();
            serviceObj = new ElectricCarService.ElectricCarClient();
            // have to fetch it in advance for adding
            discoGroups = serviceObj.getAllDiscountGroups().ToList();
            custDiscoGroup.ItemsSource = discoGroups;
            custDiscoGroup.SelectedIndex = 0;
        }

        private void fillCustTable()
        {
            List<Customer> custs = serviceObj.getAllCustomers().ToList();
            custTable.Items.Clear();
            foreach (Customer cust in custs)
            {
                custTable.Items.Add(cust);
            }
        }

        private void refreshCusts(object sender, RoutedEventArgs e)
        {
            fillCustTable();
        }

        private void rowSelected(object sender, RoutedEventArgs e)
        {
            if (custTable.SelectedItem != null)
            {
                Customer cust = (Customer) custTable.SelectedItem;
                custFName.Text = cust.FName;
                custLName.Text = cust.LName;
                custAddress.Text = cust.Address;
                custCountry.Text = cust.Country;
                custPhone.Text = cust.Phone;
                custEmail.Text = cust.Email;
                custPass.Text = cust.LogInfos
                    .Where(li => li.LoginName == cust.Email)
                    .FirstOrDefault().Password;
                custPayStat.Text = cust.PaymentStatus;
                custDiscoGroup.SelectedIndex = discoGroups
                    .IndexOf((DiscountGroup)discoGroups
                    .Where(dg => dg.ID == cust.DiscountGroup.ID)
                    .FirstOrDefault());
            }
        }

        private void clearFields(object sender, RoutedEventArgs e)
        {
            custFName.Text = "";
            custLName.Text = "";
            custAddress.Text = "";
            custCountry.Text = "";
            custPhone.Text = "";
            custEmail.Text = "";
            custPass.Text = "";
            custPayStat.Text = "";
            custDiscoGroup.SelectedIndex = 0;
        }

        private void addCust(object sender, RoutedEventArgs e)
        {
            if (regCheck.checkEmail(custEmail.Text) && regCheck.checkPassword(custPass.Text))
            {
                serviceObj.addCustomer(
                    custFName.Text,
                    custLName.Text,
                    custAddress.Text,
                    custCountry.Text,
                    custPhone.Text,
                    custEmail.Text,
                    custPass.Text,
                    custPayStat.Text,
                    (DiscountGroup)custDiscoGroup.Items.CurrentItem
                    );
            }
            else
            {
                MessageBox.Show("Please enter valid Email and Password.");
            }
        }

        private void updateCust(object sender, RoutedEventArgs e)
        {
            if (regCheck.checkEmail(custEmail.Text) && regCheck.checkPassword(custPass.Text))
            {
                Customer cust = (Customer)custTable.SelectedItem;
                // Customer cust = serviceObj.getCustomer(Convert.ToInt32(searchCustField.Text));
                LogInfo logInfo = cust.LogInfos
                    .Where(li => li.LoginName == cust.Email)
                    .FirstOrDefault();
                cust.FName = custFName.Text;
                cust.LName = custLName.Text;
                cust.Address = custAddress.Text;
                cust.Country = custCountry.Text;
                cust.Phone = custPhone.Text;
                cust.Email = custEmail.Text;
                // not adding login info on update, just updating current
                if (logInfo.LoginName == cust.Email)
                {
                    cust.LogInfos.Where(li => li.LoginName == cust.Email)
                        .FirstOrDefault().Password = custPass.Text;
                }
                custDiscoGroup.SelectedIndex = discoGroups
                        .IndexOf((DiscountGroup)discoGroups
                        .Where(dg => dg.ID == cust.DiscountGroup.ID)
                        .FirstOrDefault());
                cust.PaymentStatus = custPayStat.Text;
                serviceObj.updateCustomer(cust);
            } 
            else 
            {
                MessageBox.Show("Please enter valid Email and Password.");
            }
        }

        private void deleteCust(object sender, RoutedEventArgs e)
        {
            Customer cust = (Customer)custTable.SelectedItem;
            serviceObj.deleteCustomer(cust.ID);
            fillCustTable();
        }
    }
}
