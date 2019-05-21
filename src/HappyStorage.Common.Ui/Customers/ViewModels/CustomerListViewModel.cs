using HappyStorage.Core;
using Prism.Commands;
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
            //Don't need to listen to events, because we're not updating the DOM
            //Customers.ListChanged += Customers_ListChanged;
            this.facade = facade;
            
            NextCommand = new DelegateCommand(
                () => Next(),
                () => Pager.CanExecuteNext
            );
            PrevCommand = new DelegateCommand(
                () => Prev(),
                () => Pager.CanExecutePrev
            );
        }

        public DelegateCommand NextCommand { get; set; }
        public DelegateCommand PrevCommand { get; set; }

        public IList<CustomerLookupModel> Customers { get; set; } = new List<CustomerLookupModel>();

        private void Customers_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Customers));
        }

        public void Load()
        {
            var customers = facade.ListCustomers();
            Pager = new Pager<CustomerLookup>(customers, defaultPageSize);
            UpdateList(Pager.Next());
            UpdateCommands();
        }

        private void UpdateCommands()
        {
            NextCommand.RaiseCanExecuteChanged();
            PrevCommand.RaiseCanExecuteChanged();
        }

        public void Next()
        {
            UpdateList(Pager.Next());
            UpdateCommands();
        }

        public void Prev()
        {
            UpdateList(Pager.Prev());
            UpdateCommands();
        }

        private void UpdateList(IEnumerable<CustomerLookup> page)
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
    }
}