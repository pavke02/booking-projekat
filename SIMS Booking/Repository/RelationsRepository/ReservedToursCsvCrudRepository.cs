using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ReservedToursCsvCrudRepository : RelationsCsvCrudRepository<TourReservation>
    {

        public ReservedToursCsvCrudRepository() : base("../../../Resources/Data/reservedTours.csv") { }

        public List<TourReservation> GetAll()
        {
            return _entityList;
        }

        public int GetNumberOfGuestsForTour(int tourId)
        {
            int numberOfGuests = 0;
            foreach (TourReservation tourReservation in _entityList)
            {
                if (tourReservation.TourId == tourId)
                {
                    numberOfGuests += tourReservation.NumberOfGuests;
                }
            }
            return numberOfGuests;
        }


    }
}
