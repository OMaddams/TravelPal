using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        string dateToEdit;
        private AddTravelWindow senderWindow;

        public CalendarWindow(string dateToEdit, AddTravelWindow senderWindow)
        {
            InitializeComponent();
            this.dateToEdit = dateToEdit;
            this.senderWindow = senderWindow;
        }



        private void calendarControl_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (calendarControl.SelectedDate != null)
            {
                if (dateToEdit == "startDate")
                {
                    senderWindow.startDate = calendarControl.SelectedDate.Value;
                    senderWindow.btnStartDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }
                else if (dateToEdit == "endDate")
                {
                    senderWindow.endDate = calendarControl.SelectedDate.Value;
                    senderWindow.btnEndDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }


            }
        }
    }
}
