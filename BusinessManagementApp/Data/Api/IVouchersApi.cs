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
        IObservable<Voucher> GetVoucher(string id);

        [Post("/")]
        IObservable<List<Voucher>> GenerateVouchers([Body] VoucherType voucherType, int numOfVouchers);

        [Delete("/")]
        IObservable<List<Voucher>> DeleteVouchers([Body] IEnumerable<string> voucherIds);
    }
}