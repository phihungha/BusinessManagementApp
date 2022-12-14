using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ContractTypesRepo
    {
        public IObservable<object> DeleteContractType(string id)
        {
            throw new NotImplementedException();
        }
        public IObservable<ContractType> GetContractType(string name)
        {
            var contracttype = new ContractType()
            {
                Id ="1",
                Name = "Permanent",
                BaseSalary = 60,
                Period = 50
            };
            return Observable.FromAsync(() => Task.FromResult(contracttype));
        }
        public IObservable<List<ContractType>> GetContractTypes()
        {
            var contracttypes = new List<ContractType>()
            {
                new ContractType()
                {
                    Id = "1",
                    Name = "1 month",
                    BaseSalary = 60,
                    Period= 50
                },
                new ContractType()
                {
                    Id = "1",
                    Name = "2 month",
                    BaseSalary = 50,
                    Period= 100
                }
            };
            return Observable.FromAsync(() => Task.FromResult(contracttypes));
        }
        public IObservable<ContractType> AddContractType(ContractType contracttype)
        {
            throw new NotImplementedException();
        }

        public IObservable<ContractType> UpdateContractType(string contracttypeId, ContractType contracttype)
        {
            throw new NotImplementedException();
        }
    }
}
