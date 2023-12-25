using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.Models.DTOs.Bills
{
    public class OrderDTO
    {

        public ObservableCollection<ProductDTO> DSSPChon { get; set; }

        public int TotalProduct;

        public ObservableCollection<SeatForShowTimeDTO> DSGheChon { get; set; }

        public string Seats { get; set; }

        public int TotalTicket;
        public ShowTimeDTO showTimeDTO { get; set; }


        public OrderDTO()
        {

        }
    }
}
