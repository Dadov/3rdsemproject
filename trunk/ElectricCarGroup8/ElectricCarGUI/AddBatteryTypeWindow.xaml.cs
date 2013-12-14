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
    /// Interaction logic for AddBatteryTypeWindow.xaml
    /// </summary>
    public partial class AddBatteryTypeWindow : Window
    {
        static ElectricCarService.IElectricCar serviceObj = new ElectricCarService.ElectricCarClient();
        private BatteryType bt = new BatteryType();
        private string[] quantity = { "1", "2", "3", "4", "5" };
        private List<string> btNames = new List<string>();
        private Dictionary<string, int> name_Id = new Dictionary<string, int>();
        private BookingCtr bCtr;
        private List<BatteryType> bts = new List<BatteryType>();
        public AddBatteryTypeWindow(BookingCtr bookingCtr)
        {
            InitializeComponent();
            addNumberToCbbQuantity();
            addBTToCbbBT();
            bCtr = bookingCtr;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addNumberToCbbQuantity()
        {
            cbbQuantity.ItemsSource = quantity;
            cbbQuantity.SelectedIndex = 0;
        }

        private void addBTToCbbBT()
        {
            bts = serviceObj.getAllBatteryType().ToList();
            if (bts != null)
            {
                foreach (BatteryType b in bts)
                {
                    btNames.Add(b.name);
                    name_Id.Add(b.name, b.ID);
                }
                cbbBT.ItemsSource = btNames;
                cbbBT.SelectedIndex = 0;
            }
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            BookingLine bl = new BookingLine();
            bt.ID = name_Id[(string)cbbBT.SelectedValue];
            bt.name = (string)cbbBT.SelectedValue;
            bl.quantity = Convert.ToInt32(cbbQuantity.SelectedValue);
            
            double cost = 0;
            for (int i = 0; i < bts.Count; i++)
			{
			    if (bts[i].name == (string)cbbBT.SelectedValue)
	            {
		            cost = bts[i].price;
                    bt.price = cost;
                    break;
	            }
			}
            bl.price =Convert.ToDecimal(Convert.ToInt32(cbbQuantity.SelectedValue) * cost);
            
            bl.BatteryType = bt;
            List<BookingLine> source = new List<BookingLine>();
            source.Add(bl);
            bCtr.dgBookingLine.ItemsSource = source;

            bts = new List<BatteryType>();
            bt = new BatteryType();
            Close();
        }
    }
}
