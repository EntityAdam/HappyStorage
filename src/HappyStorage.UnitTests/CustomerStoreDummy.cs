﻿using HappyStorage.Core;
using System;
using System.Collections.Generic;

namespace HappyStorage.UnitTests
{
    internal class CustomerStoreDummy : ICustomerStore
    {
        public void Create(NewCustomer newCustomer) => throw new NotSupportedException();

        public bool CustomerExists(string customerNumber) => throw new NotSupportedException();

        public void Delete(string customerNumber) => throw new NotSupportedException();

        public IEnumerable<CustomerLookup> ListCustomers() => throw new NotSupportedException();
    }
}