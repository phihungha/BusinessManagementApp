using System;
using System.Reactive.Concurrency;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BusinessManagementApp.Utils
{
    public class ScheluderProvider
    {

        private readonly Lazy<SynchronizationContext> _ui = new(() => new DispatcherSynchronizationContext(Application.Current.Dispatcher));

        public SynchronizationContext UI()
        {
            return _ui.Value;
        }

        public IScheduler IO()
        {
            return ThreadPoolScheduler.Instance;
        }

    }
}
