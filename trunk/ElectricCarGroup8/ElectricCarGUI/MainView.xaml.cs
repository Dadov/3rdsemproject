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
                if (txtName.Text != "" && txtAddress.Text != "" && txtCountry.Text != "")
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
        public List<int> typeIDs { get; set; }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Station station = serviceObj.getStation(Convert.ToInt32(txtStationId.Text)) ;
            if (station != null)
            {
                StationId = Convert.ToInt32(txtStationId.Text);
                List<int> typeIDs = fillStorageTable(StationId);
                fillTypeTable(typeIDs);
                sInfoCtr.station = station;
                sInfoCtr.sId = StationId;
                sInfoCtr.mv = this;
                sInfoCtr.fillInfo(StationId);

                sbCtr.sId = StationId;
                sbCtr.showAllBookingLinesForStation(StationId);

                fillStorageTable(StationId);
                fillPeriodTable();
                bsStation.Text = StationId.ToString();
            }
            else
            {
                MessageBox.Show("Station does not exist, please input another one.");
            }
        }

        #region Batteries

        private void tbBtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try { 
            BatteryType bt = (BatteryType)dgType.SelectedItem;
            serviceObj.updateBatteryType(bt.ID, bt.name, bt.producer, bt.capacity, bt.exchangeCost, bt.storageNumber + int.Parse(btAmount.Text));
            fillTypeTable(typeIDs);
            }
            catch (Exception)
            {
                MessageBox.Show("Can not insert batteries.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = serviceObj.addBatteryType(btName.Text, btProd.Text, Decimal.Parse(btCap.Text), Decimal.Parse(btExc.Text), Int32.Parse(btStor.Text));
                typeIDs.Add(id);
                fillTypeTable(typeIDs);
            }
            catch (Exception)
            {
                MessageBox.Show("Fill the text boxes correctly. Battery type was not added","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void tbtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try { 
            serviceObj.updateBatteryType(Int32.Parse(btID.Text), btName.Text, btProd.Text, Decimal.Parse(btCap.Text), Decimal.Parse(btExc.Text), Int32.Parse(btStor.Text));
            fillTypeTable(typeIDs);
            }
            catch (Exception)
            {
                MessageBox.Show("Fill the text boxes correctly. Battery type was not updated", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try {
            serviceObj.deleteStorageByType(Int32.Parse(btID.Text));
            typeIDs.Remove(int.Parse(btID.Text));
            fillTypeTable(typeIDs);
            fillStorageTable(StationId);
            fillPeriodTable();
            clearBT();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Delete dependant Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Delete dependant Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Battery type was not deleted.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbtnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearBT();
        }

        private void clearBT()
        {
            btID.Text = btName.Text = btExc.Text = btCap.Text = btStor.Text = btProd.Text = "";

        }

        private List<int> fillStorageTable(int StationID)
        {
            string searchTerm = bsSearch.Text;
            dgStorage.Items.Clear();
            List<BatteryStorage> storages = serviceObj.getStationStorages(StationID).ToList();
            typeIDs = new List<int>();
            foreach (BatteryStorage storage in storages)
            {
                dgStorage.Items.Add(storage);
                typeIDs.Add(storage.typeID);
            }
            if (searchTerm != null)
            {
                var filter = new Predicate<object>(
                bs => ((BatteryStorage)bs).ID.ToString().ToLower().Contains(searchTerm)
                || ((BatteryStorage)bs).typeID.ToString().ToLower().Contains(searchTerm));
                dgStorage.Items.Filter = filter;
            }
            return typeIDs;
            
        }

        private void fillTypeTable(List<int> typeIDs)
        {
            string searchTerm = btSearch.Text; 
            dgType.Items.Clear();
            List<BatteryType> types = new List<BatteryType>();
            foreach(int typeID in typeIDs)
            {
                types.Add(serviceObj.getBatteryType(typeID));
            }

            foreach (BatteryType type in types)
            {
                dgType.Items.Add(type);
            }
            if (searchTerm != null)
            {
                var filter = new Predicate<object>(
                bt => ((BatteryType)bt).ID.ToString().ToLower().Contains(searchTerm)
                || ((BatteryType)bt).name.ToLower().Contains(searchTerm)
                || ((BatteryType)bt).producer.ToLower().Contains(searchTerm)
                || ((BatteryType)bt).capacity.ToString().ToLower().Contains(searchTerm)
                || ((BatteryType)bt).exchangeCost.ToString().ToLower().Contains(searchTerm)
                || ((BatteryType)bt).storageNumber.ToString().ToLower().Contains(searchTerm));
                dgType.Items.Filter = filter;
            }
            
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            fillPeriodTable();            
        }

        public void fillPeriodTable()
        {
            try
            {
                dgPeriods.Items.Clear();
                BatteryStorage storage = (BatteryStorage)dgStorage.SelectedItem;
                List<Period> periods = new List<Period>();
                if (storage != null)
                {
                    periods = serviceObj.getStoragePeriods(storage.ID).ToList();
                }
                else
                {
                    List<BatteryStorage> storages = serviceObj.getStationStorages(StationId).ToList();
                    foreach (BatteryStorage bs in storages)
                    {
                        foreach (Period p in bs.periods)
                        {
                            p.bsID = bs.ID;
                            periods.Add(p);
                        }
                    }
                }
                DateTime time = new DateTime();
                if (calendar.SelectedDate != null)
                {
                    time = (DateTime)calendar.SelectedDate;
                    foreach (Period p in periods)
                    {
                        if(storage!=null) p.bsID = storage.ID;
                        if (time.Date == p.time.Date) dgPeriods.Items.Add(p);
                    }
                }
                else
                {
                    foreach (Period p in periods)
                    {
                       if(storage!=null) p.bsID = storage.ID;
                        dgPeriods.Items.Add(p);
                    }
                }
                
            }
            catch (Exception) { }
        }

        private void sbtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serviceObj.addNewStorage(Int32.Parse(bsType.Text), Int32.Parse(bsStation.Text));
                fillStorageTable(StationId);
                fillPeriodTable();
                clearBS();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not add Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void sbtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serviceObj.updateStorage(Int32.Parse(bsID.Text), Int32.Parse(bsType.Text), Int32.Parse(bsStation.Text));
                fillStorageTable(StationId);
                clearBS();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not update Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void sbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try { 
            serviceObj.deleteStorage(Int32.Parse(bsID.Text));
            fillStorageTable(StationId);
            clearBS();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not delete Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void sbtnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearBS();
        }

        private void clearBS()
        {
            bsID.Text = bsType.Text = "";
        }


        private void dgStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BatteryStorage storage = (BatteryStorage)dgStorage.SelectedItem;
            try
            {
                bsID.Text = storage.ID.ToString();
                fillPeriodTable();
            }
            catch (Exception)
            {

            }
        }

        private void dgType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BatteryType bt = (BatteryType)dgType.SelectedItem;
                btID.Text = bt.ID.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void btSearch_KeyUp(object sender, KeyEventArgs e)
        {
            fillTypeTable(typeIDs);
        }

        private void bsSearch_KeyUp(object sender, KeyEventArgs e)
        {
            fillStorageTable(StationId);
        }
#endregion

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

        

        



    }
}
