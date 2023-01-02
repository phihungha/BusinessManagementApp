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
        IObservable<ContractType> GetContractType(string id);

        [Post("/")]
        IObservable<ContractType> SaveContractType([Body] ContractType contractType);

        [Patch("/{id}")]
        IObservable<ContractType> UpdateContractType(string id, [Body] ContractType contractType);

        [Delete("/{id}")]
        IObservable<object> DeleteContractType(string id);
    }
}
