using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class VoucherTypesRepo
    {
        public IObservable<object> DeleteVoucherType(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<VoucherType> GetVoucherType(int id)
        {
            var voucherType = new VoucherType()
            {
                Id = 1,
                Name = "Voucher giang sinh",
                Description = "giang sinh",
                DiscountType = DiscountType.Percent,
                DiscountValue = 0.2,
                MinNetPrice = 10000,
                AppliedProducts = new List<Product>() { new Product { Id = 1, Name = "Quan", Price = 10000, Stock = 1 } }
            };
            return Observable.FromAsync(() => Task.FromResult(voucherType));
        }

        public IObservable<List<VoucherType>> GetVoucherTypes()
        {
            var voucherTypes = new List<VoucherType>()
            {
                new VoucherType()
                {
                    Id = 1,
                    Name = "Voucher giang sinh",
                    Description = "giang sinh",
                    DiscountType = DiscountType.Percent,
                    DiscountValue = 0.2,
                    MinNetPrice = 10000,
                    AppliedProducts = new List<Product>() { new Product { Id = 1, Name = "Quan", Price = 10000, Stock = 1 } }
                },

                new VoucherType()
                {
                    Id = 2,
                    Name = "Voucher tet",
                    Description = "tet",
                    DiscountType = DiscountType.Percent,
                    DiscountValue = 0.5,
                    MinNetPrice = 10000,
                    AppliedProducts = new List<Product>() { new Product { Id = 2, Name = "Ao", Price = 50000, Stock = 2 } }
                }
            };
            return Observable.FromAsync(() => Task.FromResult(voucherTypes));
        }

        public IObservable<VoucherType> AddVoucherType(VoucherType voucherType)
        {
            throw new NotImplementedException();
        }

        public IObservable<VoucherType> UpdateVoucherType(int voucherTypeId, VoucherType voucherType)
        {
            throw new NotImplementedException();
        }
    }
}