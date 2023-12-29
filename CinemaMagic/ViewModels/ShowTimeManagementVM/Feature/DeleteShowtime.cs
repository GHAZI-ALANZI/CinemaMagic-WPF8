using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using CinemaMagic.Views.MessageBox;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand DeleteShowtimeCommand { get; set; }

        private void delete()
        {
            DeleteShowtimeCommand = new ViewModelCommand(DeleteShowtime);
        }

        private void DeleteShowtime(object obj)
        {
            if (obj != null)
            {
                YesNoMessageBox yesNoMessageBox = new YesNoMessageBox("Notification", "Do you want to delete this showtime?");
                yesNoMessageBox.ShowDialog();
                if (yesNoMessageBox.DialogResult == false)
                {
                    return;
                }
                else
                {
                    ShowTimeDTO showTimeDTO = (ShowTimeDTO)obj;
                    ShowTimeDA showTimeDA = new ShowTimeDA();
                    if (showTimeDA.checkShowtime(showTimeDTO))
                    {
                        YesMessageBox yesMessage = new YesMessageBox("Notification", "The showtime is currently running!");
                        yesMessage.ShowDialog();
                    }
                    else
                    {
                        showTimeDA.deleteShowtime(showTimeDTO);
                        YesMessageBox yesMessage = new YesMessageBox("Notification", "Showtime deleted successfully");
                        yesMessage.ShowDialog();
                        loadData(phong);
                    }
                }

            }
        }
    }
}
