using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System.Windows.Media;
using System.Windows;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

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

            string text1 = "Accommodation: " + _viewModel.SelectedReservation.Accommodation.Name;
            string text2 = "Owner: " + _viewModel.SelectedReservation.Accommodation.User.Username;
            string text3 = "Guest: " + _viewModel.SelectedReservation.User.Username;
            string text4 = "Start date: " + _viewModel.SelectedReservation.StartDate.ToShortDateString();
            string text5 = "End date: " + _viewModel.SelectedReservation.EndDate.ToShortDateString();

            // Draw text on the page
            gfx.DrawString(text1, font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text2, font, XBrushes.Black, new XRect(10, 30, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text3, font, XBrushes.Black, new XRect(10, 50, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text4, font, XBrushes.Black, new XRect(10, 70, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(text5, font, XBrushes.Black, new XRect(10, 90, page.Width, page.Height), XStringFormats.TopLeft);

            // Save the document to a file
            string filePath = "../../../Resources/PDFs/ReservationReportID" + _viewModel.SelectedReservation.GetId() + ".pdf";
            document.Save(filePath);

            // Close the document
            document.Close();

            // Show a message box with the file path
            MessageBox.Show($"PDF created successfully at {filePath}");
        }
    }
}
