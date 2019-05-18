using HappyStorage.Core;
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
            Pager = new Pager<CustomerLookup>(customers, 10);
            var page = Pager.Next();
            foreach (var c in page)
            {
                Customers.Add(new CustomerLookupModel()
                {
                    CustomerNumber = c.CustomerNumber,
                    FullName = c.FullName
                });
            }
        }

        public void Next()
        {
            var customers = new BindingList<CustomerLookupModel>();
            var page = Pager.Next();
            foreach (var c in page)
            {
                customers.Add(new CustomerLookupModel()
                {
                    CustomerNumber = c.CustomerNumber,
                    FullName = c.FullName
                });
            }
            Customers = customers;
        }

        public void Prev()
        {
            var customers = new BindingList<CustomerLookupModel>();
            var page = Pager.Prev();
            foreach (var c in page)
            {
                customers.Add(new CustomerLookupModel()
                {
                    CustomerNumber = c.CustomerNumber,
                    FullName = c.FullName
                });
            }
            Customers = customers;
        }

        public bool CanExecuteNext() => (Pager != null) ? Pager.CanExecuteNext : false;

        public bool CanExecuteBack() => (Pager != null) ? Pager.CanExecuteBack : false;
    }
}