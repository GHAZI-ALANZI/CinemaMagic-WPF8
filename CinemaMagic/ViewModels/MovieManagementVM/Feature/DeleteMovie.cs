using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.MessageBox;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand DeleteMovieCommand { get; set; }

        private void Delete()
        {
            DeleteMovieCommand = new ViewModelCommand(deleteMovie);
        }
        private void deleteMovie(object obj)
        {
            if (obj != null)
            {
                // Movies currently screening cannot be deleted
                ShowTimeDA showTimeDA = new ShowTimeDA();
                if (showTimeDA.checkShowtimeByMovie((obj as MovieDTO).Id))
                {
                    YesMessageBox mb2 = new YesMessageBox("Error", "The movie is scheduled for at least one screening, you are not allowed to delete it");
                    mb2.ShowDialog();
                    return;
                }

                MovieDA movieDA = new MovieDA();
                // Before deletion, set the foreign key references to it to null
                BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
                billAddMovieDA.updateMovie_IdNull((obj as MovieDTO).Id);

                YesNoMessageBox mb = new YesNoMessageBox("Notification", "Do you want to delete this movie?");
                mb.ShowDialog();
                if (mb.DialogResult == false)
                    return;
                else
                {
                    // Proceed with deleting the movie
                    movieDA.deleteMovie(obj as MovieDTO);
                    mb.Close();
                    YesMessageBox msb = new YesMessageBox("Notification", "Deletion successful");
                    msb.ShowDialog();
                    msb.Close();
                    loadData();
                }
            }

        }



    }
}
