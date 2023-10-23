using System;
using System.Windows;
using TravelPal.Models;
using TravelPal.Repos;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            cbCountry.ItemsSource = Enum.GetValues(typeof(Country));
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "" || cbCountry.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill out all the fields", "Warning");
                return;
            }

            if (UserManager.AddUser(new User(txtUsername.Text, txtPassword.Text, (Country)cbCountry.SelectedValue)))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }
}
