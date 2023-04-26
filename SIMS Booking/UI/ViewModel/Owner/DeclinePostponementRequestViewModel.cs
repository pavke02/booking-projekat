using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class DeclinePostponementRequestViewModel : ViewModelBase
    {
        private readonly PostponementService _postponementService;

        public ICommand DeclinePostponementRequestCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public Postponement SelectedRequest { get; set; }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public DeclinePostponementRequestViewModel(ModalNavigationStore modalNavigationStore, PostponementService postponementService, Postponement selectedRequest)
        {
            _postponementService = postponementService;
            SelectedRequest = selectedRequest;

            DeclinePostponementRequestCommand = new DeclinePostponementRequestCommand(CreateCloseModalNavigationService(modalNavigationStore), this, _postponementService);
            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
