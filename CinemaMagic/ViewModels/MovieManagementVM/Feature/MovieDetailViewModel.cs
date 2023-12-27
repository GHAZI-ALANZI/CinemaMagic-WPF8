using CinemaMagic.Models.DTOs;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand MovieDetailCommand { get; set; }
        private void MovieDetail()
        {
            MovieDetailCommand = new ViewModelCommand(movieDetail);
        }

        private void movieDetail(object obj)
        {
            if (obj != null)
            {
                MovieDetailView movieDetailView = new MovieDetailView(obj as MovieDTO);
                movieDetailView.ShowDialog();
            }
        }
    }


    public class MovieDetailViewModel : MainBaseViewModel
    {
        private MovieDTO movieDTO;

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }



        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }



        private string genre;
        public string Genre
        {
            get => genre;
            set
            {
                genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }


        private string director;
        public string Director
        {
            get => director;
            set
            {
                director = value;
                OnPropertyChanged(nameof(Director));
            }
        }



        private string releaseYear;
        public string ReleaseYear
        {
            get => releaseYear;
            set
            {
                releaseYear = value;
                OnPropertyChanged(nameof(ReleaseYear));
            }
        }



        private string language;
        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }



        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }



        private string length;
        public string Length
        {
            get => length;
            set
            {
                length = value;
                OnPropertyChanged(nameof(Length));
            }
        }


        //trailer
        private string trailer;
        public string Trailer
        {
            get => trailer;
            set
            {
                trailer = value;
                OnPropertyChanged(nameof(Trailer));
            }
        }



        private string startDate;
        public string StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }



        private string status;
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        //poster
        private BitmapImage imageSource;
        public BitmapImage? ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        private string importprice;
        public string ImportPrice
        {
            get => importprice;
            set
            {
                importprice = value;
                OnPropertyChanged(nameof(ImportPrice));
            }
        }
        public MovieDetailViewModel(MovieDTO movieDTO)
        {
            this.movieDTO = movieDTO;
            loadMovie();
        }


        private void loadMovie()
        {
            Title = movieDTO.Title;
            Description = movieDTO.Description;
            Genre = movieDTO.Genre;
            Director = movieDTO.Director;
            ReleaseYear = movieDTO.ReleaseYear;
            Language = movieDTO.Language;
            Country = movieDTO.Country;
            Length = movieDTO.Length.ToString();
            Trailer = movieDTO.Trailer;
            StartDate = movieDTO.StartDate;
            Status = movieDTO.Status;
            ImportPrice = movieDTO.ImportPrice.ToString("N0");
            ImageSource = movieDTO.ImageSource;
        }
    }
}
