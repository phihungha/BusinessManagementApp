using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public partial interface IApiClient
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
