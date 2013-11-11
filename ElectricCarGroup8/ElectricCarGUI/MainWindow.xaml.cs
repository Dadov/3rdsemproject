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

namespace ElectricCarGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            bool success = false;
            string position = "";
            if (e.Key == Key.Return)
            {
                //TODO Evaluate login name and password, get employee position
                position = "Admin"; 
                success = true;

            }
            if (success)
            {
                //create main window with authorized view
                MainView mainView = new MainView(txtName.Text, position);
                mainView.Visibility = Visibility.Visible;
                txtName.Text = "";
                txtPass.Text = "";
                Close();
            }
        }
    }
}
