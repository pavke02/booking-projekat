using SIMS_Booking.Model.Relations;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class VehicleReservationService
    {

        private readonly ICRUDRepository<ReservationOfVehicle> _repository;

        public VehicleReservationService(ICRUDRepository<ReservationOfVehicle> repository)
        {
            _repository = repository;
        }

        #region Crud
        public void Save(ReservationOfVehicle reservation)
        {
            _repository.Save(reservation);
        }

        public List<ReservationOfVehicle> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion
    }
}
