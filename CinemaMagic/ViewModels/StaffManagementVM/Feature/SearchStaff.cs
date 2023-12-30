using CinemaMagic.Models.DTOs.StaffManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        private ObservableCollection<StaffDTO> filterDSNV;
        public ObservableCollection<StaffDTO> FilterDSNV
        {
            get => filterDSNV;
            set
            {
                if (filterDSNV != value)
                {
                    filterDSNV = value;
                    OnPropertyChanged(nameof(FilterDSNV));
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
                    FilterStaffList();
                }
            }
        }

        void SearchStaff()
        {
            FilterDSNV = new ObservableCollection<StaffDTO>(DSNV);
        }
        private void FilterStaffList()
        {

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSNV = new ObservableCollection<StaffDTO>(DSNV);
            }
            else
            {
                FilterDSNV = new ObservableCollection<StaffDTO>(
                    DSNV.Where(s => s.FullName.ToLower().Contains(SearchText.ToLower())));
            }
        }
    }
}
