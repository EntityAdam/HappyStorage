using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;

namespace HappyStorage.UnitTests
{
    internal class CustomerStoreDummy : ICustomerStore
    {
        public void Create(NewCustomer newCustomer) => throw new NotSupportedException();

        public bool CustomerExists(string customerNumber) => throw new NotSupportedException();

        public void Delete(string customerNumber) => throw new NotSupportedException();

        public NewCustomer GetCustomer(string customerNumber) => throw new NotSupportedException();

        public IEnumerable<CustomerLookup> ListCustomers() => throw new NotSupportedException();

        public void UpdateCustomer(NewCustomer newCustomerDetails) => throw new NotSupportedException();
    }
}