using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IContractRepository
    {
        IObservable<List<Contract>> GetContracts();

        IObservable<Contract> GetContract(int id);

        IObservable<Contract> SaveContract(Contract Contract);

        IObservable<Contract> UpdateContract(int ContractId, Contract request);

        IObservable<Object> DeleteContract(int id);
    }
}
