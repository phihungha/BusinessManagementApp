using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class SelectProductsVM : ViewModelBase
    {
        #region Dependencies

        private ProductsRepo productsRepo;

        #endregion Dependencies

        public ObservableCollection<Product> Products { get; } = new();

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private decimal totalPrice = 0;

        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        public SelectProductsVM(ProductsRepo productsRepo) 
        {
            this.productsRepo = productsRepo;

        }
    }
}
