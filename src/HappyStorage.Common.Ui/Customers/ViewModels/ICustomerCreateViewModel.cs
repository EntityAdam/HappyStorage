using HappyStorage.Common.Ui.Customers.Models;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public interface ICustomerCreateViewModel
    {
        NewCustomerModel NewCustomer { get; set; }

        void Create();

        void Create(NewCustomerModel newCustomer);
    }
}