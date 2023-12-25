using System.ComponentModel;

namespace CinemaMagic.Models.DTOs.ShowTimeManagement
{
    public class SeatForShowTimeDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int Seat_Id { get; set; }
        public int ShowTime_Id { get; set; }
        public string Location { get; set; }

        private bool condition;
        public bool Condition
        {
            get => condition;
            set
            {
                condition = value;
                OnPropertyChanged(nameof(Condition));
            }
        }



        public SeatForShowTimeDTO(int id, int seat_Id, int showTime_Id, string location, bool condition)
        {
            Id = id;
            Seat_Id = seat_Id;
            ShowTime_Id = showTime_Id;
            Location = location;
            Condition = condition;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

