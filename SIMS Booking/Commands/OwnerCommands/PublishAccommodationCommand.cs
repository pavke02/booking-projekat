using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SIMS_Booking.Commands.OwnerCommands
{
    class PublishAccommodationCommand : CommandBase
    {
        private readonly IPublish _viewModel;
        private readonly AccommodationService _accommodationService;
        private readonly UsersAccommodationService _usersAccommodationService;
        private readonly User _user;

        public PublishAccommodationCommand(IPublish ownerMainViewModel, AccommodationService accommodationService, UsersAccommodationService usersAccommodationService, User user)
        {
            _viewModel = ownerMainViewModel;
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;
            _user = user;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.AccommodationName) && !string.IsNullOrEmpty(_viewModel.MaxGuests) && int.TryParse(_viewModel.MaxGuests, out _) &&
                !string.IsNullOrEmpty(_viewModel.MinReservationDays) && int.TryParse(_viewModel.MinReservationDays, out _) &&
                !string.IsNullOrEmpty(_viewModel.CancellationPeriod) && int.TryParse(_viewModel.CancellationPeriod, out _) &&
                !string.IsNullOrEmpty(_viewModel.ImageURLs) && !string.IsNullOrEmpty(_viewModel.City) && !string.IsNullOrEmpty(_viewModel.Country.ToString()) &&
                !string.IsNullOrEmpty(_viewModel.AccommodationType) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Location location = new Location(_viewModel.Country, _viewModel.City);

            List<string> imageURLs = new List<string>();
            string[] values = _viewModel.ImageURLs.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);

            Accommodation accommodation = new Accommodation(_viewModel.AccommodationName, location, (AccommodationType)Enum.Parse(typeof(AccommodationType),
                _viewModel.AccommodationType), _user, int.Parse(_viewModel.MaxGuests), int.Parse(_viewModel.MinReservationDays), int.Parse(_viewModel.CancellationPeriod), imageURLs, false);
            _accommodationService.Save(accommodation);

            UsersAccommodation usersAccommodation = new UsersAccommodation(_user.GetId(), accommodation.GetId());
            _usersAccommodationService.Save(usersAccommodation);
            MessageBox.Show("Accommodation published successfully");

            Clear();
        }

        public void Clear()
        {
            _viewModel.AccommodationName = "";
            _viewModel.MaxGuests = "";
            _viewModel.MinReservationDays = "";
            _viewModel.CancellationPeriod = "";
            _viewModel.ImageURLs = "";
            _viewModel.AccommodationType = null;
            _viewModel.Country = null;
            _viewModel.City = null;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OwnerMainViewModel.AccommodationName) || e.PropertyName == nameof(OwnerMainViewModel.MaxGuests) ||
                e.PropertyName == nameof(OwnerMainViewModel.MinReservationDays) || e.PropertyName == nameof(OwnerMainViewModel.CancellationPeriod) ||
                e.PropertyName == nameof(OwnerMainViewModel.ImageURLs) || e.PropertyName == nameof(OwnerMainViewModel.City) || e.PropertyName == nameof(OwnerMainViewModel.Country) ||
                e.PropertyName == nameof(OwnerMainViewModel.AccommodationType))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
