using HappyStorage.Common.Ui.Customers.Models;
using HappyStorage.Core;
using HappyStorage.Core.Models;

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
            Update(this.NewCustomer);
        }

        public void Update(NewCustomerModel newCustomer)
        {
            var newCustomerDetails = new NewCustomer(newCustomer.CustomerNumber, newCustomer.FullName, newCustomer.Address);
            facade.UpdateCustomerDetails(newCustomerDetails);
        }

        public void GetCustomer(string customerNumber)
        {
            var customer = facade.GetCustomerDetails(customerNumber);

            this.NewCustomer =
                new NewCustomerModel()
                {
                    CustomerNumber = customer.CustomerNumber,
                    FirstName = customer.FullName.Split(',')[0].Split(' ')[0].Trim(),
                    LastName = customer.FullName.Split(',')[0].Split(' ')[1].Trim(),
                    Street = customer.Address.Split(',')[0].Trim(),
                    City = customer.Address.Split(',')[1].Trim(),
                    State = customer.Address.Split(',')[2].Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
                    PostalCode = customer.Address.Split(',')[2].Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries)[1].Trim(),
                };
        }
    }
}