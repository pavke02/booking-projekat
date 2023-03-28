using Microsoft.TeamFoundation.Common;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMS_Booking.View
{
    public partial class SignUpView : Window, IDataErrorInfo
    {
        private readonly UserRepository _userRepository;
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

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

        public SignUpView(UserRepository userRepository)
        {
            InitializeComponent();
            DataContext = this;

            _userRepository = userRepository;
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password != txtPassword.Password || Username.IsNullOrEmpty())
            {
                MessageBox.Show("All the fields must be correctly filled"); 
                return;
            }                

            Roles role;
            if (ownerRb.IsChecked == true)
                role = Roles.Owner;
            else if (guest1Rb.IsChecked == true)
                role = Roles.Guest1;
            else if(guest2Rb.IsChecked == true)
                role = Roles.Guest2;
            else if(driverRb.IsChecked == true)
                role = Roles.Driver;
            else
                role = Roles.Guide;

            User user = new User(Username, txtPassword.Password, role);
            _userRepository.Save(user);
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username)) result = "Username cannot be empty"; break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
    }
}
