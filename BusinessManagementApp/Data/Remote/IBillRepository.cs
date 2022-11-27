using BusinessManagementApp.Data.Model;
using Refit;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IBillRepository
    {

        //Bill
        IObservable<List<Bill>> GetBills();
        
        IObservable<Bill> GetBill(int id);
        
        IObservable<Bill> SaveBill(Bill bill);
        
        IObservable<Bill> UpdateBill(int billId, Bill request);
        
        IObservable<Object> DeleteBill(int id);
        //BillDetail

    }
}
