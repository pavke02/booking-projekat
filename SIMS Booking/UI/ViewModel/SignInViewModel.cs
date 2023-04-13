using System.Windows.Input;
using SIMS_Booking.Utility.Commands;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        public ICommand NavigateToSignUpCommand { get; }
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
        public SignInViewModel(NavigationStore navigationStore)
        {
            NavigateToSignUpCommand = new NavigateCommand<SignUpViewModel>(navigationStore, () => new SignUpViewModel(navigationStore));
        }
    }
}
