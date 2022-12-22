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
    }
}