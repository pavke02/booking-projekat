using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace SIMS_Booking.View
{
    public partial class SignInForm : Window
    {

        private readonly UserRepository _userRepository;
        private readonly AccomodationRepository _accomodationRepository;
        private readonly CityCountryRepository _cityCountryRepository;

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
            _accomodationRepository = new AccomodationRepository();
            _cityCountryRepository = new CityCountryRepository();
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
                            OwnerView ownerView = new OwnerView(_accomodationRepository, _cityCountryRepository);
                            ownerView.Show();
                            break;
                        default:
                            MessageBox.Show("Guest1");
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
