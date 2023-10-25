using System;
using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public bool ChangeValidated { get; set; }
        public UserDetailsWindow()
        {
            InitializeComponent();
            PopulateCombobox();
        }

        private void PopulateCombobox()
        {
            cbCountry.ItemsSource = Enum.GetValues(typeof(Country));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = "";

            Country country = Repos.UserManager.SignedInUIser.Location;
            if (cbCountry.SelectedIndex != -1)
            {
                country = (Country)cbCountry.SelectedValue;
            }

            if (txtPassword.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim().Length <= 5)
                {
                    MessageBox.Show("Password needs to be longer than 5 characters!", "Warning");
                    return;
                }
                if (txtPassword.Text == txtPasswordConfirmation.Text)
                {
                    password = txtPassword.Text;
                }
                else
                {
                    MessageBox.Show("Passwords did not match", "warning");
                    return;
                }
            }
            if (password == "")
            {
                password = Repos.UserManager.SignedInUIser.Password;
            }
            if (username == "")
            {
                username = Repos.UserManager.SignedInUIser.Username;
            }
            ChangeValidated = false;

            PasswordConfirmationWindow passwordConfirmationWindow = new(this);
            passwordConfirmationWindow.ShowDialog();

            if (ChangeValidated)
            {
                Repos.UserManager.SignedInUIser.Username = username;
                Repos.UserManager.SignedInUIser.Password = password;
                Repos.UserManager.SignedInUIser.Location = country;

                TravelsWindow travelsWindow = new();
                travelsWindow.Show();
                Close();


            }
            else
            {
                return;
            }







        }
    }
}
