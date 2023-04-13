using System.Windows;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new SignInViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
