using System;
using System.Reactive.Concurrency;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BusinessManagementApp.Utils
{
    public class SchedulerProvider
    {
        private readonly Lazy<SynchronizationContext> ui = new(() => new DispatcherSynchronizationContext(Application.Current.Dispatcher));

        public SynchronizationContext UI()
        {
            return ui.Value;
        }

        public IScheduler IO()
        {
            return ThreadPoolScheduler.Instance;
        }
    }
}
