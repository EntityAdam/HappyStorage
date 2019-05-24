using Prism.Commands;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerListViewModel
    {
        BindingList<CustomerLookupModel> Customers { get; set; }
        DelegateCommand NextPageCommand { get; }
        DelegateCommand PrevPageCommand { get; }
        int CurrentPage { get; }
        bool HasNextPage { get; }
        bool HasPrevPage { get; }
        void Load();
        void JumpToPage(int? page);
    }
}