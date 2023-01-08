using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class VouchersRepo
    {
        public IObservable<Voucher> GetVoucher(string Code)
        {
            var voucher = new Voucher()
            {
                Code = "1",
                Type = new VoucherType()
                {
                    Id = 1,
                    Name = "Voucher giang sinh",
                    Description = "giang sinh",
                    DiscountType = DiscountType.Percent,
                    DiscountValue = 20,
                    MinNetPrice = 10000,
                    AppliedProducts = new List<Product>()
                    {
                        new Product()
                        {
                            Id = 1,
                            Name = "INTEL Core i3-10105",
                            Description = "4C/8T, 3.7GHz - 4.4GHz, 6MB",
                            Unit = "",
                            Price = 2_899_000,
                            Stock = 100,
                            Category = new ProductCategory () { Id = 1, Name = "CPU"}
                        },
                        new Product()
                        {
                            Id = 2,
                            Name = "INTEL Core i5-12400",
                            Description = "6C/12T, 2.50 GHz - 4.40 GHz, 18MB",
                            Unit = "",
                            Price = 5_049_000,
                            Stock = 50,
                            Category = new ProductCategory () { Id = 1, Name = "CPU"}
                        }
                    }
                },
                ReleaseDate = new DateTime(2022, 12, 20, 17, 0, 0),
                ExpiryDate = new DateTime(2023, 12, 31, 17, 0, 0)
            };
            return Observable.FromAsync(() => Task.FromResult(voucher));
        }

        public IObservable<List<Voucher>> GetVouchers()
        {
            var vouchers = new List<Voucher>()
            {
                new Voucher()
                {
                    Code = "1",
                    Type = new VoucherType() { Id = 1, Name = "Christmas discount" },
                    ReleaseDate = new DateTime(2022, 12, 20, 17, 0, 0),
                    ExpiryDate = new DateTime(2022, 12, 31, 17, 0, 0)
                },
                new Voucher()
                {
                    Code = "2",
                    Type = new VoucherType() { Id = 1, Name = "Christmas discount" },
                    ReleaseDate = new DateTime(2022, 12, 20, 17, 0, 0),
                    ExpiryDate = new DateTime(2022, 12, 31, 17, 0, 0)
                },
                new Voucher()
                {
                    Code = "3",
                    Type = new VoucherType() { Id = 2, Name = "New Year discount" },
                    ReleaseDate = new DateTime(2022, 12, 30, 13, 0, 0),
                    ExpiryDate = new DateTime(2023, 1, 5, 13, 0, 0)
                }
            };

            return Observable.FromAsync(() => Task.FromResult(vouchers));
        }

        public IObservable<List<Voucher>> CreateVouchers(Voucher voucher, int number)
        {
            return Observable.FromAsync(() => Task.FromResult(new List<Voucher>()));
        }

        public IObservable<List<Voucher>> DeleteVouchers(IEnumerable<string> id)
        {
            var vouchers = new List<Voucher>()
            {
                new Voucher()
                {
                    Code = "1",
                    Type = new VoucherType() { Id = 1, Name = "Christmas discount" },
                    ReleaseDate = new DateTime(2022, 12, 20, 17, 0, 0),
                    ExpiryDate = new DateTime(2022, 12, 31, 17, 0, 0)
                },
                new Voucher()
                {
                    Code = "2",
                    Type = new VoucherType() { Id = 1, Name = "Christmas discount" },
                    ReleaseDate = new DateTime(2022, 12, 20, 17, 0, 0),
                    ExpiryDate = new DateTime(2022, 12, 31, 17, 0, 0)
                }
            };
            return Observable.FromAsync(() => Task.FromResult(vouchers));
        }
    }
}