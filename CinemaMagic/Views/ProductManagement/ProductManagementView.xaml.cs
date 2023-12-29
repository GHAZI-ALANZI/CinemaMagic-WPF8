using CinemaMagic.ViewModels.ProductManagement;
using System.Windows.Controls;

namespace CinemaMagic.Views.ProductManagement
{
    /// <summary>
    /// Interaction logic for ProductManagementView.xaml
    /// </summary>
    public partial class ProductManagementView : UserControl
    {
        public ProductManagementView()
        {
            InitializeComponent();
            ProductManagementViewModel viewModel = new ProductManagementViewModel();
            this.DataContext = viewModel;
        }
    }
}
