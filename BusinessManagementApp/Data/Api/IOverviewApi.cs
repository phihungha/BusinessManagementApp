using BusinessManagementApp.Data.Model;
using Refit;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IOverviewApi
    {
        [Get("/")]
        IObservable<Overview> GetOverview();
    }
}