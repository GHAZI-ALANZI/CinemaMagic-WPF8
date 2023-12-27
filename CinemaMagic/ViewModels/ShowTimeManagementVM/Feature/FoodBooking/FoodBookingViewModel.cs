using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.Bills;
using CinemaMagic.Models.DTOs.ProductManagement;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class TicketBookingViewModel
    {
        public ICommand ContinueCommand { get; set; }

        private void FoodBooking()
        {
            ContinueCommand = new ViewModelCommand(Continue, CanContinue);
        }

        private void Continue(object obj)
        {
            orderDTO.Seats = Seats;
            orderDTO.TotalTicket = TotalPriceTicket;
            FoodBookingView foodBookingView = new FoodBookingView(orderDTO, Staff_Id, ticketBookingView);
            foodBookingView.ShowDialog();
        }

        private bool CanContinue(object obj)
        {
            if (Seats == "No seats have been selected yet!")
            {
                return false;
            }
            return true;
        }
    }



    public partial class FoodBookingViewModel : MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }
        private ObservableCollection<ProductDTO> dSSP;
        public ObservableCollection<ProductDTO> DSSP
        {
            get => dSSP;
            set
            {
                dSSP = value;
                OnPropertyChanged(nameof(DSSP));
            }
        }


        FoodBookingView foodBookingView;
        private OrderDTO orderDTO;
        private int Staff_Id;
        TicketBookingView ticketBookingView;
        public FoodBookingViewModel(FoodBookingView foodBookingView, OrderDTO orderDTO, int Staff_Id, TicketBookingView ticketBookingView)
        {
            BackCommand = new ViewModelCommand(Back);
            this.foodBookingView = foodBookingView;
            this.orderDTO = orderDTO;
            loadDSSP(0);
            Filter();
            Add();
            Delete();
            Bill();
            this.Staff_Id = Staff_Id;
            this.ticketBookingView = ticketBookingView;
        }

        private void Back(object obj)
        {
            foodBookingView.Close();
        }

        private void loadDSSP(int type)
        {
            ProductDA productDA = new ProductDA();
            DSSP = productDA.getDSSPTheoLoai(type);
        }
    }
}
