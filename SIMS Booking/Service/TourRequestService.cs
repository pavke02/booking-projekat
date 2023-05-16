using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;


namespace SIMS_Booking.Service
{
    public class TourRequestService
    {
        private readonly ICRUDRepository<TourRequest> _repository;

        public TourRequestService(ICRUDRepository<TourRequest> repository)
        {
            _repository = repository;
        }

        public void Save(TourRequest tour)
        {
            _repository.Save(tour);
        }

        public List<TourRequest> GetAll()
        {
            return _repository.GetAll();
        }

        public List<TourRequest> GetAllAccepted()
        {
            return GetAll().Where(e => e.Requests == Requests.Accepted).ToList();
        }

        public double GetProcentAccepted()
        {
            int sve = GetAll().Count();
            int prihvaceni = GetAllAccepted().Count();

            if (sve==0)
            {
                return 0;
            }
            return (prihvaceni / sve) * 100;            
        }

        public List<TourRequest> GetAllInvalid()
        {
            return GetAll().Where(e => e.Requests == Requests.Invalid).ToList();
        }

        public double GetProcentInvalid()
        {
            int sve = GetAll().Count();
            int odbijeni = GetAllInvalid().Count();

            if (sve == 0)
            {
                return 0;
            }
            return (odbijeni / sve)*100;
        }

        public List<TourRequest> GetAllOnHold()
        {
            return GetAll().Where(e => e.Requests == Requests.OnHold).ToList();
        }

        public double GetProcentOnHold()
        {
            int sve = GetAll().Count();
            int na_cekanju = GetAllOnHold().Count();

            if (sve == 0)
            {
                return 0;
            }
            return (na_cekanju / sve)*100;
        }




    }
}
