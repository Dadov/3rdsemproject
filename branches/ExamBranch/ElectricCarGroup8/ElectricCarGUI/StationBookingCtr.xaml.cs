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
    /// Interaction logic for StationBookingCtr.xaml
    /// </summary>
    public partial class StationBookingCtr : UserControl
    {
        public int sId { get; set; }
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();

        public StationBookingCtr()
        {
            InitializeComponent();
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            showAllBookingLinesForStation(sId);
            
        }

        private void showBookingForDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dpDate.SelectedDate.Value;
            dgBooking.ItemsSource = serviceObj.getBookingLinesForDateInStation(sId, date);
        }

        public void showAllBookingLinesForStation(int sId)
        {
            List<BookingLine> bls = serviceObj.getBookingLinesForStation(sId).ToList<BookingLine>();
            if (bls != null)
	        {
                dgBooking.ItemsSource = bls;
            }
        }
    }
}
