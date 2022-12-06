using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data.Remote;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class LocalModelRemote : IOrderApi, IContractApi, ICustomerApi, IEmployeeApi,
        IPositionApi, IDepartmentApi, IProductApi, IRecordApi, IVoucherApi
    {
        public IObservable<List<Order>> GetBills()
        {
            throw new NotImplementedException();
        }

        public IObservable<Order> GetBill(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Order> SaveBill(Order bill)
        {
            throw new NotImplementedException();
        }

        public IObservable<Order> UpdateBill(int billId, Order request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Contract>> GetContracts()
        {
            throw new NotImplementedException();
        }

        public IObservable<Contract> GetContract(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Contract> SaveContract(Contract Contract)
        {
            throw new NotImplementedException();
        }

        public IObservable<Contract> UpdateContract(int ContractId, Contract request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteContract(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public IObservable<Customer> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Customer> SaveCustomer(Customer Customer)
        {
            throw new NotImplementedException();
        }

        public IObservable<Customer> UpdateCustomer(int CustomerId, Customer request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        private List<Employee> _employees = new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                Name = "Nguyen Van A",
                Gender = "Male",
                BirthDate = new DateTime(1999, 3, 5),
                Position = "IT manager",
                Department = "IT"
            },

            new Employee()
            {
                Id = 2,
                Name = "Nguyen Van B",
                Gender = "Female",
                BirthDate = new DateTime(1986, 12, 5),
                Position = "HR manager",
                Department = "Human resources"
            },

            new Employee()
            {
                Id = 3,
                Name = "Nguyen Van C",
                Gender = "Male",
                BirthDate = new DateTime(1994, 4, 12),
                Position = "Sales",
                Department = "Sales"
            }
        };

        public IObservable<List<Employee>> GetEmployees()
        {
            return Observable.FromAsync(() => Task.FromResult(_employees));
        }

        public IObservable<Employee> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> SaveEmployee(Employee Employee)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> UpdateEmployee(int EmployeeId, Employee request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Position>> GetPositions()
        {
            throw new NotImplementedException();
        }

        public IObservable<Position> GetPosition(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Position> SavePosition(Position Position)
        {
            throw new NotImplementedException();
        }

        public IObservable<Position> UpdatePosition(int PositionId, Position request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeletePosition(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Department>> GetDepartments()
        {
            throw new NotImplementedException();
        }

        public IObservable<Department> GetDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Department> SaveDepartment(Department Department)
        {
            throw new NotImplementedException();
        }

        public IObservable<Department> UpdateDepartment(int DepartmentId, Department request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<ProductCategory>> GetProductCategorys()
        {
            throw new NotImplementedException();
        }

        public IObservable<ProductCategory> GetProductCategory(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<ProductCategory> SaveProductCategory(ProductCategory ProductCategory)
        {
            throw new NotImplementedException();
        }

        public IObservable<ProductCategory> UpdateProductCategory(int ProductCategoryId, ProductCategory request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteProductCategory(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public IObservable<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Product> SaveProduct(Product Product)
        {
            throw new NotImplementedException();
        }

        public IObservable<Product> UpdateProduct(int ProductId, Product request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Provider>> GetProviders()
        {
            throw new NotImplementedException();
        }

        public IObservable<Provider> GetProvider(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Provider> SaveProvider(Provider Provider)
        {
            throw new NotImplementedException();
        }

        public IObservable<Provider> UpdateProvider(int ProviderId, Provider request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteProvider(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<SalaryRecord>> GetSalaryRecords()
        {
            throw new NotImplementedException();
        }

        public IObservable<SalaryRecord> GetSalaryRecord(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<SalaryRecord> SaveSalaryRecord(SalaryRecord SalaryRecord)
        {
            throw new NotImplementedException();
        }

        public IObservable<SalaryRecord> UpdateSalaryRecord(int SalaryRecordId, SalaryRecord request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteSalaryRecord(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<OvertimeRecord>> GetOvertimeRecords()
        {
            throw new NotImplementedException();
        }

        public IObservable<OvertimeRecord> GetOvertimeRecord(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<OvertimeRecord> SaveOvertimeRecord(OvertimeRecord OvertimeRecord)
        {
            throw new NotImplementedException();
        }

        public IObservable<OvertimeRecord> UpdateOvertimeRecord(int OvertimeRecordId, OvertimeRecord request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteOvertimeRecord(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<VoucherType>> GetVoucherTypes()
        {
            throw new NotImplementedException();
        }

        public IObservable<VoucherType> GetVoucherType(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<VoucherType> SaveVoucherType(VoucherType VoucherType)
        {
            throw new NotImplementedException();
        }

        public IObservable<VoucherType> UpdateVoucherType(int VoucherTypeId, VoucherType request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteVoucherType(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<Voucher>> GetVouchers()
        {
            throw new NotImplementedException();
        }

        public IObservable<Voucher> GetVoucher(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Voucher> SaveVoucher(Voucher Voucher)
        {
            throw new NotImplementedException();
        }

        public IObservable<Voucher> UpdateVoucher(int VoucherId, Voucher request)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteVoucher(int id)
        {
            throw new NotImplementedException();
        }
    }
}