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
using System.Threading;

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        
        private RegexChecker regCheck = new RegexChecker();

        public MainView(string name, string position)
        {
            
            InitializeComponent();
            lblName.Content = name;
        }

        

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Visibility = Visibility.Visible;
            Close();
        }

        

        public int StationId { get; set; }
        
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (regCheck.checkNumber(txtStationId.Text))
            {
                Station station = serviceObj.getStation(Convert.ToInt32(txtStationId.Text));
                if (station != null)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
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
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                }
                else
                {
                    MessageBox.Show("Station does not exist, please input another one.");
                }
            }
            else
            {
                MessageBox.Show("Please input number in station id field.");
                txtStationId.Text = "";
            }
            
        }

        private void addStationsCtr(object sender, RoutedEventArgs e)
        {
            StationsCtr sCtr = new StationsCtr();
            tabStations.Content = sCtr;
            
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
        private bool isTabStorageLoaded = false;
        private void tabStorage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!isTabTypeLoaded)
            {
                bCtr = new BatteryStorageCtr();
                tabStorage.Content = bCtr;
                isTabStorageLoaded = true;
            }

        }

        private BatteryTypeCtr btCtr;
        private void tabType_Loaded_1(object sender, RoutedEventArgs e)
        {
            btCtr = new BatteryTypeCtr();
            tabType.Content = btCtr;
            btCtr.bCtr = bCtr; 
        }

        
        #region People

        // customer moved to separate user control
        private Customers customers;
        private void CustomersTab_Loaded(object sender, RoutedEventArgs e)
        {
            customers = new Customers();
            CustomersTab.Content = customers;
        }



        // employee moved to separate user control
        private Employees employees;
        private void EmployeesTab_Loaded(object sender, RoutedEventArgs e)
        {
            employees = new Employees();
            EmployeesTab.Content = employees;
        }

        #endregion People


        private void cursorWait(object sender, RoutedEventArgs e)
        {

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            
        }

        private void cursorBack(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }


        private bool isTabTypeLoaded = false;
        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedItem == tabType&&!isTabTypeLoaded)
            {
                btCtr.fillData();
                isTabTypeLoaded = true;
            }
        }

        


        
    }
}
