using CinemaMagic.Models.DTOs.Bills;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using CinemaMagic.ViewModels.ShowTimeManagementVM;
using System.Windows;

namespace CinemaMagic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for TicketBookingView.xaml
    /// </summary>
    public partial class TicketBookingView : Window
    {
        public TicketBookingView(ShowTimeDTO showTimeDTO, OrderDTO orderDTO, int Staff_Id)
        {
            InitializeComponent();
            TicketBookingViewModel ticketBookingViewModel = new TicketBookingViewModel(showTimeDTO, orderDTO, Staff_Id, this);
            this.DataContext = ticketBookingViewModel;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
