using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<VoucherDTO> dsvc;
        public ObservableCollection<VoucherDTO> DSVC
        {
            get => dsvc;
            set
            {
                if (dsvc != value)
                {
                    dsvc = value;
                    OnPropertyChanged(nameof(DSVC));
                }
            }
        }

        private VoucherManagementView voucherManagementView; // Serve to reset width when editing, deleting, or adding
        public VoucherManagementViewModel(VoucherManagementView view)
        {
            loadData();
            AddVoucher();
            editVoucher();
            exportExcel();
            VoucherDetail();
            SendVoucher();
            this.voucherManagementView = view;
        }

        void loadData()
        {
            VoucherDA voucherDA = new VoucherDA();
            DSVC = voucherDA.getDSVC();
            searchVoucher();
            loadWidthColumn();
        }
        private void loadWidthColumn()
        {
            if (voucherManagementView != null)
            {
                voucherManagementView.clCode.Width = 0;
                voucherManagementView.clCode.Width = double.NaN;

                voucherManagementView.clName.Width = 0;
                voucherManagementView.clName.Width = double.NaN;

                voucherManagementView.clType.Width = 0;
                voucherManagementView.clType.Width = double.NaN;

                voucherManagementView.clStartDate.Width = 0;
                voucherManagementView.clStartDate.Width = double.NaN;

                voucherManagementView.clFinDate.Width = 0;
                voucherManagementView.clFinDate.Width = double.NaN;

            }
        }
    }
}
