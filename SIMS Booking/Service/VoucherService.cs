using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class VoucherService
    {
        private readonly ICRUDRepository<Voucher> _repository;

        public VoucherService(ICRUDRepository<Voucher> repository)
        {
            _repository = repository;
        }

        #region Crud

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void Save(Voucher voucher)
        {
            _repository.Save(voucher);
        }

        public void Update(Voucher voucher)
        {
            _repository.Update(voucher);
        }

        public List<Voucher> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Voucher> GetAllValidVouchers()
        {
            return _repository.GetAll().Where(w => !w.Used).ToList();
        }

        public  void UseVoucher(Voucher voucher)
        {
            voucher.Used = true;
            Update(voucher);
        }


        public Voucher GetById(int id)
        {
            return _repository.GetById(id);
        }

        #endregion
    }
}
