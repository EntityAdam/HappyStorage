using System.Collections.Generic;

using HappyStorage.Core.Models;

namespace HappyStorage.Core
{
    public interface ICustomerStore
    {
        void Create(NewCustomer newCustomer);

        bool CustomerExists(string customerNumber);

        void Delete(string customerNumber);

        IEnumerable<CustomerLookup> ListCustomers();

        NewCustomer GetCustomer(string customerNumber);
        
        void UpdateCustomer(NewCustomer newCustomerDetails);
    }
}