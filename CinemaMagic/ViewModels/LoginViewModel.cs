using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views;
using CinemaMagic.Views.Password;
using System.Windows.Input;

namespace CinemaMagic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _userName;
        //private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private UserDA userDA;


        public string Username
        {
            get => _userName; set { _userName = value; OnPropertyChanged(nameof(Username)); }
        }
        //public SecureString Password
        //{
        //    private get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); }
        //}


        public string ErrorMessage
        {
            get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public bool IsViewVisible
        {
            get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        // -> Commands
        public ICommand LoginCommand { get; }

        public ICommand ForgotPasswordCommand { get; }


        private LoginView wd;
        public LoginViewModel(LoginView wd)
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            //  RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
            ForgotPasswordCommand = new ViewModelCommand(ForgotPassword);
            this.wd = wd;
        }
        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return ValidAccount();
        }

        private void ExecuteLoginCommand(object obj)
        {
            UserDA userDA = new UserDA();
            List<UserDTO> getAccounts = userDA.getAccounts();
            bool check = false;
            foreach (UserDTO user in getAccounts)
            {
                if (user.Username == Username && user.Password == PTChung.EncryptMD5(wd.txtPassword.Password))
                {
                    check = true;
                    MainView mainView = new MainView(user.Staff_Id, wd);
                    wd.Hide();
                    mainView.ShowDialog();

                    break;
                }
            }

            if (!check)
            {
                ErrorMessage = "Username or password is incorrect";
            }

        }




        private bool ValidAccount()
        {
            if (string.IsNullOrWhiteSpace(Username) || wd.txtPassword.Password == "") return false;
            return true;
        }


        private void ForgotPassword(object obj)
        {
            ForgetPasswordView forgetPasswordView = new ForgetPasswordView();
            forgetPasswordView.ShowDialog();
        }
    }
}
