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
    /// Interaction logic for BatteryTypeCtr.xaml
    /// </summary>
    public partial class BatteryTypeCtr : UserControl
    {
        public MainView mv { get; set; }
        public int StationId { get; set; }
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        public BatteryStorageCtr bCtr { get; set; }
        public RegexChecker regCheck = new RegexChecker();
       
        public BatteryTypeCtr()
        {
            InitializeComponent();   
        }

        public void fillData()
        {
            fillTypeTable();
        }

        private bool checkBT()
        {
            if (regCheck.checkDecimal(btCap.Text) && (regCheck.checkDecimal(btExc.Text)))
            {
                return true;
            }
            else return false;
        }

        private void tbtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkBT())
                {
                    int id = serviceObj.addBatteryType(btName.Text, btProd.Text, Decimal.Parse(btCap.Text), Decimal.Parse(btExc.Text));
                    fillTypeTable();
                }
                else MessageBox.Show("Enter valid numbers!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error. Battery type was not added", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkBT())
                {
                    serviceObj.updateBatteryType(Int32.Parse(btID.Text), btName.Text, btProd.Text, Decimal.Parse(btCap.Text), Decimal.Parse(btExc.Text));
                    fillTypeTable();
                }
                else MessageBox.Show("Enter valid numbers!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error. Battery type was not updated", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (canDeleteType())
                {
                    serviceObj.deleteBatteryType(Int32.Parse(btID.Text));
                }
                else
                {
                    MessageBox.Show("Delete all dependant battery storages first!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                fillTypeTable();
                bCtr.fillStorageTable(StationId);
                bCtr.fillPeriodTable();
                clearBT();
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error. Battery type was not deleted.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool canDeleteType()
        {
            List<BatteryStorage> storages = serviceObj.getAllStorages().ToList();
            int i = 0;
            while (i < storages.Count)
            {
                BatteryStorage storage = storages[i];
                if (storage.typeID == Int32.Parse(btID.Text))
                {
                    return false;
                }
                else i++;
            }
            return true;
        }

        private void tbtnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearBT();
        }

        private void clearBT()
        {
            btID.Text = btName.Text = btExc.Text = btCap.Text = btProd.Text = "";

        }



        public void fillTypeTable()
        {
            string searchTerm = btSearch.Text;
            dgType.Items.Clear();
            List<BatteryType> types = serviceObj.getAllBatteryTypes().ToList();

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
                || ((BatteryType)bt).exchangeCost.ToString().ToLower().Contains(searchTerm));
                dgType.Items.Filter = filter;
            }

        }





        private void dgType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BatteryType bt = (BatteryType)dgType.SelectedItem;
                btID.Text = bt.ID.ToString();
                btCap.Text = bt.capacity.ToString();
                btName.Text = bt.name;
                btProd.Text = bt.producer;
                btExc.Text = bt.exchangeCost.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void btSearch_KeyUp(object sender, KeyEventArgs e)
        {
            fillTypeTable();
        }
    }
}
