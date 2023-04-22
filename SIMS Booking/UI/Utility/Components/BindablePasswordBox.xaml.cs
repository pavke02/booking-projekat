using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking.Utility.Components
{
    public partial class BindablePasswordBox : UserControl
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), 
                new PropertyMetadata(string.Empty));

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
        }
    }
}
