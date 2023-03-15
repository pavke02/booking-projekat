using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for StartTour.xaml
    /// </summary>
    public partial class StartTour : Window,  IObserver
    {

        public ObservableCollection<string> Checkpoints { get; set; }
        public Tour SelectedTour { get; set; }
        

        

       
        public StartTour(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            Checkpoints = new ObservableCollection<string>(selectedTour.tourPoints);


        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            /*
                        if(StartTourGrid.SelectedItem != null)
                        {
                            // trenutno stanje -> id++
                            // novo stanje = trenutno stanje
                            //if()
                        }

                    }
            */
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
