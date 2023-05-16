using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public class TourReservationViewModel : ViewModelBase
    {

        public ICommand SubmitReviewCommand { get; }
        public ICommand NavigateBackCommand { get; }



        public TourReservationViewModel()
        {



        }




    }
}
