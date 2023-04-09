using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace SIMS_Booking.View
{
    public partial class SignUpView : Window, IDataErrorInfo
    {
        private readonly UserService _userService;
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

        public SignUpView(UserService userService)
        {
            InitializeComponent();
            DataContext = this;

            _userService = userService;
        }

        public Roles FindRole()
        {
            if (ownerRb.IsChecked == true)
                return Roles.Owner;
            else if (guest1Rb.IsChecked == true)
                return Roles.Guest1;
            else if (guest2Rb.IsChecked == true)
                return Roles.Guest2;
            else if (driverRb.IsChecked == true)
                return Roles.Driver;
            else
                return Roles.Guide;
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password != txtPassword.Password)
            {
                passwordValidationLb.Visibility = Visibility.Visible;
                txtConfirmPassword.BorderBrush = Brushes.Red;
                txtConfirmPassword.BorderThickness = new Thickness (1);
                return;
            }                

            Roles role = FindRole();
            
            User user = new User(Username, txtPassword.Password, role, false);
            _userService.Save(user);
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void TextBoxCheck(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            signUpButton.IsEnabled = false;
            if (this["Username"] == null)
                signUpButton.IsEnabled = true;
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username)) 
                            result = "Username cannot be empty"; 
                        else if(Username.Length < 4)
                            result = "Username must be a minimum of 4 characters";
                        else if(_userService.GetByUsername(Username) != null)
                            result = "Username already in use";
                        break;                        
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
