using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessManagementApp.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Add all objects from items to the end of ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observableCollection">Collection to add into</param>
        /// <param name="items">Items to add</param>
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                observableCollection.Add(item);
            }
        }
    }
}