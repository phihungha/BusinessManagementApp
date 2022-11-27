using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IVoucherRepository
    {

        //VoucherType
        IObservable<List<VoucherType>> GetVoucherTypes();

        IObservable<VoucherType> GetVoucherType(int id);

        IObservable<VoucherType> SaveVoucherType(VoucherType VoucherType);

        IObservable<VoucherType> UpdateVoucherType(int VoucherTypeId, VoucherType request);

        IObservable<Object> DeleteVoucherType(int id);

        //Voucher
        IObservable<List<Voucher>> GetVouchers();

        IObservable<Voucher> GetVoucher(int id);

        IObservable<Voucher> SaveVoucher(Voucher Voucher);

        IObservable<Voucher> UpdateVoucher(int VoucherId, Voucher request);

        IObservable<Object> DeleteVoucher(int id);

    }
}
