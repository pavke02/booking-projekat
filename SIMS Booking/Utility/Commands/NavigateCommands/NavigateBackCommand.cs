using SIMS_Booking.Service.NavigationService;
using System;

namespace SIMS_Booking.Utility.Commands.NavigateCommands
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
