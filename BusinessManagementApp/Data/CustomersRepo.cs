using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class CustomersRepo
    {
        private ICustomersApi api;

        public CustomersRepo(ICustomersApi api)
        {
            this.api = api;
        }

        public IObservable<List<Customer>> GetCustomers()
        {
            return api.GetCustomers();
        }

        public IObservable<Customer> GetCustomer(string id)
        {
            return api.GetCustomer(id);
        }

        public IObservable<Customer> AddCustomer(Customer customer)
        {
            return api.SaveCustomer(customer);
        }

        public IObservable<Customer> UpdateCustomer(string customerId, Customer customer)
        {
            return api.UpdateCustomer(customerId, customer);
        }

        public IObservable<object> DeleteCustomer(string id)
        {
            return api.DeleteCustomer(id);
        }
    }
}