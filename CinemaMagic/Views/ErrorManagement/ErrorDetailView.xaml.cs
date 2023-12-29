using System.Windows;

namespace CinemaMagic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorDetailView.xaml
    /// </summary>
    public partial class ErrorDetailView : Window
    {
        private bool loadedStatus;
        public ErrorDetailView()
        {
            InitializeComponent();
            loadedStatus = false;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void infoCost_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loadedStatus)
            {
                return;
            }
            if (txtStatus.Text == "Processed")
            {
                infoCost.Visibility = Visibility.Visible;
                infoEnd.Visibility = Visibility.Visible;
            }
            else
            {
                infoCost.Visibility = Visibility.Hidden;
                infoEnd.Visibility = Visibility.Hidden;
            }
        }

        private void txtStatus_Loaded(object sender, RoutedEventArgs e)
        {
            loadedStatus = true;
        }
    }
}
