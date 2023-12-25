using System.Windows.Media.Imaging;

namespace CinemaMagic.Models.DTOs.ShowTimeManagement
{
    public class ShowTimeDTO
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string ShowTime { get; set; }
        public string EndTime { get; set; }
        public int PerSeatTicketPrice { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int Length { get; set; }
        public int Auditorium_Id { get; set; }
        public BitmapImage ImageSource { get; set; }
        public string NameAuditorium { get; set; }


        public ShowTimeDTO(int id, string startTime, string endTime, int perSeatTicketPrice, int movieId, string movieTitle, int length, BitmapImage imageSource, string nameAuditorium, int auditorium_Id)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            MovieId = movieId;
            PerSeatTicketPrice = perSeatTicketPrice;
            MovieTitle = movieTitle;
            Length = length;
            ImageSource = imageSource;
            NameAuditorium = nameAuditorium;
            Auditorium_Id = auditorium_Id;

            string[] s = StartTime.Split(' ');
            ShowTime = s[1];
        }



        public ShowTimeDTO(string startTime, string endTime, int perSeatTicketPrice, int movieId, int auditorium_Id)
        {
            StartTime = startTime;
            EndTime = endTime;
            MovieId = movieId;
            PerSeatTicketPrice = perSeatTicketPrice;
            Auditorium_Id = auditorium_Id;
        }


    }
}
