using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.View
{
    public partial class ConfirmTourByGuest : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<string> GuestOnTour { get; set; }
        public ObservableCollection<int> NumberOfTour { get; set; }
        public ObservableCollection<int> NumberOfCheckpoint { get; set; }

        private ConfirmTourRepository _confirmTourRepository;
        private Tour _tour;



        public ConfirmTourByGuest(ConfirmTourRepository confirmTourRepository, Tour tour )
        {
            InitializeComponent();
            DataContext = this;

            _confirmTourRepository = confirmTourRepository;
            _confirmTourRepository.Subscribe(this);
            _tour = tour;

            GuestOnTour = new ObservableCollection<string>(_confirmTourRepository.GetGuestOnTour(_tour));
           

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

}
       

