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
        private string[] sort = { "Distance", "Price" };
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        private Dictionary<int, List<RouteStop>> rWithId = new Dictionary<int, List<RouteStop>>();
        List<RouteInfoHolder> rInfos = new List<RouteInfoHolder>();
        public AddRouoteWindow(BookingCtr bookingCtr)
        {
            InitializeComponent();
            bCtr = bookingCtr;
            addCbbSort();
            showRoute();
                
        }

        private void addCbbSort()
        {
            cbbSort.ItemsSource = sort;
            cbbSort.SelectedIndex = 0;
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
            
            if (routes.Count() >= 1) //TODO need to change
            {
                //show routes
                List<List<RouteStop>> rs = new List<List<RouteStop>>();
                for (int i = 0; i < routes.Length; i++)
                {
                    List<RouteStop> n = new List<RouteStop>();
                    RouteInfoHolder rInfo = new RouteInfoHolder();
                    string info = " ";
                    decimal totalDistance = 0;
                    for (int j = 0; j < routes[i].Length; j++)
                    {
                        n.Add(routes[i][j]);
                        info += routes[i][j].station.Name + " " + routes[i][j].time.ToString("MM/dd/yyyy HH:mm") + " | ";
                        if (j == routes[i].Length-1)
                        {
                            totalDistance = routes[i][j].distance;
                        }
                        
                    }
                    rs.Add(n);
                    rWithId.Add(i, n);
                    rInfo.Info = info;
                    rInfo.Id = i;
                    rInfo.TotalDistance = totalDistance;
                    rInfo.TotalPrice = bl.BatteryType.price * bl.quantity * routes[i].Length;
                    rInfos.Add(rInfo);
                }
                dgRoutes.ItemsSource = rInfos;
                
            }
            else
            {
                MessageBox.Show("No paths is found.");
                Close();
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RouteInfoHolder selectedRoute = (RouteInfoHolder)dgRoutes.SelectedItem;
            int id = selectedRoute.Id;
            List<RouteStop> r = rWithId[id];
            bCtr.dgRoute.ItemsSource = r;
            bCtr.txtTotalPrice.Text = Convert.ToString(selectedRoute.TotalPrice);
            Close();
        }

        private void sortHandle(object sender, SelectionChangedEventArgs e)
        {
            if (cbbSort.SelectedIndex == 0)
            {
                rInfos.OrderBy(x => x.TotalDistance);
            }
            else
            {
                rInfos.OrderBy(x => x.TotalPrice);
            }
            dgRoutes.ItemsSource = rInfos;
        }

        
    }
}
