using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface ICustomerApi
    {
        IObservable<List<Customer>> GetCustomers();

        IObservable<Customer> GetCustomer(int id);

        IObservable<Customer> SaveCustomer(Customer Customer);

        IObservable<Customer> UpdateCustomer(int CustomerId, Customer request);

        IObservable<object> DeleteCustomer(int id);
    }
}
