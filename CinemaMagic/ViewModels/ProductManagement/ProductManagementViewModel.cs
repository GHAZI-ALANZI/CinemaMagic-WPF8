using CinemaMagic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaMagic.Models.DataAccessLayer;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Reflection.Metadata;
using System.IO;

namespace CinemaMagic.ViewModels.ProductManagement
{
    
    public partial class ProductManagementViewModel:MainBaseViewModel
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
