using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IContractApi
    {
        IObservable<List<Contract>> GetContracts();

        IObservable<Contract> GetContract(int id);

        IObservable<Contract> SaveContract(Contract Contract);

        IObservable<Contract> UpdateContract(int ContractId, Contract request);

        IObservable<object> DeleteContract(int id);
    }
}
