using System.Diagnostics;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class DeclinePostponementRequestView : UserControl
    {
        private Process process;

        public DeclinePostponementRequestView()
        {
            InitializeComponent();
        }

        private void ShowKeyboard(object sender, System.Windows.RoutedEventArgs e)
        {
            process = System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "osk.exe", UseShellExecute = true });
        }

        private void HideKeyboard(object sender, System.Windows.RoutedEventArgs e)
        {
            process.Kill();
        }
    }
}
