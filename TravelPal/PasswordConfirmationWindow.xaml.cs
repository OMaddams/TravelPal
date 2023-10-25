using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for PasswordConfirmationWindow.xaml
    /// </summary>
    public partial class PasswordConfirmationWindow : Window
    {
        UserDetailsWindow UserDetailsWindow { get; set; }
        public PasswordConfirmationWindow(UserDetailsWindow userDetailsWindow)
        {
            InitializeComponent();
            UserDetailsWindow = userDetailsWindow;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Repos.UserManager.SignedInUIser.Password == txtPassword.Password)
            {
                UserDetailsWindow.ChangeValidated = true;
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect password", "Warning");
            }
        }
    }
}
