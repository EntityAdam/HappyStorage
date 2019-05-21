using HappyStorage.Core;
using System;
using System.Windows.Input;

namespace HappyStorage.Common.Ui.Customers
{
    public class CustomerCreateViewModel : ICustomerCreateViewModel
    {
        private readonly IFacade facade;

        public CustomerCreateViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public NewCustomerModel NewCustomer { get; set; }
        public ICommand NextCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand PrevCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Create(NewCustomerModel newCustomer)
        {
            //map
            var customer = new NewCustomer()
            {
                CustomerNumber = Guid.NewGuid().ToString().Split('-')[0],
                FullName = $"{newCustomer.FirstName} {newCustomer.LastName}",
                Address = $"{newCustomer.Address}, {newCustomer.City}, {newCustomer.State} {newCustomer.PostalCode}"
            };

            //create
            facade.AddNewCustomer(customer);
        }
    }
}