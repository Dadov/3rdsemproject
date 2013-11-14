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

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        private List<string> states;

        public MainView(string name, string position)
        {
            InitializeComponent();
            lblName.Content = name;
            addStateStringToCbb();
            showStations();
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

        private void showStations()
        {
            string searchTerm = txtSearch.Text;
            List<Station> stations = serviceObj.getAllStations().ToList();
            dgStations.ItemsSource = stations;
            if (searchTerm!=null)
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
            if (dbNbStations.SelectedItem!=null)
            {
                NaborStation ns = (NaborStation)dbNbStations.SelectedItem;
                txtNsId.Text = Convert.ToString(ns.Id);
                txtNsDistance.Text = Convert.ToString(ns.Distance);
                txtNsDriveHour.Text = Convert.ToString(ns.DriveHour);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text !="")
            {
                serviceObj.deleteStation(Convert.ToInt32(txtId.Text));
                dgStations.SelectedItem = null;
                clearTextBoxes();
                showStations();
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
                if (txtName.Text!="" &&txtAddress.Text!="" &&txtCountry.Text!="")
                {
                    serviceObj.addStation(txtName.Text, txtAddress.Text, txtCountry.Text, (string)cbbState.SelectedValue);
                    clearTextBoxes();
                    showStations();
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
                    showStations();
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
            showStations();
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
            if (txtId.Text!="")
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (serviceObj.getStation(Convert.ToInt32(txtStationId.Text)) != null)
            {
                StationId = Convert.ToInt32(txtStationId.Text);
            }
            else
            {
                MessageBox.Show("Station does not exist, please input another one.");
            }
        }
        

    }
}
