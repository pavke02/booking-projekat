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
            ModalNavigationStore modalNavigationStore = new ModalNavigationStore();

            navigationStore.CurrentViewModel = new SignInViewModel(navigationStore, modalNavigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore, modalNavigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
