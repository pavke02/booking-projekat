using SIMS_Booking.Utility.Commands;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        public ICommand NavigateBackCommand { get; }

        public SignUpViewModel(NavigationStore navigationStore)
        {
            NavigateBackCommand = new NavigateCommand<SignInViewModel>(new NavigationService<SignInViewModel>
                (navigationStore, () => new SignInViewModel(navigationStore)));
        }
    }
}
