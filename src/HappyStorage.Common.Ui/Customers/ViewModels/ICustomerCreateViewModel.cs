using System.Windows.Input;

namespace HappyStorage.Common.Ui.Customers
{
    public interface ICustomerCreateViewModel
    {
        NewCustomerModel NewCustomer { get; set; }
        void Create();
        void Create(NewCustomerModel newCustomer);
    }
}