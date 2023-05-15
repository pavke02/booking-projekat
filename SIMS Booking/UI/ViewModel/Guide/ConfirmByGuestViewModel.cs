using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class ConfirmByGuestViewModel: ViewModelBase,IObserver
    {
        private ConfirmTourService _confirmTourService;
        private Tour _tour;
        public ObservableCollection<User> GuestOnTour { get; set; }
        public ObservableCollection<int> NumberOfGuestsOnTour { get; set; }

        
        
        public User SelectedUser { get; set; }

        public ConfirmByGuestViewModel(ConfirmTourService confirmTourService, Tour tour)
        {
            _confirmTourService = confirmTourService;
            _tour = tour;
            _confirmTourService.Subscribe(this);

            GuestOnTour = new ObservableCollection<User>(_confirmTourService.GetGuestOnTour(tour));
        }

        private void UpdateConfirmGuests(List<User> users)
        {
            GuestOnTour.Clear();
            foreach (var user in users)
                GuestOnTour.Add(user);
        }

        public void Update()
        {
            UpdateConfirmGuests(_confirmTourService.GetGuestOnTour(_tour));
        }

        [RelayCommand]
        private void ComeOnTour()
        {
            if (SelectedUser != null)
            {
                ConfirmTour temp = new ConfirmTour();
                foreach (ConfirmTour confirmTour in _confirmTourService.GetAll())
                {
                    Trace.WriteLine(SelectedUser.GetId());
                    if (confirmTour.IdTour == _tour.GetId() && SelectedUser.GetId() == confirmTour.UserId)
                        temp = confirmTour;

                }

                temp.IdCheckpoint = _tour.CurrentTourPoint;
                _confirmTourService.Update(temp);
            }
        }
    }
}
