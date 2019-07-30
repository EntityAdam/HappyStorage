using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public class CustomerDeleteViewModel : ICustomerDeleteViewModel
    {
        private readonly IFacade facade;

        public CustomerDeleteViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public string CustomerNumber { get; set; }

        public void Delete()
        {
            facade.DeleteCustomer(CustomerNumber);
        }

        public void Delete(string customerNumber)
        {
            facade.DeleteCustomer(customerNumber);
        }
    }
}