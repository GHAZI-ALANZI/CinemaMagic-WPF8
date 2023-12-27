using CinemaMagic.Models.DataAccessLayer;
using System.IO;
using System.Windows.Input;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace CinemaMagic.ViewModels.InformationManagement
{
    public partial class InformationViewModel
    {
        public ICommand editImageCommand { get; set; }

        private void EditImage()
        {
            editImageCommand = new ViewModelCommand(editImage);
        }



        private void editImage(object obj)
        {
            addImage();
            StaffDA staffDA = new StaffDA();
            staffDA.updateImageStaff(Staff_Id, ImageSource);
            YesMessageBox mb = new YesMessageBox("Notification", "Profile picture changed successfully");
            mb.ShowDialog();
        }

        //add image
        private void addImage()
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
    }
}
