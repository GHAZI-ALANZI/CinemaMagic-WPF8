﻿using CinemaMagic.Models.DTOs.ProductManagement;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand AddProductCommand { get; set; }
        public ICommand Cong1QuantityChoiceCommand { get; set; }
        public ICommand Tru1QuantityChoiceCommand { get; set; }

        private ObservableCollection<ProductDTO> dSSPChon;
        public ObservableCollection<ProductDTO> DSSPChon
        {
            get => dSSPChon;
            set
            {
                dSSPChon = value;
                OnPropertyChanged(nameof(DSSPChon));
            }
        }

        private int totalPrice;
        public int TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }


        private void Add()
        {
            AddProductCommand = new ViewModelCommand(AddProduct);
            DSSPChon = new ObservableCollection<ProductDTO>();
            Cong1QuantityChoiceCommand = new ViewModelCommand(Cong1QuantityChoice);
            Tru1QuantityChoiceCommand = new ViewModelCommand(Tru1QuantityChoice);
            orderDTO.DSSPChon = DSSPChon;
        }



        private void AddProduct(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                if (!DSSPChon.Contains(productDTO))
                {
                    DSSPChon.Add(productDTO);
                    TotalPrice += productDTO.Price;
                }
                else
                {
                    if (productDTO.Quantity_Choice < productDTO.Quantity)
                    {
                        productDTO.Quantity_Choice += 1;
                        TotalPrice += productDTO.Price;
                    }
                }
            }
        }


        private void Cong1QuantityChoice(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                if (productDTO.Quantity_Choice < productDTO.Quantity)
                {
                    productDTO.Quantity_Choice += 1;
                    TotalPrice += productDTO.Price;
                }
            }
        }

        private void Tru1QuantityChoice(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                if (productDTO.Quantity_Choice > 1)
                {
                    productDTO.Quantity_Choice -= 1;
                    TotalPrice -= productDTO.Price;
                }
                else
                {
                    DSSPChon.Remove(productDTO);
                    TotalPrice -= productDTO.Price;
                }
            }
        }
    }
}
