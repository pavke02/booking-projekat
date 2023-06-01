using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public class ProfileViewModel:ViewModelBase
    {
        public TourReviewService _tourReviewService;
        public TourService _tourService;
        public double AverageGrade;
        public bool IsSuperGuide;
        public bool languages;

        public ProfileViewModel(TourReviewService tourReviewService,TourService tourService) 
        {
            _tourReviewService = tourReviewService;
            _tourService = tourService;

            IsSuperGuide = _tourReviewService.IsSuperGuide();
            languages = _tourService.IsMoreThan20ToursSingleLanguage();
            AverageGrade = _tourReviewService.AverageGrade();

        }
    }
}
