using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.View.Guide;
using SIMS_Booking.UI.ViewModel.Owner;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;
using static System.Net.Mime.MediaTypeNames;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        
        public ICommand NavigateToStatistics { get; }
        public ICommand NavigateCreateTour { get; }
        public ICommand NavigateTodaysTours { get; }
        public ICommand NavigateFutureTours { get; }
        public ICommand NavigateCompletedTours { get; }
        public ICommand NavigateRequestTours { get; }
        public ICommand NavigateProfile { get; }
        public ICommand BlackoutCommand { get; }

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
        private TourRequestComplexService _tourRequestComplexService;
        private TourRequestComplex _tourRequestComplex;




        public MainWindowViewModel(TourService tourService, ConfirmTourService confirmTourService,
            TourPointService tourPointService, TextBox textBox, UserService userService
            , TourReviewService tourReviewService, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,TourRequestService tourRequestService, TourRequestComplexService tourRequestComplexService,TourRequestComplex tourRequestComplex)
        {
            _textBox = textBox;
            _tourReviewService = tourReviewService;
            _tourService = tourService;
            //_tourService.Subscribe(this);
            _tourPointService = tourPointService;
            //_tourPointService.Subscribe(this);
            _userService = userService;
            _confirmTourService = confirmTourService;
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;
            _tourRequestComplexService = tourRequestComplexService;
            _tourRequestComplex = tourRequestComplex;




            NavigateCreateTour = new NavigateCommand(CreateCreateTourModalNavigationService(modalNavigationStore));
            NavigateTodaysTours = new NavigateCommand(CreateTodaysToursnavigationService(navigationStore));
            NavigateFutureTours = new NavigateCommand(CreateFutureToursnavigationService(modalNavigationStore));
            NavigateCompletedTours = new NavigateCommand(CreateCompletedToursnavigationService(modalNavigationStore));
            NavigateRequestTours = new NavigateCommand(CreateRequestToursnavigationService(navigationStore));
            NavigateProfile = new NavigateCommand(CreateProfilesnavigationService(navigationStore));
            BlackoutCommand = new RelayCommand(ExecuteBlackoutCommand);




        }
        private void ExecuteBlackoutCommand()
        {
            _confirmTourService.addVoucherFromGuide();
            Window parentWindow = Window.GetWindow(System.Windows.Application.Current.MainWindow);
            if (parentWindow != null)
            {
                parentWindow.Background = Brushes.Black;
            }
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
            (navigationStore, () => new TourRequestViewModel(_tourRequest,this,_modalNavigationStore,navigationStore,_tourRequestService,_tourService, _createTourViewModel,_tourRequest, _tourRequestComplexService, _tourRequestComplex));
        }

        private INavigationService CreateProfilesnavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<ProfileViewModel>
            (navigationStore, () => new ProfileViewModel( _tourReviewService, _tourService));
        }

        

        [RelayCommand]
        public void GuideCancel()
        {
           
        }
    }
}
