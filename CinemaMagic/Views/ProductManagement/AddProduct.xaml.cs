using CinemaMagic.ViewModels.ProductManagement;
using System.Windows;

namespace CinemaMagic.Views.ProductManagement
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            AddProductViewModel wd = new AddProductViewModel(this);
            this.DataContext = wd;
        }
    }
}
