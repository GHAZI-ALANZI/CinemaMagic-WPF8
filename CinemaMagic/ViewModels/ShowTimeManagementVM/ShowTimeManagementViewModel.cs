using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<ShowTimeDTO> dSSuatChieu;
        public ObservableCollection<ShowTimeDTO> DSSuatChieu
        {
            get => dSSuatChieu;
            set
            {
                dSSuatChieu = value;
                OnPropertyChanged(nameof(DSSuatChieu));
            }
        }


        private int Staff_Id;
        public ShowTimeManagementViewModel(int Staff_Id)
        {
            ShowTimeDA showTimeDA = new ShowTimeDA();
            showTimeDA.deleteShowtimeDone();
            loadData();// On first opening, go to the "All" section
            Auditorium();
            AddShowTime();
            TicketBooking();
            delete();
            this.Staff_Id = Staff_Id;
        }

        private void loadData(string Phong = "All")//load data by room
        {
            ShowTimeDA showTimeDA = new ShowTimeDA();
            DSSuatChieu = showTimeDA.getDSShowTime(Phong);
            FilterStartDate = null;
        }
    }
}