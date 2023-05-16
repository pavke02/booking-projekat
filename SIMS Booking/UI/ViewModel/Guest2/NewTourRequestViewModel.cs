using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;
using System;
using System.Windows;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public  class NewTourRequestViewModel : ViewModelBase
    {
        public TourRequestService _tourRequestService;
        public User LoggedUser { get; set; }

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

        public NewTourRequestViewModel(User loggedUser, TourRequestService tourRequestService) 
        {
            LoggedUser = loggedUser;
            _tourRequestService = tourRequestService;
        }

        public bool TourRequest_Click(string city, string country, string description, string language, string numberOfGuests, string timeOfStart, string timeOfEnd)
        {
            int NumberOfGuests = int.Parse(numberOfGuests);
            DateTime TimeOfStart = DateTime.Parse(timeOfStart);
            DateTime TimeOfEnd = DateTime.Parse(timeOfEnd);

            Location location = new Location(country,city);
           // requests = "OnHold";
            _tourRequestService.Save(new TourRequest(LoggedUser.GetId(),location,description,language,NumberOfGuests,TimeOfStart,TimeOfEnd));
            MessageBox.Show("Uspesno ste poslali zahtev za turu. ");
            return true;
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
