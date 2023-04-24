using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for GuideStatistics.xaml
    /// </summary>
    public partial class GuideStatistics : Window, INotifyPropertyChanged, IObserver
    {
        private ConfirmTourService _confirmTourService;
        private ConfirmTour _confirmTour;
        private TourService _tourService;
        private TextBox _textBox;
        private UserService _userService;
        private TextBox textBox {  get; set; }
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

        


        public GuideStatistics(ConfirmTourService confirmTourService, TourService tourService, TextBox textBox,UserService userService,ConfirmTour confirmTour)
        {
            InitializeComponent();
            DataContext = this;
            _confirmTourService = confirmTourService;
            _tourService = tourService;
            _textBox = textBox;
            _userService = userService;
            _confirmTour = confirmTour;


            _confirmTourService.Subscribe(this);

            Visitor = confirmTourService.MostVisitedTour(tourService).Name;
            txtNajposecenijaTura.Text = Visitor;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void MostVisitedTourForInputYear(object sender, RoutedEventArgs e)
        {
            int godina;
            bool uspesnoOcitavanje = int.TryParse(txtGodina.Text, out godina);

            if (!uspesnoOcitavanje)
            {
                txtNajposecenijaTura.Text = "Greška: unesite validnu godinu.";
                return;
            }
            txtNajposecenijaTura.Text = _confirmTourService.MostVisitedTourByYear(_tourService, godina).Name;
        }
    }
}
