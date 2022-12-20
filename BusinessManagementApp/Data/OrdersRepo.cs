using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class OrdersRepo
    {
        public IObservable<object> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Order> GetOrder(int id)
        {
            var order = new Order()
            {
                Id = 1,
                CreationTime = DateTime.Now,
                CompletionTime = DateTime.Now.AddDays(1),
                EmployeeInCharge = new Employee()
                {
                    Id = "1",
                    Name = "Nguyen Van A",
                    Gender = Gender.Male,
                    CitizenId = "123456789000",
                    BirthDate = new DateTime(1975, 5, 5),
                    PhoneNumber = "0123456789",
                    Department = new Department { Id = 1, Name = "Sales" },
                    CurrentPosition = new Position() { Name = "Sales manager" }
                },
                Customer = new Customer()
                {
                    Id = "1",
                    Name = "Ha Phi Hung",
                    Gender = Gender.Female,
                    Birthday = new DateTime(1975, 5, 5),
                    Phone = "0123456789",
                    Email = "20520526@gm.uit.edu.vn",
                    Address = "178 Nguyen Trai, Binh Duong",
                },
                Address = "178 Nguyen Trai, Binh Duong",
                Status = OrderStatus.Completed,
                Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            OrderId = "1",
                            ProductId = "1",
                            UnitPrice = 10000,
                            Quantity = 10,
                        }
                    },
                AppliedVouchers = new List<Voucher>()
                {

                },
                NetPrice = 0,
                VATRate = 0,
                TotalAmount = 10,
                TotalPrice = 100000,
            };

            return Observable.FromAsync(() => Task.FromResult(order));
        }

        public IObservable<List<Order>> GetOrders()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CreationTime = DateTime.Now,
                    CompletionTime = DateTime.Now.AddDays(1),
                    EmployeeInCharge = new Employee()
                    {
                        Id = "1",
                        Name = "Nguyen Van A",
                        Gender = Gender.Male,
                        CitizenId = "123456789000",
                        BirthDate = new DateTime(1975, 5, 5),
                        PhoneNumber = "0123456789",
                        Department = new Department { Id = 1, Name = "Sales" },
                        CurrentPosition = new Position() { Name = "Sales manager" }
                    },
                    Customer = new Customer()
                    {
                        Id = "1",
                        Name = "Ha Phi Hung",
                        Gender = Gender.Female,
                        Birthday = new DateTime(1975, 5, 5),
                        Phone = "0123456789",
                        Email = "20520526@gm.uit.edu.vn",
                        Address = "178 Nguyen Trai, Binh Duong",
                    }, 
                    Address = "178 Nguyen Trai, Binh Duong",
                    Status = OrderStatus.Completed,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            OrderId = "1",
                            ProductId = "1",
                            UnitPrice = 10000,
                            Quantity = 10,
                        }
                    },
                    AppliedVouchers = new List<Voucher>()
                    {

                    },
                    NetPrice = 0,
                    VATRate = 0,
                    TotalAmount = 10,
                    TotalPrice = 100000,
                },

                new Order()
                {
                    Id = 2,
                    CreationTime = DateTime.Now,
                    CompletionTime = DateTime.Now.AddDays(1),
                    EmployeeInCharge = new Employee()
                    {
                        Id = "2",
                        Name = "Nguyen Van",
                        Gender = Gender.Male,
                        CitizenId = "123456789000",
                        BirthDate = new DateTime(1975, 5, 5),
                        PhoneNumber = "0123456789",
                        Department = new Department { Id = 1, Name = "Sales" },
                        CurrentPosition = new Position() { Name = "Sales manager" }
                    },
                    Customer = new Customer()
                    {
                        Id = "2",
                        Name = "Vo Dang Thuan",
                        Gender = Gender.Male,
                        Birthday = new DateTime(1975, 5, 5),
                        Phone = "0123456789",
                        Email = "20520314@gm.uit.edu.vn",
                        Address = "178 Ly Tu Trong, Binh Duong",
                    },
                    Address = "178 Nguyen Trai, Binh Duong",
                    Status = OrderStatus.Pending,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            OrderId = "2",
                            ProductId = "1",
                            UnitPrice = 100000,
                            Quantity = 100,
                        }
                    },
                    AppliedVouchers = new List<Voucher>()
                    {

                    },
                    NetPrice = 0,
                    VATRate = 0,
                    TotalAmount = 100,
                    TotalPrice = 1000000,
                }
            };
            return Observable.FromAsync(() => Task.FromResult(orders));
        }

        public IObservable<Order> AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public IObservable<Order> UpdateOrder(int orderId, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
