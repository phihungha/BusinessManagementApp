using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface ISalaryApi
    {
        [Get("/")]
        IObservable<List<SalaryRecord>> GetSalaryRecords();
    }

    // TODO
    public interface IOvertimeApi
    {
        [Get("/")]
        IObservable<List<OvertimeOverview>> GetOvertimeOverviews(string employeeId, int year, int month);

        [Get("/{employeeId}/{year}/{month}")]
        IObservable<List<OvertimeRecord>> GetEmployeeOvertimeRecords(string employeeId, int year, int month);

        [Post("/{employeeId}/{year}/{month}")]
        IObservable<List<OvertimeRecord>> UpdateEmployeeOvertimeRecords(
            string employeeId,
            int year,
            int month,
            [Body] IEnumerable<OvertimeRecord> overtimeRecords);
    }

    // TODO
    public interface IBonusesApi
    {
        [Get("/{year}/{month}")]
        IObservable<List<BonusRecord>> GetBonusRecords(int month, int year);

        [Post("/{year}/{month}")]
        IObservable<List<BonusRecord>> UpdateBonusRecords(
            int month,
            int year,
            [Body] IEnumerable<BonusRecord> bonusRecords);
    }

    // TODO
    public interface IBonusTypesApi
    {
        [Get("/")]
        IObservable<List<BonusType>> GetBonusTypes();

        [Get("/{id}")]
        IObservable<BonusType> GetBonusType(int id);

        [Post("/")]
        IObservable<BonusType> SaveBonusType([Body] BonusType skillType);

        [Patch("/{id}")]
        IObservable<BonusType> UpdateBonusType(int id, [Body] BonusType skillType);

        [Delete("/{id}")]
        IObservable<object> DeleteBonusType(int id);
    }
}
