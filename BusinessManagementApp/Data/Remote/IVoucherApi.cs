using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IVoucherApi
    {

        //VoucherType
        [Get("/")]
        IObservable<List<VoucherType>> GetVoucherTypes();

        [Get("/{id}")]
        IObservable<VoucherType> GetVoucherType(int id);

        [Post("/")]
        IObservable<VoucherType> SaveVoucherType(VoucherType VoucherType);

        [Patch("/{id}")]
        IObservable<VoucherType> UpdateVoucherType(int VoucherTypeId, VoucherType request);

        [Delete("/{id}")]
        IObservable<object> DeleteVoucherType(int id);

        //Voucher
        [Get("/")]
        IObservable<List<Voucher>> GetVouchers();

        [Get("/{id}")]
        IObservable<Voucher> GetVoucher(int id);

        [Post("/")]
        IObservable<Voucher> SaveVoucher(Voucher Voucher);

        [Patch("/{id}")]
        IObservable<Voucher> UpdateVoucher(int VoucherId, Voucher request);

        [Delete("/{id}")]
        IObservable<object> DeleteVoucher(int id);

    }
}
