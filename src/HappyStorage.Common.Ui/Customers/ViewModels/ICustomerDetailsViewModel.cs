using HappyStorage.Common.Ui.Customers.Models;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public interface ICustomerDetailsViewModel
    {
        NewCustomerModel NewCustomer { get; set; }

        void GetCustomer(string customerNumber);

        void Update(NewCustomerModel newCustomer);
    }
}