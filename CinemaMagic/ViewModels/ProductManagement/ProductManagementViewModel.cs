using CinemaMagic.Models.DTOs.ProductManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.ProductManagement
{

    public partial class ProductManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<ProductDTO> DSSP_All;
        private ObservableCollection<ProductDTO> DSSP_ThucAn;
        private ObservableCollection<ProductDTO> DSSP_DoUong;

        public ProductManagementViewModel()
        {
            PhanLoai();
            addProduct();
            delete();
            editProduct();
            import();
        }

    }
}
