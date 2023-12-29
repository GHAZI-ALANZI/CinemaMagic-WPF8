using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using CinemaMagic.Views.MessageBox;
using CinemaMagic.Views.ShowTimeManagement;
using System.Globalization;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand AddShowTimeCommand { get; set; }

        private void AddShowTime()
        {
            AddShowTimeCommand = new ViewModelCommand(addShowTime);
        }

        private void addShowTime(object obj)
        {
            AddShowTimeView addShowTimeView = new AddShowTimeView(phong);
            addShowTimeView.ShowDialog();

            loadData(phong);//In which room, add the showtimes for that room, so I load according to that room.
        }
    }

    public class AddShowTimeViewModel : MainBaseViewModel
    {
        public List<Tuple<int, string>> DSPhim { get; set; }//List of currently screening movies

        public List<Tuple<int, string>> DSPhong { get; set; }//List of rooms

        // Selected movie
        public Tuple<int, string> SelectedPhim { get; set; }



        private Tuple<int, string> selectedPhong;
        public Tuple<int, string> SelectedPhong
        {
            get => selectedPhong;
            set
            {
                selectedPhong = value;
                OnPropertyChanged(nameof(SelectedPhong));
            }
        }


        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
            }
        }


        private DateTime? showtime;
        public DateTime? Showtime
        {
            get => showtime;
            set
            {
                showtime = value;
            }
        }



        private string perSeatTicketPrice;
        public string PerSeatTicketPrice
        {
            get => perSeatTicketPrice;
            set
            {
                perSeatTicketPrice = value;
                ValidateTicket();

            }
        }
        private string perSeatTicketPriceE;
        public string PerSeatTicketPriceE
        {
            get => perSeatTicketPriceE;
            set
            {
                perSeatTicketPriceE = value;
                OnPropertyChanged(nameof(PerSeatTicketPriceE));
            }
        }





        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        AddShowTimeView addShowTimeView;
        public AddShowTimeViewModel(string phong, AddShowTimeView addShowTimeView)
        {
            loadData();
            AcceptCommand = new ViewModelCommand(Accept, canAcceptAdd);
            CancelCommand = new ViewModelCommand(Cancel);
            this.addShowTimeView = addShowTimeView;
            loadSelectedPhong(phong);
        }


        private void loadData()
        {
            MovieDA movieDA = new MovieDA();
            DSPhim = movieDA.getDSTitleDPH();

            AuditoriumDA auditoriumDA = new AuditoriumDA();
            DSPhong = auditoriumDA.getDSPhong();
        }


        private void Accept(object obj)
        {

            //starttime
            try
            {
                string formatStartDate = StartDate.Value.ToString("yyyy-MM-dd");
                string formatShowtime = Showtime.Value.ToString("HH:mm:ss");
                string StartTime = formatStartDate + " " + formatShowtime;


                //endtime
                DateTime? endTime;

                MovieDA movieDA = new MovieDA();
                string EndTime = StartTime;
                try
                {
                    endTime = DateTime.Parse(StartTime).AddMinutes(movieDA.MovieLength(SelectedPhim.Item1));
                    EndTime = endTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }

                try
                {
                    DateTime startTimeByADD = DateTime.ParseExact(StartTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    ShowTimeDA showTimeDA = new ShowTimeDA();
                    if (showTimeDA.canAddShowtime(SelectedPhong.Item2, startTimeByADD))
                    {

                        showTimeDA.addShowtime(new ShowTimeDTO(StartTime, EndTime, int.Parse(PerSeatTicketPrice), SelectedPhim.Item1, SelectedPhong.Item1));

                        YesMessageBox mb = new YesMessageBox("Notification", "Showtime added successfully");
                        mb.ShowDialog();
                        addShowTimeView.Close();
                    }
                    else
                    {
                        YesMessageBox mb = new YesMessageBox("Notification", "Another showtime is already scheduled for this time slot!");
                        mb.ShowDialog();
                        addShowTimeView.Close();
                    }

                }
                catch { addShowTimeView.Close(); }
            }
            catch { addShowTimeView.Close(); }
        }


        private void Cancel(object obj)
        {
            addShowTimeView.Close();
        }


        private void loadSelectedPhong(string phong)
        {
            if (phong != "All")
            {
                AuditoriumDA auditoriumDA = new AuditoriumDA();
                SelectedPhong = new Tuple<int, string>(auditoriumDA.AuditoriumId(phong), phong);
            }
        }


        private bool[] _canAccept = new bool[1];
        private bool canAcceptAdd(object obj)
        {
            return _canAccept[0] && selectedPhong != null && SelectedPhim != null && showtime != null && startDate != null;
        }

        private void ValidateTicket()
        {
            if (string.IsNullOrWhiteSpace(PerSeatTicketPrice))
            {
                PerSeatTicketPriceE = "Not empty!";
                _canAccept[0] = false;
            }
            else if (!PerSeatTicketPrice.All(char.IsDigit))
            {
                PerSeatTicketPriceE = "Invalid!";
                _canAccept[0] = false;
            }
            else if (!int.TryParse(PerSeatTicketPrice, out int lengthvalue))
            {
                PerSeatTicketPriceE = "Invalid!";
                _canAccept[0] = false;
            }
            else
            {
                PerSeatTicketPriceE = "";
                _canAccept[0] = true;
            }
        }
    }
}
