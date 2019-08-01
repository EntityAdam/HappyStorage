using HappyStorage.Common.Ui.Customers.Models;
using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public class CustomerDetailsViewModel : BindableBase, ICustomerDetailsViewModel
    {
        private IFacade facade;

        public NewCustomerModel NewCustomer { get; set; } = new NewCustomerModel();

        public CustomerDetailsViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public void Update()
        {
            var newCustomerDetails = new NewCustomer() 
            { 
                CustomerNumber = this.NewCustomer.CustomerNumber,
                FullName = this.NewCustomer.FullName,
                Address = this.NewCustomer.Address
            }; 
            facade.UpdateCustomerDetails(newCustomerDetails);
        }

        public void GetCustomer(string customerNumber)
        {
            var customer = facade.GetCustomerDetails(customerNumber);

            this.NewCustomer =
                new NewCustomerModel()
                {
                    CustomerNumber = customer.CustomerNumber,
                    FirstName = customer.FullName.Split(',')[0].Split(' ')[0],
                    LastName = customer.FullName.Split(',')[0].Split(' ')[1],
                    Street = customer.Address.Split(',')[0],
                    City = customer.Address.Split(',')[1],
                    State = customer.Address.Split(',')[2].Split(' ')[0],
                    PostalCode = customer.Address.Split(',')[2].Split(' ')[1],
                };
        }
    }
}