using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.VoucherManagement;
using System.Windows;

namespace CinemaMagic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for EditVoucher.xaml
    /// </summary>
    public partial class EditVoucher : Window
    {
        public EditVoucher(VoucherDTO voucher)
        {
            InitializeComponent();
            EditVoucherViewModel editVoucherViewModel = new EditVoucherViewModel(this);
            editVoucherViewModel.voucher = voucher;
            editVoucherViewModel.loadEditCurrent();
            this.DataContext = editVoucherViewModel;
        }
    }
}
