using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IContractApi
    {
        [Get("/")]
        IObservable<List<Contract>> GetContracts();

        [Get("/{id}")]
        IObservable<Contract> GetContract(int id);

        [Post("/")]
        IObservable<Contract> SaveContract(Contract Contract);

        [Patch("/{id}")]
        IObservable<Contract> UpdateContract(int ContractId, Contract request);

        [Delete("/{id}")]
        IObservable<object> DeleteContract(int id);
    }
}
