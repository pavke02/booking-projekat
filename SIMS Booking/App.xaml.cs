using System.Windows;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Stores;
using SplashScreen = SIMS_Booking.UI.View.Startup.SplashScreen;

namespace SIMS_Booking
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            ModalNavigationStore modalNavigationStore = new ModalNavigationStore();

            navigationStore.CurrentViewModel = new SignInViewModel(navigationStore, modalNavigationStore);

            SplashScreen splashScreen = new SplashScreen(navigationStore, modalNavigationStore, this);
            splashScreen.Show();

            // MainWindow = new MainWindow()
            // {
            //     DataContext = new MainViewModel(navigationStore, modalNavigationStore)
            // };
            // MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
