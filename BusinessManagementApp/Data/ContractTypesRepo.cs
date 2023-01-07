using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class ContractTypesRepo
    {
        private IContractTypesApi api;

        public ContractTypesRepo(IContractTypesApi api)
        {
            this.api = api;
        }

        public IObservable<List<ContractType>> GetContractTypes()
        {
            return api.GetContractTypes();
        }

        public IObservable<ContractType> GetContractType(int id)
        {
            return api.GetContractType(id);
        }

        public IObservable<ContractType> AddContractType(ContractType contractType)
        {
            return api.SaveContractType(contractType);
        }

        public IObservable<ContractType> UpdateContractType(int contractTypeId, ContractType contractType)
        {
            return api.UpdateContractType(contractTypeId, contractType);
        }

        public IObservable<object> DeleteContractType(int contractTypeId)
        {
            return api.DeleteContractType(contractTypeId);
        }
    }
}