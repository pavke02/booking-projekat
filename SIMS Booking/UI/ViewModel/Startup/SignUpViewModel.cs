using SIMS_Booking.Commands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Enums;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Startup
{
    public class SignUpViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly UserService _userService;

        public ICommand NavigateBackCommand { get; }
        public ICommand SignUpCommand { get; }

        #region Property
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        private Roles _role;
        public Roles Role
        {
            get => _role;
            set
            {
                if (value != _role)
                {
                    _role = value;
                    OnPropertyChanged();
                }

            }
        }

        private bool _errorText;
        public bool ErrorText
        {
            get => _errorText;
            set
            {
                if (value != _errorText)
                {
                    _errorText = value;
                    OnPropertyChanged();
                }

            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (value != _confirmPassword)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }

            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                }

            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }

            }
        }

        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                if (value != _age)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        public SignUpViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, UserService userService)
        {
            _userService = userService;

            SignUpCommand = new SignUpCommand(this, _userService);
            NavigateBackCommand = new NavigateCommand(CreateSignInNavigationService(navigationStore, modalNavigationStore));
        }

        private static INavigationService CreateSignInNavigationService(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            return new NavigationService<SignInViewModel>
                (navigationStore, () => App.GetViewModel<SignInViewModel>());
        }

        #region Validation
        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username))
                            break;
                        else if (Username.Length < 4)
                            result = "Username must be a minimum of 4 characters";
                        else if (_userService.GetByUsername(Username) != null)
                            result = "Username already in use";
                        break;
                    case "Age":
                        if (string.IsNullOrEmpty(Age))
                            break;
                        else if (!uint.TryParse(Age, out _) || uint.Parse(Age) < 1)
                            result = "Age must be number greater than 0";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        #endregion
    }
}
