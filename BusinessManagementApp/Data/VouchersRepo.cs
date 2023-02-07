using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class VouchersRepo
    {
        private IVouchersApi api;

        public VouchersRepo(IVouchersApi api)
        {
            this.api = api;
        }

        public IObservable<Voucher> GetVoucher(string code)
        {
            return api.GetVoucher(code);
        }

        public IObservable<List<Voucher>> GetVouchers()
        {
            return api.GetVouchers();
        }

        public IObservable<List<Voucher>> CreateVouchers(Voucher voucher, int number)
        {
            return api.GenerateVouchers(voucher, number);
        }

        public IObservable<List<Voucher>> DeleteVouchers(IEnumerable<string> ids)
        {
            return api.DeleteVouchers(ids);
        }
    }
}