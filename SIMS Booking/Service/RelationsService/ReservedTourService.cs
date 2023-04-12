using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using System.Collections.Generic;
using System;

namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedTourService
    {
        private readonly RelationsCrudService<TourReservation> _crudService;

        public ReservedTourService()
        {
            _crudService = new RelationsCrudService<TourReservation>("../../../Resources/Data/reservedTours.csv");
        }

        public List<TourReservation> GetAll()
        {
            return _crudService.GetAll();
        }

        public int GetNumberOfGuestsForTour(int tourId)
        {
            int numberOfGuests = 0;
            foreach (TourReservation tourReservation in _crudService.GetAll())
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
            _crudService.Save(tourReservation);
        }

        public void Update(TourReservation tourReservation)
        {
          //  _crudService.Update(tourReservation);
        }

        /*public TourReservation GetById(int id)
        {
            return _crudService.GetById(id);
        }
        */

    }
}
