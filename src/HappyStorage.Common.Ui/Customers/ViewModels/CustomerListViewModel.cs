using HappyStorage.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers
{
    public class CustomerListViewModel : BindableBase, ICustomerListViewModel
    {
        private const int defaultPageSize = 10;

        private readonly IFacade facade;

        private Pager<CustomerLookup> Pager { get; set; }
        
        public CustomerListViewModel(IFacade facade)
        {
            Customers.ListChanged += Customers_ListChanged;
            this.facade = facade;
        }

        public BindingList<CustomerLookupModel> Customers { get; set; } = new BindingList<CustomerLookupModel>();

        private void Customers_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Customers));
        }

        public void Load()
        {
            var customers = facade.ListCustomers();
            Pager = new Pager<CustomerLookup>(customers, defaultPageSize);
            Update(Pager.Next());
        }

        public void Next()
        {
            Update(Pager.Next());
        }

        public void Prev()
        {
            Update(Pager.Prev());
        }

        private void Update(IEnumerable<CustomerLookup> page)
        {
            Customers.Clear();
            foreach (var c in page)
            {
                Customers.Add(new CustomerLookupModel()
                {
                    CustomerNumber = c.CustomerNumber,
                    FullName = c.FullName
                });
            }
        }

        public bool CanExecuteNext() => (Pager != null) ? Pager.CanExecuteNext : false;

        public bool CanExecuteBack() => (Pager != null) ? Pager.CanExecuteBack : false;
    }
}