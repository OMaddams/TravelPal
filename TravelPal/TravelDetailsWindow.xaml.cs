using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            lblTravelDays.Content = "Total travel days ca: " + Travel.TravelDays.ToString();


            lblUntilTravel.Content = Travel.UntilTravel <= 0 ? lblUntilTravel.Visibility = Visibility.Hidden : "Days until travel : " + Travel.UntilTravel.ToString();

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
            if (UserManager.SignedInUIser is User)
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
            else if (UserManager.SignedInUIser is Admin)
            {
                for (int i = 0; i < UserManager.Users.Count; i++)
                {
                    if (UserManager.Users[i].Username == Travel.OwnedUser.Username)
                    {
                        User currentUser = (User)UserManager.Users[i];
                        for (int j = 0; j < currentUser.Travels.Count; j++)
                        {
                            if (currentUser.Travels[j] == Travel)
                            {
                                currentUser.Travels[j] = NewTravel()!;
                                break;
                            }
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
            txtLuggage.IsEnabled = true;
            txtLuggageAmount.IsEnabled = true;
            cbTravelDocument.IsEnabled = true;
            cbRequired.IsEnabled = true;
            btnLuggageAdd.IsEnabled = true;



            btnEndDate.Background = Brushes.MediumSlateBlue;
            btnEndDate.Foreground = Brushes.MintCream;
            btnStartDate.Background = Brushes.MediumSlateBlue;
            btnStartDate.Foreground = Brushes.MintCream;
            btnLuggageAdd.Background = Brushes.MediumSlateBlue;
            btnLuggageAdd.Foreground = Brushes.MintCream;

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

        private void UpdateLuggageList()
        {
            lstLuggage.Items.Clear();
            foreach (PackingListItem item in Packinglist)
            {
                ListViewItem lstItem = new();
                lstItem.Tag = item;
                lstItem.Content = item.ToString();

                lstLuggage.Items.Add(lstItem);
            }
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

        private void lstLuggage_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem listItem = (ListViewItem)lstLuggage.SelectedItem;
            PackingListItem packingItem = (PackingListItem)listItem.Tag;

            if (packingItem != null)
            {
                Packinglist.Remove(packingItem);
                UpdateLuggageList();
            }
        }

        private void cbTravelDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLuggageAmount.Visibility = Visibility.Visible;
            cbRequired.Visibility = Visibility.Hidden;
        }

        private void cbTravelDocument_Checked(object sender, RoutedEventArgs e)
        {
            txtLuggageAmount.Visibility = Visibility.Hidden;
            cbRequired.Visibility = Visibility.Visible;
        }

        private void btnLuggageAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtLuggage.Text == "")
            {
                MessageBox.Show("Please fill out the luggage field before trying to add luggage");
                return;
            }
            if (txtLuggageAmount.Text == "" && cbTravelDocument.IsChecked == false)
            {
                AddLuggage();
                return;
            }
            if (cbTravelDocument.IsChecked == false)
            {
                int amount;
                try
                {
                    amount = int.Parse(txtLuggageAmount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter a number in the luggage amount box", "Warning");
                    return;
                }
                AddLuggage(amount);
            }
            if (cbTravelDocument.IsChecked == true)
            {
                TravelDocument newTravelDocument = new(txtLuggage.Text, (bool)cbRequired.IsChecked!);
                Packinglist.Add(newTravelDocument);
                txtLuggage.Text = "";
                cbTravelDocument.IsChecked = false;
                cbRequired.IsChecked = false;
                UpdateLuggageList();
            }
        }
        private void AddLuggage()
        {
            PackingListItem newPackingListItem = new(txtLuggage.Text);
            Packinglist.Add(newPackingListItem);
            txtLuggage.Text = "";
            UpdateLuggageList();
        }
        private void AddLuggage(int amount)
        {
            PackingListItem newPackingListItem = new(txtLuggage.Text, amount);
            Packinglist.Add(newPackingListItem);
            txtLuggage.Text = "";
            txtLuggageAmount.Text = "";
            UpdateLuggageList();
        }

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Enum.IsDefined(typeof(EuropeanCountry), UserManager.SignedInUIser.Location.ToString()))
            {
                if (!Enum.IsDefined(typeof(EuropeanCountry), cbCountry.SelectedValue.ToString()!))
                {
                    TravelDocument travelDocument = new("Passport", true);
                    for (int i = 0; i < Packinglist.Count; i++)
                    {
                        if (Packinglist[i].Name == "Passport")
                        {
                            Packinglist.RemoveAt(i);
                            break;
                        }
                    }
                    Packinglist.Add(travelDocument);
                    UpdateLuggageList();

                }
                if (Enum.IsDefined(typeof(EuropeanCountry), cbCountry.SelectedValue.ToString()!))
                {
                    TravelDocument travelDocument = new("Passport", false);
                    for (int i = 0; i < Packinglist.Count; i++)
                    {
                        if (Packinglist[i].Name == "Passport")
                        {
                            Packinglist.RemoveAt(i);
                            break;
                        }
                    }
                    Packinglist.Add(travelDocument);
                    UpdateLuggageList();
                }
            }
            else if (!Enum.IsDefined(typeof(EuropeanCountry), UserManager.SignedInUIser.Location.ToString()))
            {
                TravelDocument travelDocument = new("Passport", true);
                for (int i = 0; i < Packinglist.Count; i++)
                {
                    if (Packinglist[i].Name == "Passport")
                    {
                        Packinglist.RemoveAt(i);
                        break;
                    }
                }
                Packinglist.Add(travelDocument);
                UpdateLuggageList();
            }
        }
    }
}
