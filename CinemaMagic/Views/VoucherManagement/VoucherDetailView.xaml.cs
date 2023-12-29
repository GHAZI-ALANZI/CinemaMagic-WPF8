using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.VoucherManagement;
using System.Windows;

namespace CinemaMagic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for VoucherDetailView.xaml
    /// </summary>
    public partial class VoucherDetailView : Window
    {
        public VoucherDetailView(VoucherDTO voucherDTO)
        {
            InitializeComponent();
            VoucherDetailViewModel model = new VoucherDetailViewModel(voucherDTO, this);
            this.DataContext = model;
        }
    }
}
