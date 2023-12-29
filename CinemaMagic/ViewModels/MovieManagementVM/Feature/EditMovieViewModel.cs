﻿using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.MessageBox;
using CinemaMagic.Views.MovieManagement;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {

        public ICommand EditMovieCommand { get; set; }

        private void edit()
        {
            EditMovieCommand = new ViewModelCommand(EditMovie);
        }

        private void EditMovie(object obj)
        {

            EditFilmView editFilmView = new EditFilmView(obj as MovieDTO);
            editFilmView.ShowDialog();

            loadData();
        }

    }


    public class EditMovieViewModel : MainBaseViewModel
    {
        private MovieDTO movieDTO;

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                ValidateTitle();

            }
        }
        private string titleError;
        public string TitleError
        {
            get => titleError;
            set
            {
                titleError = value;
                OnPropertyChanged(nameof(TitleError));
            }
        }



        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                ValidateDescription();

            }
        }

        private string descriptionError;
        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged(nameof(DescriptionError));
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
                ValidateDirector();
            }
        }
        private string directorError;
        public string DirectorError
        {
            get => directorError;
            set
            {
                directorError = value;
                OnPropertyChanged(nameof(DirectorError));
            }
        }



        private string releaseYear;
        public string ReleaseYear
        {
            get => releaseYear;
            set
            {
                releaseYear = value;
                ValidateReleaseYear();
            }
        }
        private string releaseYearError;
        public string ReleaseYearError
        {
            get => releaseYearError;
            set
            {
                releaseYearError = value;
                OnPropertyChanged(nameof(ReleaseYearError));
            }
        }



        private string language;
        public string Language
        {
            get => language;
            set
            {
                language = value;
                ValidateLanguage();
            }
        }
        private string languageError;
        public string LanguageError
        {
            get => languageError;
            set
            {
                languageError = value;
                OnPropertyChanged(nameof(LanguageError));
            }
        }



        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                ValidateCountry();
            }
        }

        private string countryError;
        public string CountryError
        {
            get => countryError;
            set
            {
                countryError = value;
                OnPropertyChanged(nameof(CountryError));
            }
        }



        private string length;
        public string Length
        {
            get => length;
            set
            {
                length = value;
                ValidateLength();
            }
        }
        private string lengthError;
        public string LengthError
        {
            get => lengthError;
            set
            {
                lengthError = value;
                OnPropertyChanged(nameof(LengthError));
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
                ValidateTrailer();
            }
        }
        private string trailerError;
        public string TrailerError
        {
            get => trailerError;
            set
            {
                trailerError = value;
                OnPropertyChanged(nameof(TrailerError));
            }
        }



        private DateTime? startDate;
        public DateTime? StartDate
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


        private string importPrice;
        public string ImportPrice
        {
            get => importPrice;
            set
            {
                importPrice = value;
                ValidateImportPrice();
            }
        }
        private string importPriceError;
        public string ImportPriceError
        {
            get => importPriceError;
            set
            {
                importPriceError = value;
                OnPropertyChanged(nameof(ImportPriceError));
            }
        }


        public ICommand acceptCommand { get; set; }
        public ICommand addImageCommand { get; set; }

        private EditFilmView editFilmView;
        public EditMovieViewModel(MovieDTO movieDTO, EditFilmView editFilmView)
        {
            this.movieDTO = movieDTO;
            loadMovieCurrent();
            acceptCommand = new ViewModelCommand(accept, canAcceptAdd);
            addImageCommand = new ViewModelCommand(addImage);
            this.editFilmView = editFilmView;
        }

        private void loadMovieCurrent()
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


            string[] startDate = movieDTO.StartDate.Split('/');
            StartDate = new DateTime(int.Parse(startDate[2]), int.Parse(startDate[1]), int.Parse(startDate[0]));

            Status = movieDTO.Status;
            ImportPrice = movieDTO.ImportPrice.ToString();
            ImageSource = movieDTO.ImageSource;
        }

        private void accept(object obj)
        {

            MovieDA movieDA = new MovieDA();

            try
            {
                movieDA.editMovie(new MovieDTO(movieDTO.Id, Title, Description, Director, ReleaseYear, Language, Country, int.Parse(Length), Trailer, StartDate.Value.ToString("yyyy-MM-dd"), Genre, Status, ImageSource, int.Parse(ImportPrice)));
            }
            catch { }

            //cập nhật lại billAddMovie chỗ này: khỏi nha vì tạo trigger roài
            YesMessageBox mb = new YesMessageBox("Notification", "Movie information updated successfully");
            mb.ShowDialog();
            mb.Close();

            editFilmView.Close();
        }


        //add image
        private void addImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            byte[] imageData;

            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);

                ImageSource = ImageVsSQL.ByteArrayToBitmapImage(imageData);
            }
        }


        //validate 
        private bool[] _canAccept = new bool[9];
        private bool canAcceptAdd(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] && _canAccept[5] &&
                _canAccept[6] && _canAccept[7] && _canAccept[8];
        }
        private void ValidateTitle()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                TitleError = "Not empty!";
                _canAccept[0] = false;
            }
            else
            {
                TitleError = "";
                _canAccept[0] = true;
            }
        }

        private void ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                DescriptionError = "Not empty!";
                _canAccept[1] = false;
            }
            else
            {
                DescriptionError = "";
                _canAccept[1] = true;
            }
        }

        private void ValidateDirector()
        {
            if (string.IsNullOrWhiteSpace(Director))
            {
                DirectorError = "Not empty!";
                _canAccept[2] = false;
            }
            else
            {
                DirectorError = "";
                _canAccept[2] = true;
            }
        }

        private void ValidateLength()
        {
            if (string.IsNullOrWhiteSpace(Length))
            {
                LengthError = "Not empty!";
                _canAccept[3] = false;
            }
            else if (!Length.All(char.IsDigit))
            {
                LengthError = "Invalid!";
                _canAccept[3] = false;
            }
            else if (!int.TryParse(Length, out int lengthvalue))
            {
                LengthError = "Invalid!";
                _canAccept[3] = false;
            }
            else
            {
                LengthError = "";
                _canAccept[3] = true;
            }
        }

        private void ValidateLanguage()
        {
            if (string.IsNullOrWhiteSpace(Language))
            {
                LanguageError = "Not empty!";
                _canAccept[4] = false;
            }
            else if (Language.Length > 20)
            {
                LanguageError = "Not too long";
                _canAccept[4] = false;
            }
            else
            {
                LanguageError = "";
                _canAccept[4] = true;
            }
        }

        private void ValidateCountry()
        {
            if (string.IsNullOrWhiteSpace(Country))
            {
                CountryError = "Not empty!";
                _canAccept[5] = false;
            }
            else
            {
                CountryError = "";
                _canAccept[5] = true;
            }
        }

        private void ValidateTrailer()
        {
            if (string.IsNullOrWhiteSpace(Trailer))
            {
                TrailerError = "Not empty!";
                _canAccept[6] = false;
            }
            else
            {
                TrailerError = "";
                _canAccept[6] = true;
            }
        }
        private void ValidateReleaseYear()
        {
            if (string.IsNullOrWhiteSpace(ReleaseYear))
            {
                ReleaseYearError = "Not empty!";
                _canAccept[7] = false;
            }
            else if (!ReleaseYear.All(char.IsDigit))
            {
                ReleaseYearError = "Invalid!";
                _canAccept[7] = false;
            }
            else if (int.Parse(ReleaseYear) < 0)
            {
                ReleaseYearError = "Invalid!";
                _canAccept[7] = false;
            }
            else
            {
                ReleaseYearError = "";
                _canAccept[7] = true;
            }
        }
        private void ValidateImportPrice()
        {
            if (string.IsNullOrWhiteSpace(ImportPrice))
            {
                ImportPriceError = "Not empty!";
                _canAccept[8] = false;
            }
            else if (!ImportPrice.All(char.IsDigit))
            {
                ImportPriceError = "Invalid!";
                _canAccept[8] = false;
            }
            else if (!int.TryParse(ImportPrice, out int importPriceValue))
            {
                ImportPriceError = "Invalid!";
                _canAccept[8] = false;
            }
            else
            {
                ImportPriceError = "";
                _canAccept[8] = true;
            }
        }
    }
}
