using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Models;
using TravelPal.Repos;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        private List<PackingListItem> Packinglist = new List<PackingListItem>();
        public DateTime startDate = DateTime.MinValue;
        public DateTime endDate = DateTime.MinValue;
        public AddTravelWindow()
        {
            InitializeComponent();
            PopulateTypeCombobox();
        }


        private void PopulateTypeCombobox()
        {
            cbType.ItemsSource = new object[] { "Work trip", "Holiday" };
            cbType.SelectedIndex = 0;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Country));
        }

        private void cbType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbType.SelectedIndex == 0)
            {
                txtDetails.Visibility = Visibility.Visible;
                cbAllInclusive.Visibility = Visibility.Hidden;
                lblAllInclusive.Visibility = Visibility.Hidden;
                lblDetails.Visibility = Visibility.Visible;
            }
            else if (cbType.SelectedIndex == 1)
            {
                txtDetails.Visibility = Visibility.Hidden;
                cbAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
                lblDetails.Visibility = Visibility.Hidden;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (EmptyFieldChecker())
            {
                if (NewTravel() != null)
                {
                    TravelManager.AddTravel(NewTravel()!);
                    User currentUser = (User)UserManager.SignedInUIser;
                    currentUser.Travels.Add(NewTravel()!);
                    TravelsWindow travelsWindow = new TravelsWindow();
                    travelsWindow.Show();
                    Close();
                }
            }

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

        //TODO: Check if start and enddate is valid 
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Anything not saved will be lost, are you sure?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TravelsWindow travelswindow = new();
                travelswindow.Show();
                Close();
            }

        }

        private void cbTravelDocument_Checked(object sender, RoutedEventArgs e)
        {
            txtLuggageAmount.Visibility = Visibility.Hidden;
            cbRequired.Visibility = Visibility.Visible;
        }

        private void cbTravelDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLuggageAmount.Visibility = Visibility.Visible;
            cbRequired.Visibility = Visibility.Hidden;
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

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog();
        }
    }
}
