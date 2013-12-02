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
using System.Windows.Shapes;
using ElectricCarGUI.ElectricCarService;
using System.Data;
using System.ServiceModel;

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        private List<string> states;
        private RegexChecker regCheck = new RegexChecker();

        public MainView(string name, string position)
        {
            InitializeComponent();
            lblName.Content = name;
        }

        private void addStateStringToCbb()
        {
            states = serviceObj.getStates().ToList();
            cbbState.ItemsSource = states;
            cbbState.SelectedIndex = 0;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Visibility = Visibility.Visible;
            Close();
        }

        private void showStations(object sender, RoutedEventArgs e)
        {
            addStateStringToCbb();
            showStation();
        }
        private void showStation()
        {
            string searchTerm = txtSearch.Text;
            List<Station> stations = serviceObj.getAllStations().ToList();
            dgStations.ItemsSource = stations;
            if (searchTerm != null && stations.Count != 0)
            {
                var filter = new Predicate<object>(
                s => ((Station)s).Id.ToString().ToLower().Contains(searchTerm)
                || ((Station)s).Name.ToLower().Contains(searchTerm)
                || ((Station)s).Address.ToLower().Contains(searchTerm));
                dgStations.Items.Filter = filter;
            }



        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }

        private void clearTextBoxes()
        {
            txtName.Text = txtId.Text = txtAddress.Text = txtCountry.Text = "";
            dgStations.SelectedItem = null;

            clearNaborStationTextBox();
            dbNbStations.SelectedItem = null;
            dbNbStations.ItemsSource = null;
            lblNbStation.Content = "Nabor stations";
        }

        private void rowSelected(object sender, SelectionChangedEventArgs e)
        {
            if (dgStations.SelectedItem != null)
            {
                Station s = (Station)dgStations.SelectedItem;
                txtId.Text = Convert.ToString(s.Id);
                txtName.Text = s.Name;
                txtCountry.Text = s.Country;
                txtAddress.Text = s.Address;
                cbbState.SelectedIndex = states.IndexOf(s.State);

                //select nabor stations 
                showNbStations(s.Id);
            }

        }

        private void showNbStations(int sId)
        {
            lblNbStation.Content = "Nabor stations for station Id " + sId;

            dbNbStations.ItemsSource = serviceObj.getNaborStations(sId);
        }

        private void nbRowSelected(object sender, SelectionChangedEventArgs e)
        {
            if (dbNbStations.SelectedItem != null)
            {
                NaborStation ns = (NaborStation)dbNbStations.SelectedItem;
                txtNsId.Text = Convert.ToString(ns.Id);
                txtNsDistance.Text = Convert.ToString(ns.Distance);
                txtNsDriveHour.Text = Convert.ToString(ns.DriveHour);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                serviceObj.deleteStation(Convert.ToInt32(txtId.Text));
                dgStations.SelectedItem = null;
                clearTextBoxes();
                showStation();
            }
            else
            {
                MessageBox.Show("Please select a station.");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                if (txtName.Text != "" && txtAddress.Text != "" && txtCountry.Text != "")
                {
                    serviceObj.addStation(txtName.Text, txtAddress.Text, txtCountry.Text, (string)cbbState.SelectedValue);
                    clearTextBoxes();
                    showStation();
                }
                else
                {
                    MessageBox.Show("Please fill all the information in the text box.");
                }
            }
            else
            {
                clearTextBoxes();
                MessageBox.Show("Please fill information after clearing textboxes.");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                if (txtName.Text != "" && txtAddress.Text != "" && txtCountry.Text != "")
                {
                    serviceObj.updateStation(Convert.ToInt32(txtId.Text), txtName.Text, txtAddress.Text, txtCountry.Text, (string)cbbState.SelectedValue);
                    clearTextBoxes();
                    showStation();
                }
                else
                {
                    MessageBox.Show("Please fill all the station information in the text box.");
                }
            }
            else
            {
                MessageBox.Show("Please select a station");
            }
        }

        private void smartSearch(object sender, KeyEventArgs e)
        {
            showStation();
        }

        private void btnNsClear_Click(object sender, RoutedEventArgs e)
        {
            clearNaborStationTextBox();
            dbNbStations.SelectedItem = null;
        }

        private void clearNaborStationTextBox()
        {
            txtNsId.Text = txtNsDriveHour.Text = txtNsDistance.Text = "";
        }

        private void btnNsDel_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                if (txtNsId.Text != "")
                {
                    serviceObj.deleteNaborStation(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtNsId.Text));
                    dgStations.SelectedItem = null;
                    clearNaborStationTextBox();
                    showNbStations(Convert.ToInt32(txtId.Text));
                }
                else
                {
                    MessageBox.Show("Please select a nabor station.");
                }
            }
            else
            {
                MessageBox.Show("Please select a station first before update nabor station");
            }

        }

        private void btnNsAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                if (txtNsId.Text != "")
                {
                    if (txtNsDistance.Text != "" && txtNsDriveHour.Text != "")
                    {
                        serviceObj.addNaborStation(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtNsId.Text), Convert.ToDecimal(txtNsDistance.Text), Convert.ToDecimal(txtNsDriveHour.Text));
                        clearNaborStationTextBox();
                        showNbStations(Convert.ToInt32(txtId.Text));
                    }
                    else
                    {
                        MessageBox.Show("Please fill all nabor station information in the text box.");
                    }
                }
                else
                {
                    clearTextBoxes();
                    MessageBox.Show("Please fill nabor station information after clearing textboxes.");
                }
            }
            else
            {
                MessageBox.Show("Please select a station first before adding nabor station");
            }

        }

        private void btnNsUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                if (txtNsId.Text != "")
                {
                    if (txtNsDistance.Text != "" && txtNsDriveHour.Text != "")
                    {
                        serviceObj.updateNaborStation(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtNsId.Text), Convert.ToDecimal(txtNsDistance.Text), Convert.ToDecimal(txtNsDriveHour.Text));
                        clearNaborStationTextBox();
                        showNbStations(Convert.ToInt32(txtId.Text));
                    }
                    else
                    {
                        MessageBox.Show("Please fill all the nabor station information in the text box.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a nabor station");
                }
            }
            else
            {
                MessageBox.Show("Please select a station first before update nabor station");
            }

        }

        public int StationId { get; set; }       

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Station station = serviceObj.getStation(Convert.ToInt32(txtStationId.Text)) ;
            if (station != null)
            {
                StationId = Convert.ToInt32(txtStationId.Text);
                bCtr.fillStorageTable(StationId);
                bCtr.StationId = StationId;
                bCtr.mv = this;
                btCtr.fillData();
                bCtr.fillData();
                sInfoCtr.station = station;
                sInfoCtr.sId = StationId;
                sInfoCtr.mv = this;
                sInfoCtr.fillInfo(StationId);

                sbCtr.sId = StationId;
                sbCtr.showAllBookingLinesForStation(StationId);

                
            }
            else
            {
                MessageBox.Show("Station does not exist, please input another one.");
            }
        }


        private StationInfoControl sInfoCtr;
        private void addInfoControl(object sender, RoutedEventArgs e)
        {
            sInfoCtr = new StationInfoControl();
            tabStationiInfo.Content = sInfoCtr;
        }

        private void addBookingCtr(object sender, RoutedEventArgs e)
        {
            BookingCtr bCtr = new BookingCtr();
            tabBooking.Content = bCtr;
        }

        private StationBookingCtr sbCtr;
        private void addBookingControl(object sender, RoutedEventArgs e)
        {
            sbCtr = new StationBookingCtr();
            tabStationBooking.Content = sbCtr;
        }

        public BatteryStorageCtr bCtr;
        private void tabStorage_Loaded_1(object sender, RoutedEventArgs e)
        {
            bCtr = new BatteryStorageCtr();
            tabStorage.Content = bCtr;

        }

        private BatteryTypeCtr btCtr;
        private void tabType_Loaded_1(object sender, RoutedEventArgs e)
        {
            btCtr = new BatteryTypeCtr();
            tabType.Content = btCtr;
            btCtr.bCtr = bCtr; 
        }

        
        #region People

        // customer
        private void fillCustTable()
        {
            // TODO: check bindings
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
                // TODO: discout group
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
        }

        private void addCust(object sender, RoutedEventArgs e)
        {
            /*
            serviceObj.addCustomer(
                custFName.Text,
                custLName.Text,
                custAddress.Text,
                custCountry.Text,
                custPhone.Text,
                custEmail.Text,
                custPass.Text,
                custPayStat.Text,
                custDiscoGroup
                );
             */
        }

        private void updateCust(object sender, RoutedEventArgs e)
        {
            Customer cust = new Customer()
            {
            };
            serviceObj.updateCustomer(cust);
        }

        // employee
        private void fillEmpTable()
        {
            // TODO: check bindings
            List<Employee> emps = serviceObj.getAllEmployees().ToList();
            empTable.Items.Clear();
            foreach (Employee emp in emps)
            {
                custTable.Items.Add(emp);
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

        private void searchEmp(object sender, RoutedEventArgs e)
        {
            if (searchCustField.Text != "")
            {
                Employee emp = serviceObj.getEmployee(Convert.ToInt32(searchCustField.Text));
                empFName.Text = emp.FName;
                empLName.Text = emp.LName;
                empAddress.Text = emp.Address;
                empCountry.Text = emp.Country;
                empPhone.Text = emp.Phone;
                empEmail.Text = emp.Email;
                // don't want to show password anyway, otherwise fetch it from LogInfo
                custPass.Text = "";
                empStationID.Text = Convert.ToString(emp.StationID);
                // TODO: employee position
            }
            else
            {
                MessageBox.Show("Please insert customer ID");
            }
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

        #endregion People

        
    }
}
