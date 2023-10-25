using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Models;
using TravelPal.Repos;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        public TravelsWindow()
        {
            InitializeComponent();
            if (UserManager.SignedInUIser is User)
            {
                PopulateList((User)UserManager.SignedInUIser);
            }
            else if (UserManager.SignedInUIser is Admin)
            {
                PopulateList(TravelManager.Travels);
            }
            btnUser.Content = UserManager.SignedInUIser.Username;

        }

        private void PopulateList(User user)
        {
            lstAddedTrips.Items.Clear();
            foreach (Travel travel in user.Travels)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = travel;
                item.Content = travel.Country.ToString();
                lstAddedTrips.Items.Add(item);
            }

        }
        private void PopulateList(List<Travel> list)
        {
            lstAddedTrips.Items.Clear();
            foreach (Travel travel in list)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = travel;
                item.Content = travel.Country.ToString();
                if (UserManager.SignedInUIser is Admin)
                {
                    if (travel.OwnedUser != null)
                    {
                        item.Content += " " + travel.OwnedUser.Username;
                    }
                    else
                    {
                        item.Content += " user";
                    }

                }
                lstAddedTrips.Items.Add(item);
            }
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new AddTravelWindow();
            addTravelWindow.Show();
            Close();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lstAddedTrips.SelectedIndex != -1 && UserManager.SignedInUIser is User)
            {
                CreateTravelDetailsWindow((User)UserManager.SignedInUIser);

            }
            else if (lstAddedTrips.SelectedIndex != -1 && UserManager.SignedInUIser is Admin)
            {

                CreateTravelDetailsWindow();

            }
            else
            {
                MessageBox.Show("Select item to view first");
            }

        }

        private void CreateTravelDetailsWindow(User owner)
        {
            ListViewItem selectedItem = (ListViewItem)lstAddedTrips.SelectedItem;
            Travel travelToView = (Travel)selectedItem.Tag;
            TravelDetailsWindow travelDetailsWindow = new TravelDetailsWindow(travelToView, owner);
            travelDetailsWindow.Show();
            Close();
        }
        private void CreateTravelDetailsWindow()
        {
            ListViewItem selectedItem = (ListViewItem)lstAddedTrips.SelectedItem;
            Travel travelToView = (Travel)selectedItem.Tag;
            TravelDetailsWindow travelDetailsWindow = new TravelDetailsWindow(travelToView, (User)travelToView.OwnedUser);
            travelDetailsWindow.Show();
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstAddedTrips.SelectedIndex == -1)
            {
                MessageBox.Show("Select item to remove first");
                return;
            }
            if (MessageBox.Show("This will remove the selected trip, are you sure?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (UserManager.SignedInUIser is User)
                {
                    UserRemoveItem();
                }
                else if (UserManager.SignedInUIser is Admin)
                {
                    ListViewItem selectedItem = (ListViewItem)lstAddedTrips.SelectedItem;
                    Travel travelToRemove = (Travel)selectedItem.Tag;
                    User currentUser = (User)travelToRemove.OwnedUser;
                    currentUser.Travels.RemoveAt(FindTravel(travelToRemove, FindUser(travelToRemove.OwnedUser.Username)));
                    UserManager.Users[FindUser(travelToRemove.OwnedUser.Username)] = currentUser;
                    TravelManager.RemoveTravel(travelToRemove);
                    PopulateList(TravelManager.Travels);
                }
            }

        }

        private void UserRemoveItem()
        {
            ListViewItem selectedItem = (ListViewItem)lstAddedTrips.SelectedItem;
            Travel travelToRemove = (Travel)selectedItem.Tag;
            User currentUser = (User)UserManager.SignedInUIser;
            currentUser.Travels.Remove(travelToRemove);
            TravelManager.RemoveTravel(travelToRemove);
            PopulateList(currentUser);
        }
        private int FindUser(string username)
        {
            for (int i = 0; i < UserManager.Users.Count; i++)
            {
                if (UserManager.Users[i].Username == username)
                {
                    return i;
                }
            }
            return 0;
        }
        private int FindTravel(Travel travel, int userindex)
        {
            User user = (User)UserManager.Users[userindex];
            for (int i = 0; i < user.Travels.Count; i++)
            {
                if (user.Travels[i].ToString() == travel.ToString())
                {
                    return i;
                }
            }
            return 999;
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog();
        }
    }
}
