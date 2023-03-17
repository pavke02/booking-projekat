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

        public int active;        

        public List<CheckBox> CheckBoxList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StartTour(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;

           Checkpoints = new ObservableCollection<TourPoint>(SelectedTour.tourPoints);
           // Checked = true;

        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {


            //var selectedRow = StartTourGrid.SelectedItem as DataRowView; 

            //int selectedIndex = StartTourGrid.Items.IndexOf(selectedRow); 

            //var selectedCheckbox = selectedRow[0] as CheckBox;

            //selectedCheckbox.IsChecked = false;

            //int nextIndex = selectedIndex + 1;
            //StartTourGrid.SelectedIndex = nextIndex;
            //var nextRow = StartTourGrid.SelectedItem as DataRowView;

            //var nextCheckbox = nextRow[1] as CheckBox;

            //nextCheckbox.IsChecked = true;
            
            
        }

        public void Update()
        {
            throw new NotImplementedException();
        }   
        
       public void SetFirstCheckpoint(object sender, InitializingNewItemEventArgs e)
        {
            
        }
    }
}
