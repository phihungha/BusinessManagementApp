﻿using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class VouchersRepo
    {
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