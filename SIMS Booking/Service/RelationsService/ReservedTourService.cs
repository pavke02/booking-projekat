using SIMS_Booking.Model.Relations;
using System.Collections.Generic;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedTourService
    {
        private readonly ICRUDRepository<TourReservation> _repository;

        public ReservedTourService(ICRUDRepository<TourReservation> repository)
        {
            _repository = repository;
        }

        public List<TourReservation> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetNumberOfGuestsForTour(int tourId)
        {
            int numberOfGuests = 0;
            foreach (TourReservation tourReservation in _repository.GetAll())
            {
                if (tourReservation.TourId == tourId)
                {
                    numberOfGuests += tourReservation.NumberOfGuests;
                }
            }
            return numberOfGuests;
        }

        public void Save(TourReservation tourReservation)
        {
            _repository.Save(tourReservation);
        }

        public void Update(TourReservation tourReservation)
        {
          //  _repository.Update(tourReservation);
        }

        /*public TourReservation GetById(int id)
        {
            return _repository.GetById(id);
        }
        */

    }
}
