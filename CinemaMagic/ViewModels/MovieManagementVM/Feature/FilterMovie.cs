using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        private void Filter()
        {
            loadData();
            cboSelectionChangedCommand = new ViewModelCommand(CboSelectionChanged);
        }

        public ICommand cboSelectionChangedCommand { get; set; }


        private int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set
            {
                if (_selectedItemIndex != value)
                {
                    _selectedItemIndex = value;
                    OnPropertyChanged(nameof(SelectedItemIndex));
                }
            }
        }

        private void CboSelectionChanged(object obj)

        // Perform processing when the selected value changes here
        {
            HandleSelectionChanged();
        }


        private void HandleSelectionChanged()
        {
            if (SelectedItemIndex == 1)
            {
                FilterDSPhim = DSPhim_DPH;
            }
            else if (SelectedItemIndex == 2)
            {
                FilterDSPhim = DSPhim_NPH;
            }
            else
            {
                FilterDSPhim = DSPhim_All;
            }
        }


        private void loadData()
        {
            DSPhim_All = new ObservableCollection<MovieDTO>();
            DSPhim_DPH = new ObservableCollection<MovieDTO>();
            DSPhim_NPH = new ObservableCollection<MovieDTO>();

            MovieDA data = new MovieDA();
            ObservableCollection<MovieDTO> getDSSP = data.getAllMovie();
            foreach (var item in getDSSP)
            {
                if (item.Status == "Currently released")
                {
                    DSPhim_DPH.Add(item);
                }
                else if (item.Status == "Ceased released")
                {
                    DSPhim_NPH.Add(item);
                }
                DSPhim_All.Add(item);
            }

            SearchMovie();
        }
    }
}
