using Microsoft.TeamFoundation.Test.WebApi;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SIMS_Booking.Commands.DriverCommands
{
    public class DriverGeneratePDF: CommandBase
    {
        private readonly FinishedRidesService _finishedService;

        public DriverGeneratePDF(FinishedRidesService finishedRidesService)
        {
            _finishedService = finishedRidesService;
        }

        public override void Execute(object? parameter)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);



            // Create a font
            XFont font = new XFont("Arial", 16);
            XFont font2 = new XFont("Arial", 20);
            string filePath = "C:\\Users\\gagim\\Documents\\GitHub\\sims-projekat\\SIMS Booking\\Resources\\PDFs\\" + "driver.pdf";

            int y = 0;

            string rideStats = "Date: " + DateTime.Now.ToString();
            gfx.DrawString(rideStats, font2, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 40;
            rideStats = "PDF REPORT";
            gfx.DrawString(rideStats, font2, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 120;

            foreach (FinishedRide finishedRide in _finishedService.GetAll())
            {
                rideStats = "Street: " + finishedRide.Ride.Street + "   " + "DateTime: " + finishedRide.Time.ToString() + "   " + "Type: " + finishedRide.Ride.Type;
                gfx.DrawString(rideStats, font, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
                y += 40;
            }

            document.Save(filePath);

            MessageBox.Show("Generating PDF...");

            //gfx.DrawString(accommodationName, font, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.Center);

            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
        }
    }
}
