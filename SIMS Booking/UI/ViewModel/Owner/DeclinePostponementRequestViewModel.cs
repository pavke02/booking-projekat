using System.Windows.Input;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class DeclinePostponementRequestViewModel : ViewModelBase
    {
        private readonly PostponementService _postponementService;

        public ICommand DeclinePostponementRequestCommand { get; }

        public Postponement SelectedRequest { get; set; }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public DeclinePostponementRequestViewModel(ModalNavigationStore modalNavigationStore, PostponementService postponementService, Postponement selectedRequest)
        {
            _postponementService = postponementService;
            SelectedRequest = selectedRequest;

            DeclinePostponementRequestCommand = new DeclinePostponementRequestCommand(CreateCloseModalNavigationService(modalNavigationStore), this, _postponementService);
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
