using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.ViewModels.ProductManagement;
using System.Windows;

namespace CinemaMagic.Views.ProductManagement
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        public EditProduct(ProductDTO productEdit)
        {
            InitializeComponent();
            EditProductViewModel editProductViewModel = new EditProductViewModel(this);
            editProductViewModel.productEdit = productEdit;
            editProductViewModel.khoitao();
            this.DataContext = editProductViewModel;
        }
    }
}
