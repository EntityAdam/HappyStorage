using System.ComponentModel;

namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerListViewModel
    {
        BindingList<CustomerLookupModel> Customers { get; set; }
        void Load();
        void Next();
        void Prev();
        bool CanExecuteNext();
        bool CanExecuteBack();
    }
}