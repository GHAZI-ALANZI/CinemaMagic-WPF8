using CinemaMagic.Models.DTOs.Bills;
using CinemaMagic.ViewModels.ShowTimeManagementVM;
using System.Windows;

namespace CinemaMagic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for BillView.xaml
    /// </summary>
    public partial class BillView : Window
    {
        public BillView(OrderDTO orderDTO, int Staff_Id, TicketBookingView ticketBookingView, FoodBookingView foodBookingView)
        {
            InitializeComponent();
            BillViewModel billViewModel = new BillViewModel(this, orderDTO, Staff_Id, ticketBookingView, foodBookingView);
            this.DataContext = billViewModel;
        }
    }
}
