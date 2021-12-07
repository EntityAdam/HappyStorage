using HappyStorage.Common.Ui.Customers.Models;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public class CustomerListViewModel : BindableBase, ICustomerListViewModel
    {
        private const int defaultPageSize = 4;

        private readonly IFacade facade;

        private Pager<CustomerLookup> Pager { get; set; }

        public CustomerListViewModel(IFacade facade)
        {
            this.facade = facade;
            Customers.ListChanged += Customers_ListChanged;

            NextPageCommand = new DelegateCommand(
                () => Next(),
                () => (Pager != null) ? Pager.CanExecuteNext : false
            );
            PrevPageCommand = new DelegateCommand(
                () => Prev(),
                () => (Pager != null) ? Pager.CanExecutePrev : false
            );
        }

        public BindingList<CustomerLookupModel> Customers { get; set; } = new BindingList<CustomerLookupModel>();

        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PrevPageCommand { get; set; }

        public int CurrentPage => (Pager != null) ? Pager.CurrentPage : 0;

        //Suggested by LoisHendricks. Felt cute, might delete later.
        public bool HasPrevPage => PrevPageCommand.CanExecute();

        public bool HasNextPage => NextPageCommand.CanExecute();

        private void Customers_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Customers));
        }

        public void Load()
        {
            var customers = facade.ListCustomersAndTenants();
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
                    FullName = c.FullName,
                    UnitsReservedCount = c.UnitsReservedCount ?? default,
                });
            }
        }

        public void JumpToPage(int? pageNum)
        {
            if (pageNum != null)
            {
                UpdateList(Pager.TryJumpToPage((int)pageNum));
            }
            else
            {
                UpdateList(Pager.FirstPage());
            }
        }
    }
}