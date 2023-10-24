using System;
using System.Windows;
using System.Windows.Media;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        public Travel Travel { get; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();
            PopulateTypeCombobox();
            Travel = travel;
            InitializeFields();

        }



        private void InitializeFields()
        {
            if (Travel is WorkTrip)
            {
                WorkTrip workTrip = (WorkTrip)Travel;
                cbType.SelectedIndex = 0;
                txtDetails.Text = workTrip.MeetingDetails;

            }
            else if (Travel is Vacation)
            {
                Vacation vacation = (Vacation)Travel;
                txtDetails.Visibility = Visibility.Hidden;
                lblAllInclusive.Visibility = Visibility.Visible;
                cbAllInclusive.Visibility = Visibility.Visible;
                cbAllInclusive.IsChecked = vacation.AllInclusive;
                cbType.SelectedIndex = 1;
            }

            cbCountry.SelectedValue = Travel.Country;
            txtCity.Text = Travel.Destination;
            txtTravelers.Text = Travel.Travellers.ToString();
            btnStartDate.Content = Travel.StartDate;
            btnEndDate.Content = Travel.EndDate;

        }

        private void PopulateTypeCombobox()
        {
            cbType.ItemsSource = new object[] { "Work trip", "Holiday" };
            cbType.SelectedIndex = 0;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Country));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnSaveEdit.Content == "Edit")
            {
                WindowExit();
            }
            else if ((string)btnSaveEdit.Content == "Save")
            {
                if (MessageBox.Show("Anything not saved will be lost, are you sure?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    WindowExit();
                }
            }
        }

        private void WindowExit()
        {
            TravelsWindow travelswindow = new();
            travelswindow.Show();
            Close();
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnSaveEdit.Content == "Edit")
            {
                FlipFields();
            }
            else if ((string)btnSaveEdit.Content == "Save")
            {
                if (EmptyFieldChecker())
                {

                }
            }
        }

        private void FlipFields()
        {

            cbType.IsEnabled = true;
            cbCountry.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtTravelers.IsEnabled = true;
            txtDetails.IsEnabled = true;
            txtLuggage.IsEnabled = true;
            btnStartDate.IsEnabled = true;
            btnEndDate.IsEnabled = true;

            btnEndDate.Background = Brushes.MediumSlateBlue;
            btnEndDate.Foreground = Brushes.MintCream;
            btnStartDate.Background = Brushes.MediumSlateBlue;
            btnStartDate.Foreground = Brushes.MintCream;

            btnSaveEdit.Content = "Save";

        }

        private bool EmptyFieldChecker()
        {
            if (cbType.SelectedIndex == -1 || cbCountry.SelectedIndex == -1 || txtCity.Text.Trim() == "" || txtTravelers.Text.Trim() == "" || startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                MessageBox.Show("Please fill out all the fields", "Warning");
                return false;
            }

            return true;
        }

        private void btnStartDate_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendar = new("startDate", this);
            calendar.Show();
        }

        private void btnEndDate_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendar = new("endDate", this);
            calendar.Show();
        }

        private void cbType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbType.SelectedIndex == 0)
            {
                txtDetails.Visibility = Visibility.Visible;
                cbAllInclusive.Visibility = Visibility.Hidden;
                lblAllInclusive.Visibility = Visibility.Hidden;
            }
            else if (cbType.SelectedIndex == 1)
            {
                txtDetails.Visibility = Visibility.Hidden;
                cbAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
            }
        }
    }
}
