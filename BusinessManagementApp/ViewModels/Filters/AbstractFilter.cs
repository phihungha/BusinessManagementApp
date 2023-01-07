namespace BusinessManagementApp.ViewModels.Utils
{
    public abstract class AbstractFilter<T>
    {
        private AbstractFilter<T>? nextFilter = null;

        public bool IsEnabled = false;

        public AbstractFilter(AbstractFilter<T>? nextFilter = null)
        {
            this.nextFilter = nextFilter;
        }

        private bool FilterNext(T input)
        {
            return nextFilter == null ? true : nextFilter.Filter(input);
        }

        public bool Filter(T input)
        {
            if (!IsEnabled || PerformFiltering(input))
            {
                return FilterNext(input);
            }
            return false;
        }

        protected abstract bool PerformFiltering(T input);
    }
}