using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HappyStorage.Core;

namespace HappyStorage.UnitTests
{
    internal class CustomerStoreMock : ICustomerStore
    {
		internal class Customer
		{
			internal string CustomerNumber { get; set; }
			internal string FullName { get; set; }
			internal string Address { get; set; }
		}

		internal readonly List<Customer> Customers = new List<Customer>();

		private IMapper mapper = new MapperConfiguration(cfg =>
		{
			cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
			cfg.CreateMap<NewCustomer, Customer>();
		}).CreateMapper();

		public void Create(NewCustomer newCustomer) => Customers.Add(mapper.Map<NewCustomer, Customer>(newCustomer));

		public bool CustomerExists(string customerNumber) => Customers.Any(c => c.CustomerNumber == customerNumber);

		public void Delete(string customerNumber) => Customers.RemoveAll(u => u.CustomerNumber == customerNumber);

        public IEnumerable<CustomerLookup> ListCustomers() => Customers.Select(c => new CustomerLookup() { CustomerNumber = c.CustomerNumber, FullName = c.FullName });
    }
}
