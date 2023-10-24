using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TravelPal.Models;
using TravelPal.Repos;

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
        private List<PackingListItem> Packinglist = new List<PackingListItem>();
        private User UserToEdit { get; set; }
        public TravelDetailsWindow(Travel travel, User user)
        {
            UserToEdit = user;
            InitializeComponent();
            PopulateTypeCombobox();
            Travel = travel;
            InitializeFields();

        }



        private void InitializeFields()
        {
            Packinglist = Travel.PackingList;

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

            startDate = Travel.StartDate;
            endDate = Travel.EndDate;
            btnStartDate.Content = DateOnly.FromDateTime(Travel.StartDate);
            btnEndDate.Content = DateOnly.FromDateTime(Travel.EndDate);

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
                    EditTravel();
                    TravelsWindow travelsWindow = new();
                    travelsWindow.Show();
                    Close();
                }
            }
        }

        private void EditTravel()
        {
            if (NewTravel() != null)
            {
                SetTravelsList();
                SetUserList();

            }
        }

        //Finds the logged in user
        //Finds the trip you want to edit in the list of trips 
        //Sets the signed in user to the updated version of the user
        private void SetUserList()
        {
            for (int i = 0; i < UserManager.Users.Count; i++)
            {
                if (UserManager.Users[i].Username == UserManager.SignedInUIser.Username)
                {
                    User currentUser = (User)UserManager.Users[i];
                    for (int j = 0; j < currentUser.Travels.Count; j++)
                    {
                        if (currentUser.Travels[j] == Travel)
                        {
                            currentUser.Travels[j] = NewTravel()!;
                            UserManager.SignedInUIser = currentUser;
                            break;
                        }
                    }



                }

            }
        }
        //Finds the trip you are editing and sets it to the edited version of the trip
        private void SetTravelsList()
        {
            for (int i = 0; i < TravelManager.Travels.Count; i++)
            {
                Travel currentTravel = TravelManager.Travels[i];
                if (currentTravel.ToString() == Travel.ToString())
                {
                    TravelManager.Travels[i] = NewTravel()!;
                    break;
                }
            }
        }

        private Travel? NewTravel()
        {
            Travel newTravel;
            Country country = (Country)cbCountry.SelectedItem;
            string city = txtCity.Text;
            int travellers;
            try
            {
                travellers = int.Parse(txtTravelers.Text);
            }
            catch (InvalidCastException)
            {

                MessageBox.Show("Number of travellers field can only contain numbers!", "Warning!");
                return null;
            }

            if (cbType.SelectedIndex == 0)
            {

                newTravel = new WorkTrip(city, country, travellers, Packinglist, startDate, endDate, txtDetails.Text);
                return newTravel;
            }
            else if (cbType.SelectedIndex == 1)
            {
                bool allInclusive = (bool)cbAllInclusive.IsChecked!;
                newTravel = new Vacation(city, country, travellers, Packinglist, startDate, endDate, allInclusive);
                return newTravel;
            }

            return null;
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
            cbAllInclusive.IsEnabled = true;


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
