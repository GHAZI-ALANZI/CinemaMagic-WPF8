using CinemaMagic.ViewModels.MovieManagementVM;
using System.Windows;

namespace CinemaMagic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class AddMovieView : Window
    {
        public AddMovieView()
        {
            InitializeComponent();
            AddMovieViewModel addMovieViewModel = new AddMovieViewModel(this);
            this.DataContext = addMovieViewModel;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
