using HappyStorage.Core;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers
{
    public class CustomerListViewModel : BindableBase, ICustomerListViewModel
    {
        private const int defaultPageSize = 4;

        private readonly IFacade facade;

        private Pager<CustomerLookup> Pager { get; set; }

        public CustomerListViewModel(IFacade facade)
        {
            Customers.ListChanged += Customers_ListChanged;
            this.facade = facade;
            
            NextPageCommand = new DelegateCommand(
                () => Next(),
                () => (Pager != null) ? Pager.CanExecuteNext : false
            );;
            PrevPageCommand = new DelegateCommand(
                () => Prev(),
                () => (Pager != null) ? Pager.CanExecutePrev : false
            );
        }

        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PrevPageCommand { get; set; }

        public int CurrentPage => (Pager != null) ? Pager.CurrentPage : 0;

        public BindingList<CustomerLookupModel> Customers { get; set; } = new BindingList<CustomerLookupModel>();

        //Suggested by LoisHendricks. Felt cute, might delete later.
        public bool HasPrevPage => PrevPageCommand.CanExecute();
        public bool HasNextPage => NextPageCommand.CanExecute();

        private void Customers_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Customers));
        }

        public void Load()
        {
            var customers = facade.ListCustomers();
            Pager = new Pager<CustomerLookup>(customers, defaultPageSize);
            UpdateList(Pager.FirstPage());
        }

        public void Next()
        {
            UpdateList(Pager.Next());
        }

        public void Prev()
        {
            UpdateList(Pager.Prev());
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

        public void JumpToPage(int? page)
        {
            if (page != null) { 
                UpdateList(Pager.TryJumpToPage((int)page));
            }
            else
            {
                UpdateList(Pager.FirstPage());
            }
        }
    }
}