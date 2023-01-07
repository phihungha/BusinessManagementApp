using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IContractTypesApi
    {
        [Get("/")]
        IObservable<List<ContractType>> GetContractTypes();

        [Get("/{id}")]
        IObservable<ContractType> GetContractType(int id);

        [Post("/")]
        IObservable<ContractType> SaveContractType([Body] ContractType contractType);

        [Patch("/{id}")]
        IObservable<ContractType> UpdateContractType(int id, [Body] ContractType contractType);

        [Delete("/{id}")]
        IObservable<object> DeleteContractType(int id);
    }
}