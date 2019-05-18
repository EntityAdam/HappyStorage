using HappyStorage.Common.Ui.Customers;
using HappyStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HappyStorage.UnitTests
{
    public class CommonUiTests
    {
        [Fact]
        public void INPC_DEMO()
        {
            //var changes = new List<object>();
            //var customerChanges = new List<(object, string)>();
            //var vm = new CustomerListViewModel(new Facade());


            //vm.PropertyChanged += (s, e) => { changes.Add(e.PropertyName); };
            //vm.Customers.ListChanged += (s, e) => { customerChanges.Add((e.ListChangedType, e.PropertyDescriptor?.DisplayName)); };

            //var customer1 = new CustomerModel() { FirstName = "First", LastName = "Last" };
            //var customer2 = new CustomerModel() { FirstName = "First", LastName = "Last" };

            //vm.Customers.Add(customer1);
            //Assert.Equal(1, changes.Count());

            //vm.Customers.Add(customer2);
            //Assert.Equal(2, changes.Count());

            //customer1.FirstName = "Jack";
            //Assert.Equal(1, customerChanges.Where(x => x.Item2 == "FirstName").Count());
            //Assert.Equal(1, customerChanges.Where(x => x.Item2 == "FullName").Count());

            //customer2.FirstName = "Adam";
            //Assert.Equal(2, customerChanges.Where(x => x.Item2 == "FirstName").Count());
            //Assert.Equal(2, customerChanges.Where(x => x.Item2 == "FullName").Count());
        }
    }
}
