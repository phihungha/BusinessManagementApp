using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IRecordApi
    {

        //Salary Record
        [Get("/")]
        IObservable<List<SalaryRecord>> GetSalaryRecords();

        [Get("/{id}")]
        IObservable<SalaryRecord> GetSalaryRecord(int id);

        [Post("/")]
        IObservable<SalaryRecord> SaveSalaryRecord(SalaryRecord SalaryRecord);

        [Patch("/{id}")]
        IObservable<SalaryRecord> UpdateSalaryRecord(int SalaryRecordId, SalaryRecord request);

        [Delete("/{id}")]
        IObservable<object> DeleteSalaryRecord(int id);

        //Overtime Record
        [Get("/")]
        IObservable<List<OvertimeRecord>> GetOvertimeRecords();

        [Get("/{id}")]
        IObservable<OvertimeRecord> GetOvertimeRecord(int id);

        [Post("/")]
        IObservable<OvertimeRecord> SaveOvertimeRecord(OvertimeRecord OvertimeRecord);

        [Patch("/{id}")]
        IObservable<OvertimeRecord> UpdateOvertimeRecord(int OvertimeRecordId, OvertimeRecord request);

        [Delete("/{id}")]
        IObservable<object> DeleteOvertimeRecord(int id);

    }
}
