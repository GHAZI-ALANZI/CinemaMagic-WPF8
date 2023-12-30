using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.CustomerManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<CustomerDTO> dsctm;
        public ObservableCollection<CustomerDTO> DSCTM
        {
            get => dsctm;
            set
            {
                if (dsctm != value)
                {
                    dsctm = value;
                    OnPropertyChanged(nameof(DSCTM));
                }
            }
        }

        private CustomerManagementView customerManagementView;
        public CustomerManagementViewModel(CustomerManagementView customerManagementView)
        {
            loadData();
            editCustomer();
            exportExcel();
            delete();
            this.customerManagementView = customerManagementView;
        }

        void loadData()
        {
            CustomerDA customerDA = new CustomerDA();
            DSCTM = customerDA.getDSCustomer();
            searchCTM();
            loadWidthColumn();
        }

        private void loadWidthColumn()
        {
            if (customerManagementView != null)
            {
                customerManagementView.clID.Width = 0;
                customerManagementView.clID.Width = double.NaN;

                customerManagementView.clFullName.Width = 0;
                customerManagementView.clFullName.Width = double.NaN;

                customerManagementView.clSDT.Width = 0;
                customerManagementView.clSDT.Width = double.NaN;

                customerManagementView.clEmail.Width = 0;
                customerManagementView.clEmail.Width = double.NaN;

                customerManagementView.clNgayDangKi.Width = 0;
                customerManagementView.clNgayDangKi.Width = double.NaN;

                customerManagementView.clDiem.Width = 0;
                customerManagementView.clDiem.Width = double.NaN;
            }
        }
    }
}
