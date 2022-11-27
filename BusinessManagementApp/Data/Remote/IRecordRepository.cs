using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IRecordRepository
    {

        //Salary Record
        IObservable<List<SalaryRecord>> GetSalaryRecords();

        IObservable<SalaryRecord> GetSalaryRecord(int id);

        IObservable<SalaryRecord> SaveSalaryRecord(SalaryRecord SalaryRecord);

        IObservable<SalaryRecord> UpdateSalaryRecord(int SalaryRecordId, SalaryRecord request);

        IObservable<Object> DeleteSalaryRecord(int id);

        //Overtime Record
        IObservable<List<OvertimeRecord>> GetOvertimeRecords();

        IObservable<OvertimeRecord> GetOvertimeRecord(int id);

        IObservable<OvertimeRecord> SaveOvertimeRecord(OvertimeRecord OvertimeRecord);

        IObservable<OvertimeRecord> UpdateOvertimeRecord(int OvertimeRecordId, OvertimeRecord request);

        IObservable<Object> DeleteOvertimeRecord(int id);

    }
}
