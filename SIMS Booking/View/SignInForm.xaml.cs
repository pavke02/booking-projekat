using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using System.ComponentModel;
using System.Diagnostics;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Windows;


namespace SIMS_Booking.View
{
    public partial class SignInForm : Window
    {

        private readonly UserRepository _userRepository;

        private readonly AccomodationRepository _accommodationRepository;


        private readonly CityCountryRepository _cityCountryRepository;   
        private readonly ReservationRepository _reservationRepository;

        private readonly ReservedAccommodationRepository _reservedAccommodationRepository;


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;

            _userRepository = new UserRepository();
            _accommodationRepository = new AccomodationRepository();

            _cityCountryRepository = new CityCountryRepository();   
            _reservationRepository = new ReservationRepository();

            _reservedAccommodationRepository = new ReservedAccommodationRepository();

            _reservedAccommodationRepository.LoadAccommodationsAndUsersInReservation(_userRepository, _accommodationRepository, _reservationRepository);
            

        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userRepository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {                    
                    switch(user.Role)
                    {
                        case Roles.Owner:

                            OwnerMainView ownerView = new OwnerMainView(_accommodationRepository, _cityCountryRepository, _reservationRepository);
                            ownerView.Show();
                            break;
                        case Roles.Guest1:
                            Guest1MainView guest1View = new Guest1MainView(_accommodationRepository, _cityCountryRepository, _reservationRepository, _reservedAccommodationRepository ,user);
                            guest1View.Show();
                            break;
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
    }
}
