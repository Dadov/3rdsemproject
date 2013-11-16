﻿using System;
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
        string[] payStates = { "UnPay", "Payed" };
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
        }

        private BatteryTypeTest bt = new BatteryTypeTest();
        private void btnAddBT_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingLine.ItemsSource==null)
            {
                AddBatteryTypeWindow btWin = new AddBatteryTypeWindow(this);
                btWin.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Only one battery type is allowed to added. Please delete the old one before adding.");
            }
            
        }

        private void btnDeleteBT_Click(object sender, RoutedEventArgs e)
        {
            bt = new BatteryTypeTest();
            dgBookingLine.ItemsSource = null;
        }
    }
}