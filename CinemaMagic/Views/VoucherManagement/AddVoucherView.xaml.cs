using CinemaMagic.ViewModels.VoucherManagement;
using System.Windows;

namespace CinemaMagic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for AddVoucherView.xaml
    /// </summary>
    public partial class AddVoucherView : Window
    {
        public AddVoucherView()
        {
            InitializeComponent();
            AddVoucherViewModel addVoucherViewModel = new AddVoucherViewModel(this);
            this.DataContext = addVoucherViewModel;
        }
    }
}
