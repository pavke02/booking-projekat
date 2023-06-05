using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public interface IPublish : INotifyPropertyChanged
    {
        public bool ButtCancel {get; set; }
        public string AccommodationName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AccommodationType { get; set; }
        public string MaxGuests { get; set; }
        public string MinReservationDays { get; set; }
        public string CancellationPeriod { get; set; }
        public string Url { get; set; }
        public string ImageURLs { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
