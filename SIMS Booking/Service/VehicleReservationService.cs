﻿using SIMS_Booking.Model.Relations;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;

namespace SIMS_Booking.Service
{
    public class VehicleReservationService
    {

        private readonly CrudService<ReservationOfVehicle> _crudService;

        public VehicleReservationService()
        {
            _crudService = new CrudService<ReservationOfVehicle>("../../../Resources/Data/vehiclereservation.csv");
        }


        #region Crud

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public void Save(ReservationOfVehicle reservation)
        {
            _crudService.Save(reservation);
        }

        public void Update(ReservationOfVehicle reservation)
        {
            _crudService.Update(reservation);
        }

        public List<ReservationOfVehicle> GetAll()
        {
            return _crudService.GetAll();
        }

        public ReservationOfVehicle GetById(int id)
        {
            return _crudService.GetById(id);
        }

        #endregion


    }
}
