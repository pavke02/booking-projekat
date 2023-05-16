using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for Guest2TourRequestAllDisplays.xaml
    /// </summary>
    public partial class Guest2TourRequestAllDisplays : Window
    {
        public User LoggedUser { get; set; }
        private readonly TourRequestService _tourRequestService;
        private readonly TourRequest _tourRequest;
        public ObservableCollection<TourRequest> TourRequests { get; set; }



        public Guest2TourRequestAllDisplays(User loggedUser, TourRequestService tourRequestService)
        {
            InitializeComponent();
            DataContext = this;

            _tourRequestService = tourRequestService;
             
            Accepted.Text = _tourRequestService.GetProcentAccepted().ToString();
            Invalid.Text = _tourRequestService.GetProcentInvalid().ToString();
            OnHold.Text = _tourRequestService.GetProcentOnHold().ToString();


            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());


        }

        private void AddTourRequest(object sender, RoutedEventArgs e)
        {
            Guest2TourRequest request =
                new Guest2TourRequest(LoggedUser,_tourRequestService);

            request.Show();

        }



    }
}
