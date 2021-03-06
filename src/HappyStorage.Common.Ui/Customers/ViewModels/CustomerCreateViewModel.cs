﻿using HappyStorage.Common.Ui.Customers.Models;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;

namespace HappyStorage.Common.Ui.Customers.ViewModels
{
    public class CustomerCreateViewModel : ICustomerCreateViewModel
    {
        private readonly IFacade facade;

        public CustomerCreateViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public NewCustomerModel NewCustomer { get; set; } = new NewCustomerModel();

        public void Create()
        {
            Create(this.NewCustomer);
        }

        public void Create(NewCustomerModel newCustomer)
        {
            //map
            var customer = new NewCustomer()
            {
                CustomerNumber = Guid.NewGuid().ToString().Split('-')[0],
                FullName = $"{newCustomer.FirstName} {newCustomer.LastName}",
                Address = $"{newCustomer.Address}"
            };

            //create
            facade.AddNewCustomer(customer);
        }
    }
}