using System.Windows;

namespace CinemaMagic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorReportView.xaml
    /// </summary>
    public partial class ErrorReportView : Window
    {
        public ErrorReportView()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
