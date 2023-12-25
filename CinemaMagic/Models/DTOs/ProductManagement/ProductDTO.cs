using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace CinemaMagic.Models.DTOs.ProductManagement
{
    public class ProductDTO : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int quantity;
        private int price;
        private int purchasePrice;
        private int type;
        private BitmapImage imageSource;
        private int quantity_Choice = 1;
        private int totalPrice_QuantityChoice;


        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public int PurchasePrice { get { return purchasePrice; } set { purchasePrice = value; } }

        public int Quantity_Choice
        {
            get => quantity_Choice;
            set
            {
                quantity_Choice = value;
                OnPropertyChanged(nameof(Quantity_Choice));
            }
        }
        public int TotalPrice_QuantityChoice
        {
            get => totalPrice_QuantityChoice;
            set
            {
                totalPrice_QuantityChoice = value;
                OnPropertyChanged(nameof(TotalPrice_QuantityChoice));
            }
        }


        public int Price { get { return price; } set { price = value; } }
        public int Type { get { return type; } set { type = value; } }
        public BitmapImage ImageSource { get { return imageSource; } set { imageSource = value; } }


        public ProductDTO(int id, string name, int quantity, int purchasePrice, int price, int type, BitmapImage imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }


        public ProductDTO(int id, string name, int quantity, int purchasePrice, int type, BitmapImage imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Type = type;
            ImageSource = imageSource;
        }

        public ProductDTO(string name, int quantity, int purchasePrice, int type, BitmapImage imageSource)
        {
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Type = type;
            ImageSource = imageSource;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}