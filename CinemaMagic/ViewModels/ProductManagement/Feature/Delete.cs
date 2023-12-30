using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ProductManagement;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand deleteProductCommand { get; set; }

        void delete()
        {
            deleteProductCommand = new ViewModelCommand(deleteProduct);
        }
        private void deleteProduct(object obj)
        {
            ProductDA productDA = new ProductDA();
            if (obj is ProductDTO product)
            {
                productDA.deleteProduct(product);
                loadData();
            }
        }
    }
}
