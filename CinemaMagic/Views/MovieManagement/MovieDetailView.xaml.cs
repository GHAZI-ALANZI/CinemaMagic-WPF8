using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.MovieManagementVM;
using System.Windows;
using System.Windows.Input;

namespace CinemaMagic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for MovieDetailView.xaml
    /// </summary>
    public partial class MovieDetailView : Window
    {
        public MovieDetailView(MovieDTO movieDTO)
        {
            InitializeComponent();
            MovieDetailViewModel movieDetailViewModel = new MovieDetailViewModel(movieDTO);
            this.DataContext = movieDetailViewModel;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void wdDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
