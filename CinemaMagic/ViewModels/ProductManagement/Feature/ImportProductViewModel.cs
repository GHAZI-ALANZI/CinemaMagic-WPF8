using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.Views.MessageBox;
using CinemaMagic.Views.ProductManagement;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand importQuantityCommand { get; set; }

        private void import()
        {
            importQuantityCommand = new ViewModelCommand(importQuantity);
        }

        private void importQuantity(object obj)
        {
            ImportProduct importProduct = new ImportProduct(obj as ProductDTO);
            importProduct.ShowDialog();
            loadData();
        }
    }


    public class ImportProductViewModel : MainBaseViewModel
    {

        //Quantity
        private string quantity;
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                ValidateQuantity();
            }
        }
        private string quantityError;
        public string QuantityError
        {
            get => quantityError;
            set
            {
                quantityError = value;
                OnPropertyChanged(nameof(QuantityError));
            }
        }

        ImportProduct wd;
        ProductDTO product;
        public ICommand quitCommand { get; set; }
        public ICommand acceptImportCommand { get; set; }
        public ImportProductViewModel(ImportProduct wd, ProductDTO product)
        {
            this.wd = wd;
            this.product = product;
            Quantity = product.Quantity.ToString();
            Quantity = "";
            quitCommand = new ViewModelCommand(quit);
            acceptImportCommand = new ViewModelCommand(acceptImport, canAccept);
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void acceptImport(object obj)
        {


            ProductDA productDA = new ProductDA();
            productDA.importSL(product, int.Parse(Quantity));

            YesMessageBox mb = new YesMessageBox("Notification", "Successfully added more quantity of the product");
            mb.ShowDialog();
            wd.Close();
        }

        //Validate
        private bool _canAccept;
        private bool canAccept(object obj)
        {
            return _canAccept;
        }
        private void ValidateQuantity()
        {
            if (string.IsNullOrWhiteSpace(Quantity))
            {
                QuantityError = "The quantity cannot be empty";
                _canAccept = false;
            }
            else if (!Quantity.All(char.IsDigit))
            {
                QuantityError = "The quantity is invalid";
                _canAccept = false;
            }
            else if (int.Parse(Quantity) < 0)
            {
                QuantityError = "The quantity is invalid";
                _canAccept = false;
            }
            else
            {
                QuantityError = "";
                _canAccept = true;
            }
        }
    }
}
