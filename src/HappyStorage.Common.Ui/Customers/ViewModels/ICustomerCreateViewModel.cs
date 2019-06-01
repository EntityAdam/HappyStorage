using System.Windows.Input;

namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerCreateViewModel
    {
        ICommand NextCommand { get; set; }
        ICommand PrevCommand { get; set; }
        NewCustomerModel NewCustomer { get; set; }
        void Create();
        void Create(NewCustomerModel newCustomer);
    }
}