using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System.Collections.Generic;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class GeneratePdfCommand : CommandBase
    {
        private readonly OwnerReviewService _ownerReviewService;
        private readonly User _user;

        public GeneratePdfCommand(OwnerReviewService ownerReviewService, User user)
        {
            _ownerReviewService = ownerReviewService;
            _user = user;
        }

        public override void Execute(object? parameter)
        {
            Dictionary<Accommodation, AccommodationRatings> accommodationsRatings = GenerateRatingForAccommodations();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Arial", 20);

            int y = 0;
            foreach (KeyValuePair<Accommodation, AccommodationRatings> accommodationsRating in accommodationsRatings)
            {
                string accommodationName = accommodationsRating.Key.Name  
                                           + "     Tidiness: " +
                                           accommodationsRating.Value.GetTidinessRating() 
                                           + "     OwnersCorrectness: " +
                                           accommodationsRating.Value.GetOwnersCorrectnessRating();

                gfx.DrawString(accommodationName, font, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.Center);
                y += 40;
            }

            document.Save("../../../Resources/PDFs/owner.pdf");
        }

        private Dictionary<Accommodation, AccommodationRatings> GenerateRatingForAccommodations()
        {
            Dictionary<Accommodation, AccommodationRatings> accommodationsRatings = new Dictionary<Accommodation, AccommodationRatings>();
            foreach (OwnerReview ownerReview in _ownerReviewService.GetByOwnerId(_user.GetId()))
            {
                if(ownerReview.Tidiness == 0) continue;

                if (accommodationsRatings.ContainsKey(ownerReview.Reservation.Accommodation))
                    accommodationsRatings[ownerReview.Reservation.Accommodation].AddReviewStats(ownerReview.Tidiness, ownerReview.OwnersCorrectness);
                else 
                    accommodationsRatings.Add(ownerReview.Reservation.Accommodation, new AccommodationRatings(ownerReview.Tidiness, ownerReview.OwnersCorrectness));
            }

            return accommodationsRatings;
        }
    }

    internal class AccommodationRatings
    {
        public int TidinessSum { get; set; }
        public int OwnersCorrectnessSum { get; set; }
        public int ReviewsCount { get; set; }

        public AccommodationRatings(int tidinessSum, int ownersCorrectnessSum)
        {
            TidinessSum = tidinessSum;
            OwnersCorrectnessSum = ownersCorrectnessSum;
            ReviewsCount = 1;
        }

        public void AddReviewStats(int tidiness, int ownersCorrectness)
        {
            TidinessSum += tidiness;
            OwnersCorrectnessSum += ownersCorrectness;
            ReviewsCount++;
        }

        public double GetTidinessRating()
        {
            return (double)TidinessSum/ReviewsCount;
        }

        public double GetOwnersCorrectnessRating()
        {
            return (double)OwnersCorrectnessSum / ReviewsCount;
        }
    }
}
