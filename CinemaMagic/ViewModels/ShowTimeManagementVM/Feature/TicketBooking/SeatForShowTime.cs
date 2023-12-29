using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using CinemaMagic.Views.MessageBox;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class TicketBookingViewModel
    {
        private ObservableCollection<SeatForShowTimeDTO> dSGhe;
        public ObservableCollection<SeatForShowTimeDTO> DSGhe//Load the list of seats.
        {
            get => dSGhe;
            set
            {
                dSGhe = value;
                OnPropertyChanged(nameof(DSGhe));
            }
        }

        public ObservableCollection<SeatForShowTimeDTO> DSGheChon;//List of selected seats

        public ICommand SelectedSeatCommand { get; set; }

        private void Seat()
        {
            loadSeat();
            SelectedSeatCommand = new ViewModelCommand(selectedSeat);
        }

        List<string> checkSeats;
        private void loadSeat()
        {
            if (showTimeDTO != null)
            {
                SeatForShowTimeDA seatForShowTimeDA = new SeatForShowTimeDA();
                DSGhe = seatForShowTimeDA.getDSGhe(showTimeDTO.Id);
                DSGheChon = new ObservableCollection<SeatForShowTimeDTO>();
                orderDTO.DSGheChon = DSGheChon;
                checkSeats = new List<string>();
                foreach (var item in DSGhe)
                {
                    if (item.Condition)
                    {
                        checkSeats.Add(item.Location);
                    }
                }
            }
        }


        private void selectedSeat(object obj)
        {
            if (obj is SeatForShowTimeDTO seatForShowTimeDTO)
            {
                if (!checkSeats.Contains(seatForShowTimeDTO.Location))
                {
                    if (Seats == "No seats have been selected yet!")
                    {
                        Seats = "";
                    }
                    seatForShowTimeDTO.Condition = !seatForShowTimeDTO.Condition;

                    if (seatForShowTimeDTO.Condition)
                    {
                        if (Seats != "")
                        {
                            Seats += "," + seatForShowTimeDTO.Location;
                        }
                        else
                        {
                            Seats = seatForShowTimeDTO.Location;
                        }
                        TotalPriceTicket += showTimeDTO.PerSeatTicketPrice;
                        DSGheChon.Add(seatForShowTimeDTO);
                    }
                    else
                    {
                        TotalPriceTicket -= showTimeDTO.PerSeatTicketPrice;
                        DSGheChon.Remove(seatForShowTimeDTO);

                        if (Seats.Contains(','))
                        {
                            string[] kq = Seats.Split(',');
                            Seats = "";
                            foreach (string item in kq)
                            {
                                if (item != seatForShowTimeDTO.Location)
                                {
                                    if (Seats != "")
                                    {
                                        Seats += "," + item;
                                    }
                                    else
                                    {
                                        Seats += item;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Seats = "";
                        }
                    }
                }
                else
                {
                    YesMessageBox mb = new YesMessageBox("Notification", "Seat has already been booked");
                    mb.ShowDialog();
                }
            }
        }
    }
}
