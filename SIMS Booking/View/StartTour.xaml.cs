using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for StartTour.xaml
    /// </summary>
    public partial class StartTour : Window,  IObserver, INotifyPropertyChanged
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
        private ConfirmTourCsvCrudRepository gosti { get; set; }
        private ConfirmTourCsvCrudRepository _confirmTourCsvCrudRepository;
        private Tour _tour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StartTour(Tour selectedTour , ConfirmTourCsvCrudRepository confirmTourCsvCrudRepository)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            
            _confirmTourCsvCrudRepository = confirmTourCsvCrudRepository;          
          
            Checkpoints = new ObservableCollection<TourPoint>(SelectedTour.TourPoints);
        }
            
        

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            Checkpoints[0].CheckedCheckBox = true;
            SelectedTour.CurrentTourPoint = 0;
            for (int j = 1; j < Checkpoints.Count; j++)
            {
                Checkpoints[j].CheckedCheckBox = false;
            }
            foreach(ConfirmTour confirmTour in _confirmTourCsvCrudRepository.GetAll())
            {
                if(confirmTour.IdTour == SelectedTour.getID())
                {
                    _confirmTourCsvCrudRepository.Delete(confirmTour);
                }
            }


            Window.GetWindow(this).Close();
        }

              
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            

            for( int i = 0; i < Checkpoints.Count; i++ )
            {
                if ((Checkpoints[i].CheckedCheckBox) && (i != Checkpoints.Count-1))
                {
                    
                    ConfirmTourByGuest confirmTourByGuest = new ConfirmTourByGuest(_confirmTourCsvCrudRepository, SelectedTour);
                    confirmTourByGuest.ShowDialog();
                                                                           

                    Checkpoints[i].CheckedCheckBox = false;
                    Checkpoints[i + 1].CheckedCheckBox = true;
                    SelectedTour.CurrentTourPoint = i + 1;
                    OnPropertyChanged();                                                           
                    
                    break;
                } 

                if(i == Checkpoints.Count-1)
                {
                    
                    Window.GetWindow(this).Close();
                    Checkpoints[0].CheckedCheckBox = true;
                    SelectedTour.CurrentTourPoint = 0;
                    for ( int j = 1; j < Checkpoints.Count; j++ )
                    {
                        Checkpoints[j].CheckedCheckBox = false;
                    }
                }

            }
            

         }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

   
        
      
}

