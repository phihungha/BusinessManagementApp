using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IVouchersApi
    {
        [Get("/")]
        IObservable<List<Voucher>> GetVouchers();

        [Get("/{id}")]
        IObservable<Voucher> GetVoucher(int id);

        [Post("/")]
        IObservable<Voucher> SaveVoucher([Body] Voucher voucher);

        [Patch("/{id}")]
        IObservable<Voucher> UpdateVoucher(int id, [Body] Voucher voucher);

        [Delete("/{id}")]
        IObservable<object> DeleteVoucher(int id);
    }
}