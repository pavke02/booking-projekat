using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Observer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View.Guide
{
    /// <summary>
    /// Interaction logic for StartTour.xaml
    /// </summary>
    public partial class StartTour : UserControl,  IObserver, INotifyPropertyChanged
    {
        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<TourPoint> Checkpoints { get; set; }
        public Tour SelectedTour { get; set; }
        private ConfirmTourService _confirmTourService;
        private Tour _tour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StartTour()
        {
            InitializeComponent();
        }

        public StartTour(Tour selectedTour , ConfirmTourService confirmTourService)
        {
            InitializeComponent();
            SelectedTour = selectedTour;

            _confirmTourService = confirmTourService;                              
            Checkpoints = new ObservableCollection<TourPoint>(SelectedTour.TourPoints);
        }
     
        public void Update()
        {
            throw new NotImplementedException();      
        }

        //private void NextCheckPoint(object sender, RoutedEventArgs e)
        //{
        //    for (int i = 0; i < Checkpoints.Count; i++)
        //    {
        //        if ((Checkpoints[i].CheckedCheckBox) && (i != Checkpoints.Count - 1))
        //        {
        //            ConfirmByGuest cf = new ConfirmByGuest(_confirmTourService,SelectedTour);
        //            cf.Show();

        //            Checkpoints[i].CheckedCheckBox = false;
        //            Checkpoints[i + 1].CheckedCheckBox = true;
        //            SelectedTour.CurrentTourPoint = i + 1;
        //            OnPropertyChanged();

        //            break;
        //        }

        //        if (i == Checkpoints.Count - 1)
        //        {
        //            Window.GetWindow(this).Close();
        //            Checkpoints[0].CheckedCheckBox = true;
        //            SelectedTour.CurrentTourPoint = 0;
        //            for (int j = 1; j < Checkpoints.Count; j++)
        //            {
        //                Checkpoints[j].CheckedCheckBox = false;
        //            }
        //        }
        //    }
        //}

        private void EmergencyStopTour(object sender, RoutedEventArgs e)
        {
            Checkpoints[0].CheckedCheckBox = true;
            SelectedTour.CurrentTourPoint = 0;
            for (int j = 1; j < Checkpoints.Count; j++)
            {
                Checkpoints[j].CheckedCheckBox = false;
            }
            foreach (ConfirmTour confirmTour in _confirmTourService.GetAll().ToList())
            {
                if (confirmTour.IdTour == SelectedTour.GetId())
                {
                    _confirmTourService.Delete(confirmTour);
                }
            }
            Window.GetWindow(this).Close();
        }
    }
}

