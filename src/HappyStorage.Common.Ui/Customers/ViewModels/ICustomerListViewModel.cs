using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerListViewModel
    {

        DelegateCommand NextCommand { get; }
        DelegateCommand PrevCommand { get; }
        IList<CustomerLookupModel> Customers { get; set; }
        void Load();
    }
}