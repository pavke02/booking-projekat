using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public class GroupRideViewModel : ViewModelBase
    {
        public User LoggedUser { get; set; }
        public GroupRideService _groupRideService;

        #region Property
        private bool _errorText;
        public bool ErrorText
        {
            get => _errorText;
            set
            {
                if (value != _errorText)
                {
                    _errorText = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tidiness = 0;
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    if (value > 5)
                        tidiness = 5;
                    else if (value < 1)
                        tidiness = 1;
                    else
                        tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ruleFollowing = 0;
        public int RuleFollowing
        {
            get => ruleFollowing;
            set
            {
                if (value != ruleFollowing)
                {
                    if (value > 5)
                        ruleFollowing = 5;
                    else if (value < 1)
                        ruleFollowing = 1;
                    else
                        ruleFollowing = value;
                    OnPropertyChanged();
                }
            }
        }

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

        #endregion

        public GroupRideViewModel(User _loggedUser, GroupRideService groupRideService) {
        
            LoggedUser = _loggedUser;

            _groupRideService = groupRideService;
        }

        public bool Button_Click(int numberOfPassengers,string street1, string city1, string country1,string street2, string city2, string country2, string timeOfDeparture, string language)
        {
            Address startingAddress = new Address(street1, new Location(country1,city1));
            Address endingAddress = new Address(street2, new Location(country2, city2));
            _groupRideService.Save(new GroupRide(LoggedUser.GetId(),numberOfPassengers,startingAddress,endingAddress,timeOfDeparture,language));
              
            MessageBox.Show("Uspesno ste poslali zahtev za grupnu voznju. ");
                    
            return true;
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        #region Validation
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
        #endregion

    }
}
