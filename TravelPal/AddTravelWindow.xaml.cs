using System;
using System.Collections.Generic;
using System.Windows;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        private List<PackingListItem> packinglist = new List<PackingListItem>();
        private DateTime startDate = DateTime.MinValue;
        private DateTime endDate = DateTime.MinValue;
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
            }
            else if (cbType.SelectedIndex == 1)
            {
                txtDetails.Visibility = Visibility.Hidden;
                cbAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (EmptyFieldChecker())
            {
                if (NewTravel != null)
                {
                    Repos.TravelManager.travels.Add(NewTravel()!);
                    User currentUser = (User)Repos.UserManager.SignedInUIser;
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



                newTravel = new WorkTrip(city, country, travellers, packinglist, startDate, endDate, txtDetails.Text);
                return newTravel;
            }
            else if (cbType.SelectedIndex == 1)
            {
                bool allInclusive = (bool)cbAllInclusive.IsChecked!;
                newTravel = new Vacation(city, country, travellers, packinglist, startDate, endDate, allInclusive);
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
            PackingListItem newPackingListItem = new(txtLuggage.Text);
            packinglist.Add(newPackingListItem);
            txtLuggage.Text = "";
        }
    }
}
