﻿using SIMS_Booking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Service
{
    public class FinishedRidesService
    {
        private readonly CrudService<FinishedRide> _crudService;

        public FinishedRidesService()
        {
            _crudService = new CrudService<FinishedRide>("../../../Resources/Data/finishedRides.csv");
        }

        #region Crud 

        public void Save(FinishedRide finishedRide)
        {
            _crudService.Save(finishedRide);
        }

        public List<FinishedRide> GetAll()
        {
            return _crudService.GetAll();
        }

        public FinishedRide GetById(int id)
        {
            return _crudService.GetById(id);
        }

        #endregion



    }
}