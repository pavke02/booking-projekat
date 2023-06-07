using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class GeneratePDFCommand : CommandBase
    {
        private readonly Guest1MainViewModel _viewModel;

        public GeneratePDFCommand(Guest1MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Arial", 20);
            XFont font2 = new XFont("Arial", 26);

            string heading = "Reservation Report ID " + _viewModel.SelectedReservation.GetId();
            string dateOfCreation = DateTime.Today.ToShortDateString();

            string text1 = "Accommodation: " + _viewModel.SelectedReservation.Accommodation.Name;
            string text2 = "Owner: " + _viewModel.SelectedReservation.Accommodation.User.Username;
            string text3 = "Guest: " + _viewModel.SelectedReservation.User.Username;
            string text4 = "Start date: " + _viewModel.SelectedReservation.StartDate.ToShortDateString();
            string text5 = "End date: " + _viewModel.SelectedReservation.EndDate.ToShortDateString();

            // Draw text on the page
            gfx.DrawString(heading, font2, XBrushes.Black, new XRect(0, 10, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString(dateOfCreation, font, XBrushes.Black, new XRect(-20, 10, page.Width, page.Height), XStringFormats.TopRight);

            gfx.DrawString(text1, font, XBrushes.Black, new XRect(10, 60, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text2, font, XBrushes.Black, new XRect(10, 80, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text3, font, XBrushes.Black, new XRect(10, 100, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text4, font, XBrushes.Black, new XRect(10, 120, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text5, font, XBrushes.Black, new XRect(10, 140, page.Width, page.Height), XStringFormats.TopLeft);

            // Save the document to a file
            string filePath = "C:\\Users\\gagim\\Documents\\GitHub\\sims-projekat\\SIMS Booking\\Resources\\PDFs\\Reservation" + _viewModel.SelectedReservation.GetId() + ".pdf";
            document.Save(filePath);

            // Close the document
            document.Close();

            // Show a message box with the file path
            MessageBox.Show($"PDF created successfully at {filePath}");

            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
        }
    }
}
