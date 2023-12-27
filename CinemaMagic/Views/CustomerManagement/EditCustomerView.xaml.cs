using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.CustomerManagement;
using System.Windows;

namespace CinemaMagic.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for EditCustomerView.xaml
    /// </summary>
    public partial class EditCustomerView : Window
    {
        public EditCustomerView(CustomerDTO customer)
        {
            InitializeComponent();
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(this);
            editCustomerViewModel.customer = customer;
            editCustomerViewModel.loadEditCurrent();
            this.DataContext = editCustomerViewModel;
        }
    }
}
