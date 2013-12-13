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
using System.Data;
using System.ServiceModel;

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BatteryStorageCtr : UserControl
    {
        public RegexChecker regCheck = new RegexChecker();
        public MainView mv { get; set; }
        public int StationId { get; set; }
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        public BatteryTypeCtr bctr { get; set; }
        private List<string> btNames = new List<string>();
        private Dictionary<string, int> name_Id = new Dictionary<string, int>();
        public BatteryStorageCtr()
        {
           
            InitializeComponent();
            
        }

        public void fillData()
        {
            cbPeriod.SelectedIndex = 0;
            fillPeriodTable();
            bsStation.Text = StationId.ToString();
            addBTToCbbBT();
        }

        private void bsSearch_KeyUp(object sender, KeyEventArgs e)
        {
            fillStorageTable(StationId);
        }

        private BatteryStorage toStorage(tableType tt)
        {
            BatteryStorage bs = serviceObj.getStorage(tt.ID);
            return bs;
        }

        private void dgStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tableType tt = (tableType)dgStorage.SelectedItem;
            BatteryStorage storage = new BatteryStorage();
           if(tt!=null) storage = toStorage((tableType)dgStorage.SelectedItem);
            try
            {
                bsID.Text = storage.ID.ToString();
                if (tt != null) cbType.SelectedItem = tt.type;
                else cbType.SelectedIndex = 0;
                btStor.Text = storage.storageNumber.ToString();
                if (tt != null)
                {
                    if (calendar.SelectedDate == null)
                    {
                        cbPeriod.SelectedItem = cbi2;
                    }
                    else
                    {
                        cbPeriod.SelectedItem = cbi4;
                    }
                }
                changePeriodsView();
            }
            catch (Exception)
            {

            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                if (dgStorage.SelectedItem == null)
                {
                    cbPeriod.SelectedItem = cbi3;
                }
                else
                {
                    cbPeriod.SelectedItem = cbi4;
                }
                changePeriodsView();
            }
        }

        public void fillPeriodTable()
        {
            try
            {
                dgPeriods.Items.Clear();
                BatteryStorage storage = null;
                if((tableType)dgStorage.SelectedItem!=null) storage = toStorage((tableType)dgStorage.SelectedItem);
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
                        if (storage != null) p.bsID = storage.ID;
                        if (time.Date == p.time.Date) dgPeriods.Items.Add(p);
                    }
                }
                else
                {
                    foreach (Period p in periods)
                    {
                        if (storage != null) p.bsID = storage.ID;
                        dgPeriods.Items.Add(p);
                    }
                }

            }
            catch (Exception) { }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            changePeriodsView();
        }

        private void changePeriodsView()
        {
            if (cbPeriod.SelectedItem == cbi1)
            {
                dgStorage.SelectedItem = null;
                calendar.SelectedDate = null;
                fillPeriodTable();
            }
            if (cbPeriod.SelectedItem == cbi3)
            {
                dgStorage.SelectedItem = null;
                if (calendar.SelectedDate == null) calendar.SelectedDate = DateTime.Today;
                fillPeriodTable();
            }
            if (cbPeriod.SelectedItem == cbi2)
            {
                calendar.SelectedDate = null;
                if (dgStorage.SelectedItem == null) dgStorage.SelectedIndex = 0;
                fillPeriodTable();
            }
            if (cbPeriod.SelectedItem == cbi4)
            {
                if (calendar.SelectedDate == null) calendar.SelectedDate = DateTime.Today;
                if (dgStorage.SelectedItem == null) dgStorage.SelectedIndex = 0;
                fillPeriodTable();
            }
        }

        private bool checkBS()
        {
            if (regCheck.checkNumber(btStor.Text))
            {
                return true;
            }
            else return false;
        }

        private void addBTToCbbBT()
        {
            List<BatteryType> bts = new List<BatteryType>();
            bts = serviceObj.getAllBatteryTypes().ToList();
            if (bts != null)
            {
                foreach (BatteryType b in bts)
                {
                    btNames.Add(b.name);
                    name_Id.Add(b.name, b.ID);
                }
                cbType.ItemsSource = btNames;
                cbType.SelectedIndex = 0;
            }

        }

        private void sbtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = name_Id[(string)cbType.SelectedValue];
                if (checkBS())
                {
                    serviceObj.addNewStorage(Id, Int32.Parse(bsStation.Text), Int32.Parse(btStor.Text));
                    fillStorageTable(StationId);
                    fillPeriodTable();
                    clearBS();
                }
                else MessageBox.Show("Enter valid numbers!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error. Can not add Battery storage.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void sbtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = name_Id[(string)cbType.SelectedValue];
                if (checkBS())
                {
                    serviceObj.updateStorage(Int32.Parse(bsID.Text), Id, Int32.Parse(bsStation.Text), Int32.Parse(btStor.Text));
                    fillStorageTable(StationId);
                    fillPeriodTable();
                    clearBS();
                }
                else MessageBox.Show("Enter valid numbers!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void sbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BatteryStorage bs = serviceObj.getStorage(int.Parse(bsID.Text));
                serviceObj.deleteStorage(Int32.Parse(bsID.Text));
                fillStorageTable(StationId);
                fillPeriodTable();
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
            bsID.Text = btStor.Text = "";
        }

        public class tableType
        {
            public int ID {get; set;}
            public string type { get; set; }
            public int storageNumber { get; set; }
        }

        public void fillStorageTable(int StationID)
        {
            string searchTerm = bsSearch.Text;
            dgStorage.Items.Clear();
            List<BatteryStorage> storages = serviceObj.getStationStorages(StationID).ToList();
            foreach (BatteryStorage storage in storages)
            {
                dgStorage.Items.Add(new tableType() { ID = storage.ID, type = serviceObj.getBatteryType(storage.typeID).name, storageNumber = storage.storageNumber });
            }
            if (searchTerm != null)
            {
                var filter = new Predicate<object>(
                bs => ((tableType)bs).ID.ToString().ToLower().Contains(searchTerm)
                || ((tableType)bs).type.ToLower().Contains(searchTerm)
                || ((tableType)bs).storageNumber.ToString().ToLower().Contains(searchTerm));
                dgStorage.Items.Filter = filter;
            }

        }
        private void tbBtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (regCheck.checkNumber(btAmount.Text))
                {
                    BatteryStorage bt = (BatteryStorage)dgStorage.SelectedItem;
                    serviceObj.updateStorage(bt.ID, bt.typeID, StationId, bt.storageNumber + int.Parse(btAmount.Text));
                    fillStorageTable(StationId);
                    fillPeriodTable();
                }
                else
                {
                    MessageBox.Show("Enter valid number!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
