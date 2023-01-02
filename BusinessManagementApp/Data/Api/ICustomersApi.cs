using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface ICustomersApi
    {
        [Get("/")]
        IObservable<List<Customer>> GetCustomers();

        [Get("/{id}")]
        IObservable<Customer> GetCustomer(string id);

        [Post("/")]
        IObservable<Customer> SaveCustomer([Body] Customer customer);

        [Patch("/{id}")]
        IObservable<Customer> UpdateCustomer(string id, [Body] Customer customer);

        [Delete("/{id}")]
        IObservable<object> DeleteCustomer(string id);
    }
}
