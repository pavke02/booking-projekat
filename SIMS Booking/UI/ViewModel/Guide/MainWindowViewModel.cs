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
        private TourService _tourService;
        private ConfirmTourService _confirmTourService;
        private TourPointService _tourPointService;
        private TextBox _textBox;
        private UserService _userService;
        private TourReview _tourReview;
        private Tour _tour;
        private TourReviewService _tourReviewService;
        private NavigationStore _navigationStore;
        private ModalNavigationStore _modalNavigationStore;
        private TodaysToursViewModel _viewModel;
        private CreateTourViewModel _createTourViewModel;
        private MainWindowViewModel _mainviewModel;




        public MainWindowViewModel(TourService tourService, ConfirmTourService confirmTourService,
            TourPointService tourPointService, TextBox textBox, UserService userService, TourReview tourReview
            , TourReviewService tourReviewService, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,MainWindowViewModel mainViewModel)
        {
            _textBox = textBox;
            _tourReview = tourReview;
            _tourReviewService = tourReviewService;
            _tourService = tourService;
            _tourService.Subscribe(this);
            _tourPointService = tourPointService;
            _tourPointService.Subscribe(this);
            _userService = userService;
            _confirmTourService = confirmTourService;
            _mainviewModel = mainViewModel;
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;



            NavigateCreateTour = new NavigateCommand(CreateCreateTourModalNavigationService(modalNavigationStore));
            NavigateTodaysTours = new NavigateCommand(CreateTodaysToursnavigationService(navigationStore));
            NavigateFutureTours = new NavigateCommand(CreateFutureToursnavigationService(modalNavigationStore));
            NavigateCompletedTours = new NavigateCommand(CreateCompletedToursnavigationService(modalNavigationStore));

        }

        private INavigationService CreateCreateTourModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<CreateTourViewModel>
            (modalNavigationStore, () => new CreateTourViewModel(_tourService,_confirmTourService,_tourPointService,_textBox,_userService,_tourReview,_tour,_tourReviewService,_navigationStore,modalNavigationStore, _mainviewModel));
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



        public void Update()
        {
            
        }
    }
}
