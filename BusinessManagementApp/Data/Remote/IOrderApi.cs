using BusinessManagementApp.Data.Model;
using Refit;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IOrderApi
    {

        //Bill
        IObservable<List<Order>> GetBills();
        
        IObservable<Order> GetBill(int id);
        
        IObservable<Order> SaveBill(Order bill);
        
        IObservable<Order> UpdateBill(int billId, Order request);
        
        IObservable<object> DeleteBill(int id);
        //BillDetail

    }
}
