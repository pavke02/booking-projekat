using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System.Windows.Controls;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class GuideStatisticsViewModel : ViewModelBase
    {
        private ConfirmTourService _confirmTourService;
        private ConfirmTour _confirmTour;
        private TourService _tourService;
        private TextBox _textBox;
        private UserService _userService;
        private TextBox textBox { get; set; }
        public String Visitor { get; set; }
        public String VisitorByYear { get; set; }
        public Tour SelectedTour { get; set; }

        private string tourName;
        public string TourtName
        {
            get { return tourName; }
            set
            {
                if (value != tourName)
                {
                    tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int year;

        public int Year
        {
            get => year;
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        private int numberOfVisitors;
        public int NumberOfVisitors
        {
            get => numberOfVisitors;
            set
            {
                if (value != numberOfVisitors)
                {
                    numberOfVisitors = value;
                    OnPropertyChanged();
                }
            }
        }

        public GuideStatisticsViewModel(ConfirmTourService confirmTourService, TourService tourService, TextBox textBox,
            UserService userService, ConfirmTour confirmTour)
        {
            _confirmTourService = confirmTourService;
            _tourService = tourService;
            _textBox = textBox;
            _userService = userService;
            _confirmTour = confirmTour;

            Visitor = confirmTourService.MostVisitedTour(tourService).Name;
        }

        [RelayCommand] // command za xaml
        private void MostVisitedTourForInputYear()
        {
            Visitor = _confirmTourService.MostVisitedTourByYear(_tourService, Year).Name;
        }
    }
}
