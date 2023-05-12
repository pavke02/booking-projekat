using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public class GuideReviewViewModel : ViewModelBase
    {

        private GuideReviewService _guideReviewService;
        private ReservedTourService _reservedTourService;
        private TourReservation _tourReservation;
        public string ImageURLs;

        #region Property
        private int tourRating = 0;
        public int TourRating
        {
            get => tourRating;
            set
            {
                if (value != tourRating)
                {
                    if (value > 5)
                        tourRating = 5;
                    else if (value < 1)
                        tourRating = 1;
                    else
                        tourRating = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public GuideReviewViewModel(GuideReviewService guideReviewService, ReservedTourService reservedTourService, TourReservation tourReservation)
        {

            _tourReservation = tourReservation;
            _guideReviewService = guideReviewService;
            _reservedTourService = reservedTourService;

        }


        public void SubmitReview(string imageTb)
        {
            List<string> imageURLs = new List<string>();
            string[] values = imageTb.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);
            _guideReviewService.SubmitReview(TourRating, _tourReservation, imageURLs);
            _reservedTourService.Update(_tourReservation);

        }

        public bool ImageTbCheck(string urlTb)
        {

            if (!string.IsNullOrEmpty(urlTb) && !string.IsNullOrWhiteSpace(urlTb) && Uri.IsWellFormedUriString(urlTb, UriKind.Absolute))
            {
                return true;
            }
            return false;
        }




        public void ClearURLs()
        {

            ImageURLs = "";
        }

    }
}
