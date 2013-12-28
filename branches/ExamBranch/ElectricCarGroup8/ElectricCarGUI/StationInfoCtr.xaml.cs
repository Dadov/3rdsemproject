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
using ElectricCarGUI.ElectricCarService;

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for StationInfoControl.xaml
    /// </summary>
    public partial class StationInfoControl : UserControl
    {

        public MainView mv { get; set; }
        public int sId { get; set; }
        public Station station { get; set; }
        private List<string> states;
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        public StationInfoControl()
        {
            InitializeComponent();
            states = serviceObj.getStates().ToList();
        }

        public void fillInfo(int id)
        {
            txtSId.Text = Convert.ToString(sId);
            txtAddress.Text = station.Address;
            txtCountry.Text = station.Country;
            txtName.Text = station.Name;
            cbbInfoState.ItemsSource = states;
            cbbInfoState.SelectedIndex = states.IndexOf(station.State);
            //show nabor stations
            showNbStations(sId);

        }

        private void btnInfoUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtAddress.Text != "" && txtCountry.Text != "")
            {
                serviceObj.updateStation(Convert.ToInt32(txtSId.Text), txtName.Text, txtAddress.Text, txtCountry.Text, (string)cbbInfoState.SelectedValue);

            }
            else
            {
                MessageBox.Show("Please fill all the station information in the text box.");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtId.Text = txtDriveHour.Text = txtDistance.Text = "";
            dbNbStations.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
                {
                    if (txtDistance.Text != "" && txtDriveHour.Text != "")
                    {
                        if (!serviceObj.isConnectionExist(Convert.ToInt32(sId), Convert.ToInt32(txtId.Text)))
                        {
                            serviceObj.addNaborStation(sId, Convert.ToInt32(txtId.Text), Convert.ToDecimal(txtDistance.Text), Convert.ToDecimal(txtDriveHour.Text));
                            clear();
                            showNbStations(sId);
                        }
                        else
                        {
                            MessageBox.Show("The connection already exists.");
                            clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please fill all nabor station information in the text box.");
                    }
                }
                else
                {
                    clear();
                    MessageBox.Show("Please fill nabor station information after clearing textboxes.");
                }
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
                {
                    if (txtDistance.Text != "" && txtDriveHour.Text != "")
                    {
                        serviceObj.updateNaborStation(sId, Convert.ToInt32(txtId.Text), Convert.ToDecimal(txtDistance.Text), Convert.ToDecimal(txtDriveHour.Text));
                        clear();
                        showNbStations(sId);
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

        private void showNbStations(int id)
        {
            
            string searchTerm = txtSearch.Text;
            List<NaborStation> stations = serviceObj.getNaborStations(sId).ToList();
            dbNbStations.ItemsSource = stations;
            if (searchTerm != null && stations.Count != 0)
            {
                var filter = new Predicate<object>(
                s => ((NaborStation)s).Id.ToString().ToLower().Contains(searchTerm)
                || ((NaborStation)s).Name.ToLower().Contains(searchTerm)
                || ((NaborStation)s).Address.ToLower().Contains(searchTerm));
                dbNbStations.Items.Filter = filter;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                serviceObj.deleteNaborStation(sId, Convert.ToInt32(txtId.Text));
                dbNbStations.SelectedItem = null;
                clear();
                showNbStations(sId);
            }
            else
            {
                MessageBox.Show("Please select a nabor station.");
            }

        }

        private void nbRowSelected(object sender, SelectionChangedEventArgs e)
        {
            if (dbNbStations.SelectedItem != null)
            {
                NaborStation ns = (NaborStation)dbNbStations.SelectedItem;
                txtId.Text = Convert.ToString(ns.Id);
                txtDistance.Text = Convert.ToString(ns.Distance);
                txtDriveHour.Text = Convert.ToString(ns.DriveHour);

            }
        }

        private void search_handle(object sender, KeyEventArgs e)
        {
            showNbStations(sId);
        }
    }
}
