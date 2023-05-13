using System;
using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;


namespace SIMS_Booking.Service
{
    public class TourRequestService
    {
        private readonly CrudService<TourRequest> _crudService;


        public TourRequestService()
        {
            _crudService = new CrudService<TourRequest>("../../../Resources/Data/tourRequests.csv");
        }

        #region Crud 

        public void Save(TourRequest tourRequest)
        {
            _crudService.Save(tourRequest);
        }

        public List<TourRequest> GetAll()
        {
            return _crudService.GetAll();
        }

        public TourRequest GetById(int id)
        {
            return _crudService.GetById(id);
        }

        #endregion





    }
}
