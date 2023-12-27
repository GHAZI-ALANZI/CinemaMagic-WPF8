using CinemaMagic.Models.DataAccessLayer;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.ForgotPassword
{
    public class ForgotPasswordViewModel : MainBaseViewModel
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";

        private string notification;
        public string Notification
        {
            get => notification;
            set
            {
                if (notification != value)
                {
                    notification = value;
                    OnPropertyChanged(nameof(Notification));
                }
            }
        }

        private ForgetPasswordView wd;
        public ICommand AcceptCommand { get; set; }
        public ICommand backCommand { get; set; }
        public ForgotPasswordViewModel(ForgetPasswordView wd)
        {
            this.wd = wd;
            AcceptCommand = new ViewModelCommand(accept, canexecuteAccept);
            backCommand = new ViewModelCommand(back);
        }
        private void accept(object obj)
        {
            Task.Run(async () =>
            {
                Notification = "";
                if (!notify())
                {
                    Notification = "The account name or email does not exist!";
                    return;
                }

                if (MotSoPTBoTro.sendMail(Username, Email))
                {
                    Notification = "The password has been sent to the linked email!";
                }
                else
                {
                    Notification = "There was an error while sending to the linked email!";
                }


                await Task.Delay(3000);
                Notification = "";
            });
        }

        private bool canexecuteAccept(object obj)
        {
            if (Username == "" || Email == "")
            {
                return false;
            }
            return true;
        }

        private void back(object obj)
        {
            wd.Close();
        }


        private bool notify()
        {
            UserDA userDA = new UserDA();
            List<Tuple<string, string>> result = userDA.selectUserAndMail();
            foreach (var item in result)
            {
                if (item.Item1 == Username && item.Item2 == Email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
