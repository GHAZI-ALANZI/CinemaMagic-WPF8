using CinemaMagic.ViewModels.MovieManagementVM;
using System.Windows.Controls;

namespace CinemaMagic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for MovieManagementView.xaml
    /// </summary>
    public partial class MovieManagementView : UserControl
    {
        public MovieManagementView()
        {
            MovieManagementViewModel viewModel = new();
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
