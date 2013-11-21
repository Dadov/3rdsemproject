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
    /// Interaction logic for BookingCtr.xaml
    /// </summary>
    
    public partial class BookingCtr : UserControl
    {
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        string[] payStates = { "UnPayed", "Payed" };
        public BookingCtr()
        {
            InitializeComponent();
            showBookings();
            addPayStates();
        }

        private void addPayStates()
        {
            cbbPayStatus.ItemsSource = payStates;
            cbbPayStatus.SelectedIndex = 0;
        }

        private void showBookings()
        {
            List<Booking> bookings = serviceObj.getAllBookings().ToList();
            if (bookings.Count!=0)
            {
                dgBookings.ItemsSource = bookings;
            }
            setTimeToTxtBox();

        }

        private void rowSelected(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookings.SelectedItem != null)
            {
                Booking b = (Booking)dgBookings.SelectedItem;
                //show info
                txtBId.Text = Convert.ToString(b.Id);
                txtCId.Text = Convert.ToString(b.cId);
                txtCreateDate.Text = b.createDate;
                if (b.bookinglines != null)
                {
                    txtEId.Text = Convert.ToString(b.bookinglines.Last().station.Id);
                    txtSId.Text = Convert.ToString(b.bookinglines.First().station.Id);
                    dgBookingLine.ItemsSource = b.bookinglines;
                    dgRoute.ItemsSource = b.bookinglines;
                }
                txtTotalPrice.Text = Convert.ToString(b.totalPrice);
                txtTripStart.Text = b.tripStart;
                if (b.payStatus == "Payed")
                {
                    cbbPayStatus.SelectedIndex = 1;
                }
                else
                {
                    cbbPayStatus.SelectedIndex = 0;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearTextBox();
        }
        private void clearTextBox()
        {
            txtBId.Text = txtCId.Text = txtCreateDate.Text = txtEId.Text = txtSId.Text = txtTotalPrice.Text = txtTripStart.Text = "";
            cbbPayStatus.SelectedIndex = 0;
            dgRoute.ItemsSource = null;
            dgBookingLine.ItemsSource = null;
            setTimeToTxtBox();
        }

        private void setTimeToTxtBox()
        {
            txtTripStart.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            txtCreateDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        }
        

        private BatteryTypeTest bt = new BatteryTypeTest();
        private void btnAddBT_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text == "")
            {
                if (dgBookingLine.ItemsSource == null)
                {
                    AddBatteryTypeWindow btWin = new AddBatteryTypeWindow(this);
                    btWin.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Only one battery type is allowed to added. Please delete the old one before adding.");
                }
            }
            else
            {
                MessageBox.Show("Battery type can not be edited after placing the booking.");
            }
            
        }

        private void btnDeleteBT_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text=="")
            {
                bt = new BatteryTypeTest();
                dgBookingLine.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("Battery type can not be edited after placing the booking.");
            }
            
        }

        private void btnSelectRoute_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text=="")
            {
                AddRouoteWindow routeWin = new AddRouoteWindow(this);
                routeWin.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Route can not be edited after placing the booking.");
            }
            

        }

        private void btnDeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text == "")
            {
                dgRoute.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("Route can not be edited after placing the booking.");
            }
            
        }

        private void btnBAdd_Click(object sender, RoutedEventArgs e)
        {
            Booking bk = new Booking();
            bk.cId = Convert.ToInt32(txtCId.Text);
            bk.createDate = txtCreateDate.Text;
            bk.tripStart = txtTripStart.Text;
            bk.payStatus = (string)cbbPayStatus.SelectedValue;
            bk.totalPrice = Convert.ToDecimal(txtTotalPrice.Text);
            bk.startStationId = Convert.ToInt32(txtSId.Text);

            List<RouteStop> rs = (List<RouteStop>)dgRoute.Items.OfType<RouteStop>();
            List<BookingLine> bls = new List<BookingLine>();
            BookingLine blForBatteryType = (BookingLine)dgBookingLine.Items.GetItemAt(0);
            decimal price = blForBatteryType.price;
            int quantity = blForBatteryType.quantity;
            int btId = blForBatteryType.BatteryType.Id;
            for (int i = 0; i < rs.Count; i++)
            {
                BookingLine bl = new BookingLine();
                bl.station.Id = rs[i].station.Id;
                bl.time = rs[i].time;
                bl.quantity = quantity;
                bl.price = price;
                bl.BatteryType.Id = btId;
                bls.Add(bl);
            }

            serviceObj.addBooking(bk);
        }

        private void btnBUpdate_Click(object sender, RoutedEventArgs e)
        {
            Booking bk = new Booking();
            bk.cId = Convert.ToInt32(txtCId.Text);
            bk.createDate = txtCreateDate.Text;
            bk.payStatus = (string)cbbPayStatus.SelectedValue;
            bk.totalPrice = Convert.ToDecimal(txtTotalPrice.Text);
            serviceObj.updateBooking(bk);
        }

        private void btnBDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text!="")
            {
                serviceObj.deleteBooking(Convert.ToInt32(txtBId.Text));
            }
            else
            {
                MessageBox.Show("Please select a booking.");
            }
            
        }


    }
}
