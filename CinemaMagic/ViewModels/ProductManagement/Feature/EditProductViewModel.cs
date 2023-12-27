using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ProductManagement;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand showWDEditProductCommand { get; set; }

        void editProduct()
        {
            showWDEditProductCommand = new ViewModelCommand(ShowWDEditProduct);
        }
        private void ShowWDEditProduct(object obj)
        {
            //truyền ID để phục vụ update
            ProductDTO product = obj as ProductDTO;
            EditProduct editProduct = new EditProduct(product);


            editProduct.ShowDialog();
            //load data
            loadData();
        }
    }

    public class EditProductViewModel : INotifyPropertyChanged
    {
        public ProductDTO productEdit { get; set; }


        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
            }
        }
        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }



        private BitmapImage imageSource;
        public BitmapImage? ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

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

        //purchasePrice
        private string purchasePrice;
        public string PurchasePrice
        {
            get => purchasePrice;
            set
            {
                purchasePrice = value;
                ValidatePrice();
            }
        }
        private string purchasePriceError;
        public string PurchasePriceError
        {
            get => purchasePriceError;
            set
            {
                purchasePriceError = value;
                OnPropertyChanged(nameof(PurchasePriceError));
            }
        }
        public int Type { get; set; }

        EditProduct wd;

        public ICommand quitCommand { get; set; }
        public ICommand addImageCommand { get; set; }
        public ICommand acceptEditCommand { get; set; }
        public ICommand WindowClosingCommand { get; set; }

        public EditProductViewModel(EditProduct wd)
        {
            quitCommand = new ViewModelCommand(quit);
            addImageCommand = new ViewModelCommand(addImage);
            acceptEditCommand = new ViewModelCommand(acceptEdit, canAccept);
            WindowClosingCommand = new ViewModelCommand(windowClosing);
            this.wd = wd;


        }

        public void khoitao()
        {
            Name = productEdit.Name;
            Quantity = productEdit.Quantity.ToString();
            purchasePrice = productEdit.PurchasePrice.ToString();
            Type = productEdit.Type - 1;


            ImageSource = productEdit.ImageSource;
        }


        private void quit(object obj)
        {
            wd.Close();
        }


        //accept edit
        private void acceptEdit(object obj)
        {
            string nameNew = Name;
            int quantityNew = int.Parse(Quantity);
            int purchasePriceNew = int.Parse(PurchasePrice);
            int typeNew = Type + 1;
            ProductDA productDA = new ProductDA();
            productDA.editProduct(new ProductDTO(productEdit.Id, nameNew, quantityNew, purchasePriceNew, typeNew, ImageSource));
            YesMessageBox mb = new YesMessageBox("Notification", "Success");
            mb.ShowDialog();
            wd.Close();
        }


        //add image
        private void addImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            byte[] imageData;

            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);

                ImageSource = ImageVsSQL.ByteArrayToBitmapImage(imageData);
            }
        }

        private void windowClosing(object obj)
        {

        }




        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Validate 

        private bool[] _canAccept = new bool[] { true, true, true };

        private bool canAccept(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2];
        }
        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "The product name cannot be empty!";
                _canAccept[0] = false;
            }
            else
            {
                NameError = "";
                _canAccept[0] = true;
            }
        }

        private void ValidateQuantity()
        {
            if (string.IsNullOrWhiteSpace(Quantity))
            {
                QuantityError = "The quantity cannot be empty";
                _canAccept[1] = false;
            }
            else if (!Quantity.All(char.IsDigit))
            {
                QuantityError = "The quantity is invalid";
                _canAccept[1] = false;
            }
            else if (!int.TryParse(Quantity, out int Quantityvalue))
            {
                QuantityError = "Invalid!";
                _canAccept[1] = false;
            }
            else
            {
                QuantityError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidatePrice()
        {
            if (string.IsNullOrWhiteSpace(PurchasePrice))
            {
                PurchasePriceError = "The price cannot be empty";
                _canAccept[2] = false;
            }
            else if (!PurchasePrice.All(char.IsDigit))
            {
                PurchasePriceError = "The price is invalid";
                _canAccept[2] = false;
            }
            else if (!int.TryParse(Quantity, out int Pricevalue))
            {
                PurchasePriceError = "Invalid!";
                _canAccept[2] = false;
            }
            else
            {
                PurchasePriceError = "";
                _canAccept[2] = true;
            }
        }
    }
}
