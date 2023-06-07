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
        public double AverageGrade { get; set; }
        public bool IsSuperGuide { get; set; }
        public string IsSuperGuideString { get; set; }
        public bool languages;
        public bool Have20Tours { get; set; }

        public ProfileViewModel(TourReviewService tourReviewService,TourService tourService) 
        {
            _tourReviewService = tourReviewService;
            _tourService = tourService;

            Have20Tours = _tourService.Have20ToursO();
            languages = _tourService.IsMoreThan20ToursSingleLanguage();
            AverageGrade = _tourReviewService.AverageGrade();

            if (AverageGrade>=9 && Have20Tours==true) IsSuperGuideString = "SUPER VODIC"; // stavio sam za HAVE20  da bude vece od 1 
            else IsSuperGuideString = "/";
        }
    }
}
