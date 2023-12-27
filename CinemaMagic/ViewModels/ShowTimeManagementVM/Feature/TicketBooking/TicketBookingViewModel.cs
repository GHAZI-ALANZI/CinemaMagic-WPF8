using CinemaMagic.Models.DTOs.Bills;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand TicketBookingCommand { get; set; }

        private void TicketBooking()
        {
            TicketBookingCommand = new ViewModelCommand(ticketBooking);
        }


        private OrderDTO orderDTO;
        private void ticketBooking(object obj)
        {
            if (obj != null)
            {
                orderDTO = new OrderDTO();
                TicketBookingView ticketBookingView = new TicketBookingView(obj as ShowTimeDTO, orderDTO, Staff_Id);
                ticketBookingView.ShowDialog();
            }
        }
    }



    public partial class TicketBookingViewModel : MainBaseViewModel
    {
        public BitmapImage ImageSource { get; set; }


        public string Title { get; set; }


        public string Showtime { get; set; }


        public string NameAuditorium { get; set; }


        public int PerSeatTicketPrice { get; set; }


        private string seats;
        public string Seats
        {
            get => seats;
            set
            {
                seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        private int totalPriceTicket;
        public int TotalPriceTicket
        {
            get => totalPriceTicket;
            set
            {
                totalPriceTicket = value;
                OnPropertyChanged(nameof(TotalPriceTicket));
            }
        }

        private ShowTimeDTO showTimeDTO;

        private OrderDTO orderDTO;
        private int Staff_Id;

        TicketBookingView ticketBookingView;
        public TicketBookingViewModel(ShowTimeDTO showTimeDTO, OrderDTO orderDTO, int Staff_Id, TicketBookingView ticketBookingView)
        {
            this.showTimeDTO = showTimeDTO;
            this.orderDTO = orderDTO;
            orderDTO.showTimeDTO = showTimeDTO;
            loadShowTimeCurrent();
            Seat();
            FoodBooking();
            this.Staff_Id = Staff_Id;
            this.ticketBookingView = ticketBookingView;
        }


        private void loadShowTimeCurrent()
        {
            if (showTimeDTO != null)
            {
                ImageSource = showTimeDTO.ImageSource;
                Title = showTimeDTO.MovieTitle;
                Showtime = showTimeDTO.ShowTime;
                NameAuditorium = showTimeDTO.NameAuditorium;
                PerSeatTicketPrice = showTimeDTO.PerSeatTicketPrice;
                Seats = "No seats have been selected yet!";
                TotalPriceTicket = 0;
            }
        }
    }
}
