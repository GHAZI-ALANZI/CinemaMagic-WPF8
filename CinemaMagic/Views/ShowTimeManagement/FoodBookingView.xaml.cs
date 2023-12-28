using CinemaMagic.Models.DTOs.Bills;
using CinemaMagic.ViewModels.ShowTimeManagementVM;
using System.Windows;

namespace CinemaMagic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for FoodBookingView.xaml
    /// </summary>
    public partial class FoodBookingView : Window
    {
        public FoodBookingView(OrderDTO orderDTO, int Staff_Id, TicketBookingView ticketBookingView)
        {
            InitializeComponent();
            FoodBookingViewModel foodBookingViewModel = new FoodBookingViewModel(this, orderDTO, Staff_Id, ticketBookingView);
            this.DataContext = foodBookingViewModel;
        }
    }
}
