using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Api
{
    public interface ICustomerApi
    {
        [Get("/")]
        IObservable<List<Customer>> GetCustomers();

        [Get("/{id}")]
        IObservable<Customer> GetCustomer(string id);

        [Post("/")]
        IObservable<Customer> SaveCustomer(Customer Customer);

        [Patch("/{id}")]
        IObservable<Customer> UpdateCustomer(string customerId, Customer request);

        [Delete("/{id}")]
        IObservable<object> DeleteCustomer(string id);
    }
}
