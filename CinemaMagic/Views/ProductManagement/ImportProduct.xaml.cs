using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.ViewModels.ProductManagement;
using System.Windows;

namespace CinemaMagic.Views.ProductManagement
{
    /// <summary>
    /// Interaction logic for ImportProduct.xaml
    /// </summary>
    public partial class ImportProduct : Window
    {
        public ImportProduct(ProductDTO product)
        {
            InitializeComponent();
            ImportProductViewModel importProductViewModel = new ImportProductViewModel(this, product);
            this.DataContext = importProductViewModel;
        }
    }
}
