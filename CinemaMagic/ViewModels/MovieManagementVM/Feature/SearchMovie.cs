using CinemaMagic.Models.DTOs;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        private ObservableCollection<MovieDTO> filterDSPhim;
        public ObservableCollection<MovieDTO> FilterDSPhim
        {
            get => filterDSPhim;
            set
            {
                if (filterDSPhim != value)
                {
                    filterDSPhim = value;
                    OnPropertyChanged(nameof(FilterDSPhim));
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterMovieList();
                }
            }
        }


        void SearchMovie()
        {
            if (SelectedItemIndex == 1)
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_DPH);
            }
            else if (SelectedItemIndex == 2)
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_NPH);
            }
            else
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_All);
            }
        }


        private void FilterMovieList()
        {
            // Update FilteredStaffList based on SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_DPH);
                }
                else if (SelectedItemIndex == 2)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_NPH);
                }
                else
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_All);
                }
            }
            else
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_DPH.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
                }
                else if (SelectedItemIndex == 2)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_NPH.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
                }
                else
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_All.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
            }
        }

    }
}
