using BusinessManagementApp.ViewModels.Utils;
using System;

namespace BusinessManagementApp.ViewModels.Filters
{
    public class TimeRangeFilter<T> : AbstractFilter<T>
    {
        public delegate DateTime? GetTime(T value);

        public GetTime GetTimeFunc { get; set; }

        public bool StartTimeFilterEnabled = false;

        public DateTime StartTime { get; set; } = DateTime.Now.Date.AddDays(-7);

        public bool EndTimeFilterEnabled = false;

        public DateTime EndTime { get; set; } = DateTime.Now.Date;

        public TimeRangeFilter(GetTime getTimeFunc, AbstractFilter<T>? filter = null)
        : base(filter)
        {
            IsEnabled = true;
            GetTimeFunc = getTimeFunc;
        }

        protected override bool PerformFiltering(T input)
        {
            DateTime? time = GetTimeFunc(input);

            if (time == null)
            {
                return false;
            }

            bool startTimeFilterPassed = false;
            if (!StartTimeFilterEnabled || time >= StartTime)
            {
                startTimeFilterPassed = true;
            }

            bool endTimeFilterPassed = false;
            if (!EndTimeFilterEnabled || time <= EndTime)
            {
                endTimeFilterPassed = true;
            }

            return startTimeFilterPassed && endTimeFilterPassed;
        }
    }
}