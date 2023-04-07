using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Service;

namespace SIMS_Booking.View.Owner
{
    public partial class DeclinePostponementRequestView : Window, INotifyPropertyChanged
    {
        private readonly PostponementService _postponementService;

        public Postponement SelectedRequest { get; set; }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DeclinePostponementRequestView(PostponementService postponementService, Postponement selectedRequest)
        {
            InitializeComponent();
            DataContext = this;

            _postponementService = postponementService;
            SelectedRequest = selectedRequest;
        }

        private void DeclinePostponementRequest(object sender, RoutedEventArgs e)
        {
            PostponementStatus postponementStatus = PostponementStatus.Declined;
            _postponementService.ReviewPostponementRequest(SelectedRequest.getID(), Comment, postponementStatus);
            Close();
        }
    }
}
