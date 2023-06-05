using SIMS_Booking.UI.Utility;
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
