using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ConfirmTourRepository gosti { get; set; }
        private ConfirmTourRepository _confirmTourRepository;
        private Tour _tour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StartTour(Tour selectedTour , ConfirmTourRepository confirmTourRepository)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;

            
            _confirmTourRepository = confirmTourRepository;
          
           
          
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
            foreach(ConfirmTour confirmTour in _confirmTourRepository.GetAll())
            {
                if(confirmTour.IdTour == SelectedTour.ID)
                {
                    _confirmTourRepository.Delete(confirmTour);
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
                    
                    ConfirmTourByGuest confirmTourByGuest = new ConfirmTourByGuest(_confirmTourRepository, SelectedTour);
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

