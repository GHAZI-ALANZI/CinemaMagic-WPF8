using CinemaMagic.ViewModels.CustomerManagement;
using System.Windows.Controls;

namespace CinemaMagic.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CustomerManagementView.xaml
    /// </summary>
    public partial class CustomerManagementView : UserControl
    {
        public CustomerManagementView()
        {
            InitializeComponent();
            CustomerManagementViewModel viewModel = new CustomerManagementViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
