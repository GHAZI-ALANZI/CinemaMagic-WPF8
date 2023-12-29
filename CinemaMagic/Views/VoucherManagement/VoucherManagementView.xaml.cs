using CinemaMagic.ViewModels.VoucherManagement;
using System.Windows.Controls;

namespace CinemaMagic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for VoucherManagementView.xaml
    /// </summary>
    public partial class VoucherManagementView : UserControl
    {
        public VoucherManagementView()
        {
            InitializeComponent();
            VoucherManagementViewModel viewModel = new VoucherManagementViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
