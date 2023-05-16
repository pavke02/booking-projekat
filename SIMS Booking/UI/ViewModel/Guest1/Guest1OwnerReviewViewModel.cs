using System.Windows;
using System.Windows.Input;
using Microsoft.TeamFoundation.Common;
using SIMS_Booking.Commands.Guest1Commands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    public class Guest1OwnerReviewViewModel : ViewModelBase
    {

        private OwnerReviewService _ownerReviewService;
        private ReservationService _reservationService;
        public Reservation _reservation;



        #region Property
        private int _tidiness = 0;
        public int Tidiness
        {
            get => _tidiness;
            set
            {
                if (value != _tidiness)
                {
                    if (value > 5)
                        _tidiness = 5;
                    else if (value < 1)
                        _tidiness = 1;
                    else
                        _tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ownerFairness = 0;
        public int OwnerFairness
        {
            get => _ownerFairness;
            set
            {
                if (value != _ownerFairness)
                {
                    if (value > 5)
                        _ownerFairness = 5;
                    else if (value < 1)
                        _ownerFairness = 1;
                    else
                        _ownerFairness = value;
                    OnPropertyChanged();
                }
            }
        }



        private int _renovationLevel = 0;
        public int RenovationLevel
        {
            get => _renovationLevel;
            set
            {
                if (!RenovationEnabled)
                {
                    _renovationLevel = 0;
                }
                if (value != _renovationLevel)
                {
                    if (value > 5)
                        _renovationLevel = 5;
                    else if (value < 1)
                        _renovationLevel = 1;
                    else
                        _renovationLevel = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    if (RenovationEnabled)
                    {
                        SubmitEnabled = !Comment.IsNullOrEmpty() && !RenovationComment.IsNullOrEmpty();
                    }
                    else
                    {
                        SubmitEnabled = !Comment.IsNullOrEmpty();
                    }
                    OnPropertyChanged();
                }
            }
        }

        private string _renovationComment;
        public string RenovationComment
        {
            get => _renovationComment;
            set
            {
                if (value != _renovationComment)
                {
                    _renovationComment = value;
                    if (RenovationEnabled)
                    {
                        SubmitEnabled = !Comment.IsNullOrEmpty() && !RenovationComment.IsNullOrEmpty();
                    }
                    else
                    {
                        SubmitEnabled = !Comment.IsNullOrEmpty();
                    }
                    OnPropertyChanged();
                }
            }
        }

        private string _urlText;
        public string UrlText
        {
            get => _urlText;
            set
            {
                if (value != _urlText)
                {
                    _urlText = value;
                    /*AddUrlVisibility = Visibility.Hidden;
                    if (!string.IsNullOrEmpty(_urlText) && !string.IsNullOrWhiteSpace(_urlText) && Uri.IsWellFormedUriString(_urlText, UriKind.Absolute))
                    {
                        AddUrlVisibility = Visibility.Visible;
                    }*/
                    OnPropertyChanged();
                }
            }
        }

        private string _imageURLs;
        public string ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (value != _imageURLs)
                {
                    _imageURLs = value;
                    AddUrlVisibility = Visibility.Hidden;
                    OnPropertyChanged();
                }
            }
        }

        private bool _renovationEnabled;
        public bool RenovationEnabled
        {
            get => _renovationEnabled;
            set
            {
                if (value != _renovationEnabled)
                {
                    _renovationEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _renovationCommentEnabled;
        public bool RenovationCommentEnabled
        {
            get => _renovationCommentEnabled;
            set
            {
                if (value != _renovationCommentEnabled)
                {
                    _renovationCommentEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _renovationSliderEnabled;
        public bool RenovationSliderEnabled
        {
            get => _renovationSliderEnabled;
            set
            {
                if (value != _renovationSliderEnabled)
                {
                    _renovationSliderEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _submitEnabled;
        public bool SubmitEnabled
        {
            get => _submitEnabled;
            set
            {
                if (value != _submitEnabled)
                {
                    _submitEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _addUrlVisibility;
        public Visibility AddUrlVisibility
        {
            get => _addUrlVisibility;
            set
            {
                if (value != _addUrlVisibility)
                {
                    _addUrlVisibility = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        public ICommand AddImageOwnerReviewCommand { get; }
        public ICommand ClearUrlCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public Guest1OwnerReviewViewModel(ModalNavigationStore modalNavigationStore, OwnerReviewService ownerReviewService, ReservationService reservationService, Reservation reservation)
        {
            _reservation = reservation;

            _ownerReviewService = ownerReviewService;
            _reservationService = reservationService;

            RenovationComment = "";
            RenovationEnabled = false;

            AddImageOwnerReviewCommand = new AddImageOwnerReviewCommand(this);
            ClearUrlCommand = new ClearUrlCommand(this);
            SubmitCommand = new SubmitOwnerReviewCommand(CreateCloseModalNavigationService(modalNavigationStore) ,this, _ownerReviewService, _reservationService);
            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));

        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }


        /*#region Validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required";
                }
                return null;
            }
        }

        private readonly string[] validatedProperties = { "Comment" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        #endregion*/

        /*private void EnableRenovation(object sender, RoutedEventArgs e)
        {
            renovationTb.IsReadOnly = false;
            renovationCommentTb.IsReadOnly = false;
            renovationSl.IsEnabled = true;
            renovationTb.Focusable = true;
            renovationCommentTb.Focusable = true;
            RenovationCommentLabel.Opacity = 1;
            RenovationLevelLabel.Opacity = 1;
            renovationTb.FontSize = 12;
        }

        private void DisableRenovation(object sender, RoutedEventArgs e)
        {
            renovationTb.IsReadOnly = true;
            renovationTb.Text = "";
            renovationCommentTb.IsReadOnly = true;
            renovationTb.Text = "";
            renovationSl.IsEnabled = false;
            RenovationCommentLabel.Opacity = 0.5;
            RenovationLevelLabel.Opacity = 0.5;
            renovationTb.Focusable = false;
            renovationCommentTb.Focusable = false;
        }*/

    }
}
