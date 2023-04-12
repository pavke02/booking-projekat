using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class VoucherService
    {

        private readonly CrudService<Voucher> _crudService;

        public VoucherService()
        {
            _crudService = new CrudService<Voucher>("../../../Resources/Data/vouchers.csv");
        }

        #region Crud

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public void Save(Voucher voucher)
        {
            _crudService.Save(voucher);
        }

        public void Update(Voucher voucher)
        {
            _crudService.Update(voucher);
        }

        public List<Voucher> GetAll()
        {
            return _crudService.GetAll();
        }

        public List<Voucher> GetAllValidVouchers()
        {
            return _crudService.GetAll().Where(w => !w.Used).ToList();
        }

        public  void UseVoucher(Voucher voucher)
        {
            voucher.Used = true;
            Update(voucher);
        }


        public Voucher GetById(int id)
        {
            return _crudService.GetById(id);
        }

        #endregion


    }
}
