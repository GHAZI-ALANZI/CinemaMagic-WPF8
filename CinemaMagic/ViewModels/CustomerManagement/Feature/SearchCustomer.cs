using CinemaMagic.Models.DTOs;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public string cboLuaChonTimKiem { get; set; }

        void searchCTM()
        {
            cboLuaChonTimKiem = "";
            FilterDSCTM = new ObservableCollection<CustomerDTO>(DSCTM);
        }

        private ObservableCollection<CustomerDTO> filterDSCTM;
        public ObservableCollection<CustomerDTO> FilterDSCTM
        {
            get => filterDSCTM;
            set
            {
                if (filterDSCTM != value)
                {
                    filterDSCTM = value;
                    OnPropertyChanged(nameof(FilterDSCTM));
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
                    FilterCustomerList();
                }
            }
        }

        private void FilterCustomerList()
        {
            // SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSCTM = new ObservableCollection<CustomerDTO>(DSCTM);
            }
            else
            {
                if (cboLuaChonTimKiem == "Customer name")
                {
                    FilterDSCTM = new ObservableCollection<CustomerDTO>(
                        DSCTM.Where(s => s.FullName.ToLower().Contains(SearchText.ToLower())));
                }
                else if (cboLuaChonTimKiem == "Phone number")
                {
                    FilterDSCTM = new ObservableCollection<CustomerDTO>(
                        DSCTM.Where(s => s.PhoneNumber.ToLower().Contains(SearchText.ToLower())));
                }
            }
        }

    }
}
