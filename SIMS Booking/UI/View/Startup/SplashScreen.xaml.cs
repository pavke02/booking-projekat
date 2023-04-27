using System.ComponentModel;
using System.Threading;
using System.Windows;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.View.Startup
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly App _app;

        public SplashScreen(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, App app)
        {
            InitializeComponent();

            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _app = app;
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(40);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (progressBar.Value == 100)
            {
                _app.MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
                };
                Close();
                _app.MainWindow.Show();
            }
        }
    }
}
