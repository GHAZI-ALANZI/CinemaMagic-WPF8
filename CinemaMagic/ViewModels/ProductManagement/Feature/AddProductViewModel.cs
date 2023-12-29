using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.Views.MessageBox;
using CinemaMagic.Views.ProductManagement;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand showWDAddProductCommand { get; set; }

        void addProduct()
        {
            showWDAddProductCommand = new ViewModelCommand(ShowWDAddProduct);
        }
        private void ShowWDAddProduct(object obj)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();

            //load data
            loadData();
        }
    }


    public class AddProductViewModel : MainBaseViewModel
    {
        bool checkImage = false;

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

        //PurchasePrice
        private string purchasePrice;
        public string PurchasePrice
        {
            get => purchasePrice;
            set
            {
                purchasePrice = value;
                ValidatePurchasePrice();
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

        AddProduct wd;
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand { get; set; }
        public ICommand addImageCommand { get; set; }

        public AddProductViewModel(AddProduct wd)
        {
            Type = 0;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept, canAccept);
            addImageCommand = new ViewModelCommand(addImage);
            this.wd = wd;
        }

        private void quit(object obj)
        {
            wd.Close();
        }



        private void accept(object obj)
        {
            if (checkImage == false)
            {
                YesMessageBox mbb = new YesMessageBox("Notification", "You haven't added a photo yet!");
                mbb.ShowDialog();
                return;
            }
            Type += 1;
            ProductDA productDA = new ProductDA();
            productDA.addProduct(new ProductDTO(Name, int.Parse(Quantity), int.Parse(PurchasePrice), Type, ImageSource));
            YesMessageBox mb = new YesMessageBox("Notification", "Product added successfully");
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
                checkImage = true;
            }
        }





        private bool[] _canAccept = new bool[3];

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
            else if (int.Parse(Quantity) < 0)
            {
                QuantityError = "The quantity is invalid";
                _canAccept[1] = false;
            }
            else
            {
                QuantityError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidatePurchasePrice()
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
            else if (!int.TryParse(PurchasePrice, out int PurchasePriceValue))
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
