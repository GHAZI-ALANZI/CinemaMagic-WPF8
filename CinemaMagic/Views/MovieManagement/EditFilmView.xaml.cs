using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.MovieManagementVM;
using System.Windows;

namespace CinemaMagic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for EditFilmView.xaml
    /// </summary>
    public partial class EditFilmView : Window
    {
        public EditFilmView(MovieDTO movieDTO)
        {
            InitializeComponent();
            EditMovieViewModel editMovieViewModel = new EditMovieViewModel(movieDTO, this);
            this.DataContext = editMovieViewModel;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
