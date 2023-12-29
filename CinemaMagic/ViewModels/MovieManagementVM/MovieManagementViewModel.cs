using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.MovieManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<MovieDTO> DSPhim_All;
        private ObservableCollection<MovieDTO> DSPhim_DPH;
        private ObservableCollection<MovieDTO> DSPhim_NPH;

        private MovieManagementView movieManagementView;

        public MovieManagementViewModel()
        {
            AddMovie();
            // SearchMovie();
            Delete();
            MovieDetail();
            Filter();
            edit();
        }



    }
}
