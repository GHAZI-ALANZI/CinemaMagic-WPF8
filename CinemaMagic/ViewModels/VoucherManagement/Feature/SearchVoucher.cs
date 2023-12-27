using CinemaMagic.Models.DTOs;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        private ObservableCollection<VoucherDTO> filterDSVC;
        public ObservableCollection<VoucherDTO> FilterDSVC
        {
            get => filterDSVC;
            set
            {
                if (filterDSVC != value)
                {
                    filterDSVC = value;
                    OnPropertyChanged(nameof(FilterDSVC));
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
                    FilterVoucherList();
                }
            }
        }

        void searchVoucher()
        {
            FilterDSVC = new ObservableCollection<VoucherDTO>(DSVC);
        }

        private void FilterVoucherList()
        {

            // Update FilterVoucherList based on SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSVC = new ObservableCollection<VoucherDTO>(DSVC);
            }
            else
            {
                FilterDSVC = new ObservableCollection<VoucherDTO>(
                    DSVC.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
            }
        }
    }
}
