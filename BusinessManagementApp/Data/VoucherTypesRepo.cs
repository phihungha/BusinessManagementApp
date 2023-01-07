using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class VoucherTypesRepo
    {
        private IVoucherTypesApi api;

        public VoucherTypesRepo(IVoucherTypesApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeleteVoucherType(int id)
        {
            return api.DeleteVoucherType(id);
        }

        public IObservable<VoucherType> GetVoucherType(int id)
        {
            return api.GetVoucherType(id);
        }

        public IObservable<List<VoucherType>> GetVoucherTypes()
        {
            return api.GetVoucherTypes();
        }

        public IObservable<VoucherType> AddVoucherType(VoucherType voucherType)
        {
            return api.SaveVoucherType(voucherType);
        }

        public IObservable<VoucherType> UpdateVoucherType(int voucherTypeId, VoucherType voucherType)
        {
            return api.UpdateVoucherType(voucherTypeId, voucherType);
        }
    }
}