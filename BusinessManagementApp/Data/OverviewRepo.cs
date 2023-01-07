using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;

namespace BusinessManagementApp.Data
{
    public class OverviewRepo
    {
        private IOverviewApi api;

        public OverviewRepo(IOverviewApi api)
        {
            this.api = api;
        }

        public IObservable<Overview> GetOverview()
        {
            return api.GetOverview();
        }
    }
}