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
using System.ServiceModel;
using System.ServiceModel.Channels;

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
            string searchTerm = txtSearch.Text;
            dgBookings.ItemsSource = bookings;
            if (searchTerm != null && bookings.Count != 0)
            {
                var filter = new Predicate<object>(
                s => ((Booking)s).Id.ToString().ToLower().Contains(searchTerm)
                || ((Booking)s).createDate.ToLower().Contains(searchTerm)
                || ((Booking)s).customer.Id.ToString().ToLower().Contains(searchTerm)
                || ((Booking)s).tripStart.ToLower().Contains(searchTerm));
                dgBookings.Items.Filter = filter;
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
                    b.bookinglines = b.bookinglines.OrderBy(x => x.time).ToArray();
                    txtEId.Text = Convert.ToString(b.bookinglines.Last().station.Id);
                    txtSId.Text = Convert.ToString(b.bookinglines.First().station.Id);
                    BookingLine bl = new BookingLine() { price = b.bookinglines[0].price, quantity = b.bookinglines[0].quantity, BatteryType = b.bookinglines[0].BatteryType };
                    List<BookingLine> list = new List<BookingLine>();
                    list.Add(bl);
                    dgBookingLine.ItemsSource = list;
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
                dgBookings.SelectedItem = null;
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
            dgBookings.SelectedItem = null;
        }

        private void setTimeToTxtBox()
        {
            txtTripStart.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            txtCreateDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
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
                dgRoute.ItemsSource = null;
                txtTotalPrice.Text = "";
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
                Station sStation = serviceObj.getStation(Convert.ToInt32(txtSId.Text));
                Station eStation = serviceObj.getStation(Convert.ToInt32(txtEId.Text));
                if (sStation.Id != 0)
                {
                    if (eStation.Id != 0)
                    {
                        if (dgBookingLine.Items.Count != 0 && txtTripStart.Text != "")
                        {
                            AddRouoteWindow routeWin = new AddRouoteWindow(this);
                            routeWin.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show("Please add valid trip start time and battery type");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please input a valid end station Id.");
                    }
                }
                else
                {
                    MessageBox.Show("Please input a valid start station Id.");
                }
                
                
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
                txtTotalPrice.Text = "";
            }
            else
            {
                MessageBox.Show("Route can not be edited after placing the booking.");
            }
            
        }

        private void btnBAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer Customer = serviceObj.getCustomer(Convert.ToInt32(txtCId.Text));

            if (Customer.ID != 0)
            {
                Booking bk = new Booking();
                bk.cId = Convert.ToInt32(txtCId.Text);
                bk.createDate = txtCreateDate.Text;
                bk.tripStart = txtTripStart.Text;
                bk.payStatus = (string)cbbPayStatus.SelectedValue;
                bk.totalPrice = Convert.ToDecimal(txtTotalPrice.Text);
                bk.startStationId = Convert.ToInt32(txtSId.Text);

                List<RouteStop> rs = dgRoute.Items.OfType<RouteStop>().ToList<RouteStop>();
                List<BookingLine> bls = new List<BookingLine>();
                BookingLine blForBatteryType = (BookingLine)dgBookingLine.Items.GetItemAt(0);
                decimal price = blForBatteryType.price;
                int quantity = blForBatteryType.quantity;
                int btId = blForBatteryType.BatteryType.Id;
                for (int i = 0; i < rs.Count; i++)
                {
                    BookingLine bl = new BookingLine();
                    Station s = new Station();
                    BatteryTypeTest bt = new BatteryTypeTest();
                    bl.station = s;
                    bl.BatteryType = bt;
                    bl.station.Id = rs[i].station.Id;
                    bl.time = rs[i].time;
                    bl.quantity = quantity;
                    bl.price = price;
                    bl.BatteryType.Id = btId;
                    bls.Add(bl);
                }
                bk.bookinglines = bls.ToArray<BookingLine>();
                try
                {
                    serviceObj.addBooking(bk);
                    showBookings();
                    clearTextBox();
                }
                catch (FaultException a)
                {
                    MessageBox.Show(a.Message);
                }
                
                
            }
            else
            {
                MessageBox.Show("Can not find customer. Please add a valid customer Id.");
            }
            
        }

        private void btnBUpdate_Click(object sender, RoutedEventArgs e)
        {
            Booking bk = new Booking();
            bk.Id = Convert.ToInt32(txtBId.Text);
            bk.cId = Convert.ToInt32(txtCId.Text);
            bk.createDate = txtCreateDate.Text;
            bk.tripStart = txtTripStart.Text;
            bk.payStatus = (string)cbbPayStatus.SelectedValue;
            bk.totalPrice = Convert.ToDecimal(txtTotalPrice.Text);
            serviceObj.updateBooking(bk);
            showBookings();
            clearTextBox();
        }

        private void btnBDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtBId.Text!="")
            {
                serviceObj.deleteBooking(Convert.ToInt32(txtBId.Text));
                showBookings();
                clearTextBox();
            }
            else
            {
                MessageBox.Show("Please select a booking.");
            }
            
        }

        private void search_handle(object sender, KeyEventArgs e)
        {
            showBookings();
        }


    }
}
