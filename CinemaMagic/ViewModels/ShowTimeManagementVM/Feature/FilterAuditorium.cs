using System.Windows.Input;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        string phong = "";//Viewing in which room?
        public ICommand AuditoriumCommand { get; set; }// All rooms
        public ICommand AuditoriumCommand1 { get; set; }
        public ICommand AuditoriumCommand2 { get; set; }
        public ICommand AuditoriumCommand3 { get; set; }
        public ICommand AuditoriumCommand4 { get; set; }
        public ICommand AuditoriumCommand5 { get; set; }
        public ICommand AuditoriumCommand6 { get; set; }
        public ICommand AuditoriumCommand7 { get; set; }



        private void Auditorium()
        {
            // Initially, it's all
            phong = "All";

            AuditoriumCommand = new ViewModelCommand(auditorium);
            AuditoriumCommand1 = new ViewModelCommand(auditorium1);
            AuditoriumCommand2 = new ViewModelCommand(auditorium2);
            AuditoriumCommand3 = new ViewModelCommand(auditorium3);
            AuditoriumCommand4 = new ViewModelCommand(auditorium4);
            AuditoriumCommand5 = new ViewModelCommand(auditorium5);
            AuditoriumCommand6 = new ViewModelCommand(auditorium6);
            AuditoriumCommand7 = new ViewModelCommand(auditorium7);
        }


        private void auditorium(object obj)//all Theaters
        {
            loadData();
            phong = "All";
        }

        private void auditorium1(object obj)
        {
            loadData("Theater 1");
            phong = "Theater 1";
        }
        private void auditorium2(object obj)
        {
            loadData("Theater 2");
            phong = "Theater 2";
        }
        private void auditorium3(object obj)
        {
            loadData("Theater 3");
            phong = "Theater 3";
        }
        private void auditorium4(object obj)
        {
            loadData("Theater 4");
            phong = "Theater 4";
        }
        private void auditorium5(object obj)
        {
            loadData("Theater 5");
            phong = "Theater 5";
        }
        private void auditorium6(object obj)
        {
            loadData("Theater 6");
            phong = "Theater 6";
        }
        private void auditorium7(object obj)
        {
            loadData("Theater 7");
            phong = "Theater 7";
        }
    }
}
