using AutoMapper;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.UnitTests
{
    internal class CustomerStoreMock : ICustomerStore
    {
        internal record Customer(string CustomerNumber, string FullName, string Address);

        internal readonly List<Customer> Customers = new();

        private readonly IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.CreateMap<NewCustomer, Customer>();
        }).CreateMapper();

        public void Create(NewCustomer newCustomer) => Customers.Add(mapper.Map<NewCustomer, Customer>(newCustomer));

        public bool CustomerExists(string customerNumber) => Customers.Any(c => c.CustomerNumber == customerNumber);

        public void Delete(string customerNumber) => Customers.RemoveAll(u => u.CustomerNumber == customerNumber);

        public IEnumerable<CustomerLookup> ListCustomers() => Customers.Select(c => new CustomerLookup(c.CustomerNumber, c.FullName, 0));

        public NewCustomer GetCustomer(string customerNumber)
        {
            var customer = Customers.Single(x => x.CustomerNumber == customerNumber);
            return new NewCustomer(customer.CustomerNumber, customer.FullName, customer.Address);
        }

        public void UpdateCustomer(NewCustomer newCustomerDetails)
        {
            var customer = Customers.Single(x => x.CustomerNumber == newCustomerDetails.CustomerNumber);
            var update = customer with { FullName = newCustomerDetails.FullName, Address = newCustomerDetails.Address };
            Customers.Remove(customer);
            Customers.Add(update);
        }
    }
}