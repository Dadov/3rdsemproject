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

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for AddRouoteWindow.xaml
    /// </summary>
    public partial class AddRouoteWindow : Window
    {
        private BookingCtr bCtr;
        private string[] sort = { "Distance", "Time", "Price" };
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        public AddRouoteWindow(BookingCtr bookingCtr)
        {
            InitializeComponent();
            bCtr = bookingCtr;
            cbbSort.ItemsSource = sort;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showRoute()
        {
            //get batteryLimit
            BookingLine bl = (BookingLine)bCtr.dgBookingLine.Items.GetItemAt(0);
            decimal batteryLimit = serviceObj.convertCapacityToDistance(bl.BatteryType.capacity);
            RouteStop[][] routes = serviceObj.getRoutes(Convert.ToInt32(bCtr.txtSId.Text), Convert.ToInt32(bCtr.txtEId.Text), Convert.ToDateTime(bCtr.txtTripStart.Text), batteryLimit);
            if (routes.Count() != 0)
            {
                //TODO show routes
            }
            else
            {
                MessageBox.Show("No paths is found.");
                Close();
            }
            
        }
    }
}
