using System.Windows;
using System.Windows.Controls;

namespace CinemaMagic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorEditView.xaml
    /// </summary>
    public partial class ErrorEditView : Window
    {
        public ErrorEditView()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string stt = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
            if (stt != "Processed")
            {
                dtpEnd.IsEnabled = false;
                txtCost.IsEnabled = false;
            }
            else
            {
                dtpEnd.IsEnabled = true;
                txtCost.IsEnabled = true;
            }
        }
    }
}
