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
        private Array discoGroups;
        

        public Customers()
        {
            serviceObj = new ElectricCarService.ElectricCarClient();
            // have to fetch it in advance for adding
            discoGroups = serviceObj.getAllDiscountGroups();
            InitializeComponent();
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

        private void deleteCust(object sender, RoutedEventArgs e)
        {
            if (delCustField.Text != "")
            {
                serviceObj.deleteCustomer(Convert.ToInt32(delCustField.Text));
                fillCustTable();
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
        }

        private void delCustField_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            delCustField.Text = "";
        }

        private void searchCust(object sender, RoutedEventArgs e)
        {
            if (searchCustField.Text != "")
            {
                Customer cust = serviceObj.getCustomer(Convert.ToInt32(searchCustField.Text));
                custFName.Text = cust.FName;
                custLName.Text = cust.LName;
                custAddress.Text = cust.Address;
                custCountry.Text = cust.Country;
                custPhone.Text = cust.Phone;
                custEmail.Text = cust.Email;
                // don't want to show password anyway, otherwise fetch it from LogInfo
                custPass.Text = "";
                custPayStat.Text = cust.PaymentStatus;
                // TODO: discount group fill and selection for add and update
                custDiscoGroup.Items.Clear();
                discoGroups = serviceObj.getAllDiscountGroups();
                /*
                foreach (DiscountGroup discoGroup in discoGroups) 
                {
                    custDiscoGroup.Items.Add(discoGroup);
                }
                custDiscoGroup.SelectedItem = cust.DiscountGroup;
                custDiscoGroup.SelectedValue = cust.DiscountGroup.Name;
                */
                custDiscoGroup.DataContext = discoGroups;
                custDiscoGroup.DisplayMemberPath = cust.DiscountGroup.Name;
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
        }

        private void searchCustField_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            searchCustField.Text = "";
        }

        private void addCust(object sender, RoutedEventArgs e)
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
                (DiscountGroup) custDiscoGroup.Items.CurrentItem
                );
            Console.WriteLine("kokot jedna");
        }

        private void updateCust(object sender, RoutedEventArgs e)
        {
            Customer cust = new Customer()
            {
            };
            serviceObj.updateCustomer(cust);
        }


    }
}
