using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.Commands.NavigateCommands
{
    public class NavigateBackCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigateBackCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
