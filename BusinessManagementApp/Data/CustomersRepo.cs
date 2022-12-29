using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class CustomersRepo
    {
        public IObservable<object> DeleteCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Customer> GetCustomer(string id)
        {
            var customer = new Customer()
            {
                Id = "1",
                Name = "Ha Phi Hung",
                Gender = Gender.Female,               
                Birthday = new DateTime(1975, 5, 5),
                Phone = "0123456789",
                Email = "20520526@gm.uit.edu.vn",
                Address = "178 Nguyen Trai, Binh Duong",               
            };

            return Observable.FromAsync(() => Task.FromResult(customer));
        }

        public IObservable<List<Customer>> GetCustomers()
        {
            var customer = new List<Customer>()
            {
                new Customer()
                {
                    Id = "1",
                    Name = "Ha Phi Hung",
                    Gender = Gender.Female,
                    Birthday = new DateTime(1975, 5, 5),
                    Phone = "0123456789",
                    Email = "20520526@gm.uit.edu.vn",
                    Address = "178 Nguyen Trai, Binh Duong",
                },

                new Customer()
                {
                    Id = "2",
                    Name = "Vo Dang Thuan",
                    Gender = Gender.Male,
                    Birthday = new DateTime(1975, 5, 5),
                    Phone = "0123456789",
                    Email = "20520314@gm.uit.edu.vn",
                    Address = "178 Ly Tu Trong, Binh Duong",
                },

            };
            return Observable.FromAsync(() => Task.FromResult(customer));
        }

        public IObservable<Customer> AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IObservable<Customer> UpdateCustomer(string customerId, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
