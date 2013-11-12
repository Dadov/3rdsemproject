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

            dgStations.ItemsSource = serviceObj.getAllStations().ToList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }

        private void clearTextBoxes()
        {
            txtName.Text = txtId.Text = txtAddress.Text = txtCountry.Text = "";
            dgStations.SelectedItem = null;
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
                    MessageBox.Show("Please fill all the information in the text box.");
                }
            }
            else
            {
                MessageBox.Show("Please select a station");
            }
        }

        private void smartSearch(object sender, KeyEventArgs e)
        {

        }


    }
}
