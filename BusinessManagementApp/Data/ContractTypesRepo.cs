using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ContractTypesRepo
    {
        public IObservable<object> DeleteContractType(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<ContractType> GetContractType(int id)
        {
            var contractType = new ContractType()
            {
                Id = 1,
                Name = "Permanent",
                BaseSalary = 60,
                Period = 50
            };
            return Observable.FromAsync(() => Task.FromResult(contractType));
        }

        public IObservable<List<ContractType>> GetContractTypes()
        {
            var contractTypes = new List<ContractType>()
            {
                new ContractType()
                {
                    Id = 1,
                    Name = "1 month",
                    BaseSalary = 60,
                    Period= 50
                },
                new ContractType()
                {
                    Id = 2,
                    Name = "2 month",
                    BaseSalary = 50,
                    Period = 100
                }
            };
            return Observable.FromAsync(() => Task.FromResult(contractTypes));
        }

        public IObservable<ContractType> AddContractType(ContractType contractType)
        {
            throw new NotImplementedException();
        }

        public IObservable<ContractType> UpdateContractType(int contractTypeId, ContractType contractType)
        {
            throw new NotImplementedException();
        }
    }
}