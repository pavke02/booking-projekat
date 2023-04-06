using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest1OwnerReviewView.xaml
    /// </summary>
    public partial class Guest1OwnerReviewView : Window
    {
        private OwnerReviewService _ownerReviewService;
        private ReservationService _reservationService;
        private Reservation _reservation;
        public String ImageURLs;

        #region Property
        private int tidiness = 0;
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    if (value > 5)
                        tidiness = 5;
                    else if (value < 1)
                        tidiness = 1;
                    else
                        tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ownerFairness = 0;
        public int OwnerFairness
        {
            get => ownerFairness;
            set
            {
                if (value != ownerFairness)
                {
                    if (value > 5)
                        ownerFairness = 5;
                    else if (value < 1)
                        ownerFairness = 1;
                    else
                        ownerFairness = value;
                    OnPropertyChanged();
                }
            }
        }

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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guest1OwnerReviewView(OwnerReviewService ownerReviewService, ReservationService reservationService, Reservation reservation)
        {
            InitializeComponent();
            DataContext = this;

            _reservation = reservation;

            _ownerReviewService = ownerReviewService;
            _reservationService = reservationService;
        }

        private void TextBoxCheck(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            submitButton.IsEnabled = false;
            if (IsValid)
                submitButton.IsEnabled = true;
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {   
            List<string> imageURLs = new List<string>();
            string[] values = imageTb.Text.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);
            _ownerReviewService.SubmitReview(Tidiness, OwnerFairness, Comment, _reservation, imageURLs);
            _reservationService.Update(_reservation);
            Close();
        }

        #region Validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required";
                }
                return null;
            }
        }

        private readonly string[] validatedProperties = { "Comment" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        #endregion

        private void ImageTbCheck(object sender, TextChangedEventArgs e)
        {
            addURLButton.Visibility = Visibility.Hidden;
            if (!string.IsNullOrEmpty(urlTb.Text) && !string.IsNullOrWhiteSpace(urlTb.Text) && Uri.IsWellFormedUriString(urlTb.Text, UriKind.Absolute))
            {
                addURLButton.Visibility = Visibility.Visible;
            }
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (imageTb.Text == "")
                imageTb.Text = urlTb.Text;
            else
                imageTb.Text += "\n" +urlTb.Text;

            urlTb.Clear();
        }

        private void ClearURLs(object sender, RoutedEventArgs e)
        {
            imageTb.Clear();
            ImageURLs = "";
        }
    }
}
