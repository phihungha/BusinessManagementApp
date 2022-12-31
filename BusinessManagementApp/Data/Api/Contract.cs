using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public partial interface IApiClient
    {
        [Get("/{employeeId}")]
        IObservable<List<Contract>> GetContracts(string employeeId);

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
