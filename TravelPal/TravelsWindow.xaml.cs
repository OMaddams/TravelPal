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
            TravelDetailsWindow travelDetailsWindow = new TravelDetailsWindow();
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
            if (MessageBox.Show("This will remove your selected trip, are you sure?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListViewItem selectedItem = (ListViewItem)lstAddedTrips.SelectedItem;
                Travel travelToRemove = (Travel)selectedItem.Tag;
                User currentUser = (User)UserManager.SignedInUIser;
                currentUser.Travels.Remove(travelToRemove);
                TravelManager.RemoveTravel(travelToRemove);
                PopulateList(currentUser);
            }

        }
    }
}
