using System;
using System.Windows;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        public Travel Travel { get; }
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
    }
}
