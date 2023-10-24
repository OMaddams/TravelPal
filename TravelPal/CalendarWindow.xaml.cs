using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        string dateToEdit;
        private AddTravelWindow addSenderWindow;
        private TravelDetailsWindow editSenderWindow;

        public CalendarWindow(string dateToEdit, AddTravelWindow senderWindow)
        {
            InitializeComponent();
            this.dateToEdit = dateToEdit;
            this.addSenderWindow = senderWindow;
        }
        public CalendarWindow(string dateToEdit, TravelDetailsWindow senderWindow)
        {
            InitializeComponent();
            this.dateToEdit = dateToEdit;
            this.editSenderWindow = senderWindow;
        }


        private void calendarControl_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (addSenderWindow != null)
            {
                SelectionChangedLogic(addSenderWindow);
            }
            else if (editSenderWindow != null)
            {
                SelectionChangedLogic(editSenderWindow);
            }
        }

        private void SelectionChangedLogic(AddTravelWindow window)
        {
            if (calendarControl.SelectedDate != null)
            {
                if (dateToEdit == "startDate")
                {
                    addSenderWindow.startDate = calendarControl.SelectedDate.Value;
                    addSenderWindow.btnStartDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }
                else if (dateToEdit == "endDate")
                {
                    addSenderWindow.endDate = calendarControl.SelectedDate.Value;
                    addSenderWindow.btnEndDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }


            }
        }
        private void SelectionChangedLogic(TravelDetailsWindow window)
        {
            if (calendarControl.SelectedDate != null)
            {
                if (dateToEdit == "startDate")
                {
                    editSenderWindow.startDate = calendarControl.SelectedDate.Value;
                    editSenderWindow.btnStartDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }
                else if (dateToEdit == "endDate")
                {
                    editSenderWindow.endDate = calendarControl.SelectedDate.Value;
                    editSenderWindow.btnEndDate.Content = calendarControl.SelectedDate.Value;

                    Close();

                }


            }
        }
    }
}
