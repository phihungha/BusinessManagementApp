using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ContractTypesRepo
    {
        public IObservable<List<ContractType>> GetContractTypes()
        {
            var contractTypes = new List<ContractType>()
            {
                new ContractType()
                {
                    Id = 1,
                    Name = "6 months",
                    BaseSalary = 5_000_000,
                    Period = 183,
                },
                new ContractType()
                {
                    Id = 2,
                    Name = "1 year",
                    BaseSalary = 8_000_000,
                    Period = 365
                },
                new ContractType()
                {
                    Id = 3,
                    Name = "Permanent",
                    BaseSalary = 8_000_000,
                    Period = null
                }
            };

            return Observable.FromAsync(() => Task.FromResult(contractTypes));
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

        public IObservable<ContractType> AddContractType(ContractType contractType)
        {
            throw new NotImplementedException();
        }

        public IObservable<ContractType> UpdateContractType(int contractTypeId, ContractType contractType)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> DeleteContractType(int contractTypeId)
        {
            throw new NotImplementedException();
        }
    }
}