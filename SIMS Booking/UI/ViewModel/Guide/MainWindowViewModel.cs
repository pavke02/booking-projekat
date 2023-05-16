using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.View.Guide;
using SIMS_Booking.UI.ViewModel.Owner;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public class MainWindowViewModel : ViewModelBase, IObserver
    {
        
        public ICommand NavigateToStatistics { get; }
        public ICommand NavigateCreateTour { get; }
        public ICommand NavigateTodaysTours { get; }
        public ICommand NavigateFutureTours { get; }
        public ICommand NavigateCompletedTours { get; }
        public ICommand NavigateRequestTours { get; }

        private TourService _tourService;
        private ConfirmTourService _confirmTourService;
        private TourPointService _tourPointService;
        private TextBox _textBox;
        private UserService _userService;
        private TourReview _tourReview;
        private Tour _tour;
        private TourRequest _tourRequest;
        private TourReviewService _tourReviewService;
        private NavigationStore _navigationStore;
        private ModalNavigationStore _modalNavigationStore;
        private CreateTourViewModel _createTourViewModel;
        private MainWindowViewModel _mainviewModel;
        private TourRequestService _tourRequestService;




        public MainWindowViewModel(TourService tourService, ConfirmTourService confirmTourService,
            TourPointService tourPointService, TextBox textBox, UserService userService
            , TourReviewService tourReviewService, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,TourRequestService tourRequestService)
        {
            _textBox = textBox;
            _tourReviewService = tourReviewService;
            _tourService = tourService;
            _tourService.Subscribe(this);
            _tourPointService = tourPointService;
            _tourPointService.Subscribe(this);
            _userService = userService;
            _confirmTourService = confirmTourService;
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;
            //_tourRequestService.Subscribe(this);




            NavigateCreateTour = new NavigateCommand(CreateCreateTourModalNavigationService(modalNavigationStore));
            NavigateTodaysTours = new NavigateCommand(CreateTodaysToursnavigationService(navigationStore));
            NavigateFutureTours = new NavigateCommand(CreateFutureToursnavigationService(modalNavigationStore));
            NavigateCompletedTours = new NavigateCommand(CreateCompletedToursnavigationService(modalNavigationStore));
            NavigateRequestTours = new NavigateCommand(CreateRequestToursnavigationService(navigationStore));

        }

        private INavigationService CreateCreateTourModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<CreateTourViewModel>
            (modalNavigationStore, () => new CreateTourViewModel(_tourService,_confirmTourService,_tourPointService,_textBox,_userService,_tour,_tourReviewService,_navigationStore,modalNavigationStore, _mainviewModel,_tourRequestService));
        }

        private INavigationService CreateTodaysToursnavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TodaysToursViewModel>
            (navigationStore, () => new TodaysToursViewModel(_tourService,_tour,this,_modalNavigationStore,_navigationStore,_confirmTourService,_createTourViewModel) );
        }

        private INavigationService CreateFutureToursnavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<FutureToursViewModel>
            (modalNavigationStore, () => new FutureToursViewModel(_tourService,this,_modalNavigationStore));
        }

        private INavigationService CreateCompletedToursnavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<CompletedToursViewModel>
            (modalNavigationStore, () => new CompletedToursViewModel(_tourService,this,_modalNavigationStore));
        }

        private INavigationService CreateRequestToursnavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TourRequestViewModel>
            (navigationStore, () => new TourRequestViewModel(_tourRequest,this,_modalNavigationStore,navigationStore,_tourRequestService,_tourService, _createTourViewModel,_tourRequest));
        }



        public void Update()
        {
            
        }
    }
}
