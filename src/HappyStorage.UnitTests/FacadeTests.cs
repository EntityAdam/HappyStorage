using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;
using System.Linq;
using Xunit;

namespace HappyStorage.UnitTests
{
    public class FacadeTests
    {
        [Fact]
        public void NullChecks()
        {
            var facade = new Facade(new UnitStoreDummy(), new CustomerStoreDummy(), new TenancyStoreDummy(), new DateServiceDummy());
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.CommissionNewUnit(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.DecommissionUnit(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.AddNewCustomer(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.DeleteCustomer(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.ReserveUnit(null, null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.ReleaseUnit(null, null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.CalculateAmountDue(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                facade.Pay(null, 0);
            });
        }

        [Fact]
        public void ValidationChecks()
        {
            var facade = new Facade(new UnitStoreDummy(), new CustomerStoreDummy(), new TenancyStoreDummy(), new DateServiceDummy());
            Assert.Throws<ArgumentException>(() =>
            {
                facade.DeleteCustomer(String.Empty);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                facade.ReserveUnit(String.Empty, String.Empty);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                facade.ReleaseUnit(String.Empty, String.Empty);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                facade.CalculateAmountDue(String.Empty);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                facade.Pay(String.Empty, 0);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                facade.Pay("Alpha", -99);
            });
        }

        [Fact]
        public void CommissionsAndDecommissionsUnitsCorrectly()
        {
            var unitStoreMock = new UnitStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var facade = new Facade(unitStoreMock, new CustomerStoreDummy(), tenancyStoreMock, new DateServiceDummy());
            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            facade.CommissionNewUnit(new NewUnit("2B", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("3C", 0, 0, 0, true, false, 0));
            facade.CommissionNewUnit(new NewUnit("4D", 0, 0, 0, false, true, 0));
            facade.CommissionNewUnit(new NewUnit("5E", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("6F", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("7G", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("9I", 0, 0, 0, false, false, 0));

            facade.DecommissionUnit("7G");

            facade.CommissionNewUnit(new NewUnit("8H", 0, 0, 0, false, false, 0));

            facade.DecommissionUnit("8H");
            Assert.Equal(7, unitStoreMock.Units.Count);
            var unit = unitStoreMock.Units.Single(u => u.UnitNumber == "1A");
            Assert.Equal(10, unit.Length);
            Assert.Equal(12, unit.Width);
            Assert.Equal(14, unit.Height);
            Assert.True(unit.IsClimateControlled);
            Assert.True(unit.IsVehicleAccessible);
            Assert.Equal(90, unit.PricePerMonth);
            var unitNumbers = unitStoreMock.Units.Select(u => u.UnitNumber).ToArray();
            Assert.Contains("2B", unitNumbers);
            Assert.Contains("3C", unitNumbers);
            Assert.Contains("4D", unitNumbers);
            Assert.Contains("5E", unitNumbers);
            Assert.Contains("6F", unitNumbers);
            Assert.Contains("9I", unitNumbers);
        }

        [Fact]
        public void CommissionedUnitCubicFeetShouldNotExceedIntMaxValue()
        {
            var unitStoreMock = new UnitStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var facade = new Facade(unitStoreMock, new CustomerStoreDummy(), tenancyStoreMock, new DateServiceDummy());

            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));

            facade.CommissionNewUnit(new NewUnit("2B", 0, 0, 0, true, true, 90));

            var failUnit1 = new NewUnit("3C", int.MaxValue / 2, int.MaxValue / 2, 2, false, false, 90);
            Assert.Throws<ArgumentOutOfRangeException>(() => facade.CommissionNewUnit(failUnit1));

        }

        [Fact]
        public void CantDecomissionOccupiedUnits()
        {
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var unitStoreMock = new UnitStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);

            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.AddNewCustomer(new NewCustomer("Bravo", "Bravo Name", "Bravo Address"));
            facade.AddNewCustomer(new NewCustomer("Charlie", "Charlie Name", "Charlie Address"));
            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            facade.CommissionNewUnit(new NewUnit("2B", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("3C", 0, 0, 0, true, false, 0));

            dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);

            facade.ReserveUnit("1A", "Alpha");
            facade.ReserveUnit("2B", "Bravo");
            facade.ReserveUnit("3C", "Charlie");

            Assert.Throws<InvalidOperationException>(() => facade.DecommissionUnit("2B"));
            facade.ReleaseUnit("2B", "Bravo");
            facade.DecommissionUnit("2B");
            Assert.Equal(2, unitStoreMock.Units.Count());

            Assert.Throws<InvalidOperationException>(() => facade.DecommissionUnit("1A"));
            facade.ReleaseUnit("1A", "Alpha");
            facade.DecommissionUnit("1A");
            Assert.Single(unitStoreMock.Units);
        }

        [Fact]
        public void AddsAndDeletesCustomersCorrectly()
        {
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var facade = new Facade(new UnitStoreMock(), customerStoreMock, tenancyStoreMock, new DateServiceDummy());

            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.AddNewCustomer(new NewCustomer("Bravo", "Bravo Name", "Bravo Address"));
            facade.AddNewCustomer(new NewCustomer("Charlie", "Charlie Name", "Charlie Address"));
            facade.AddNewCustomer(new NewCustomer("Delta", "Delta Name", "Delta Address"));
            facade.AddNewCustomer(new NewCustomer("Echo", "Echo Name", "Echo Address"));
            facade.AddNewCustomer(new NewCustomer("Foxtrot", "Foxtrot Name", "Foxtrot Address"));

            Assert.Equal(6, customerStoreMock.Customers.Count);
            var delta = facade.GetCustomerDetails("Delta");
            Assert.Equal("Delta Name", delta.FullName);
            Assert.Equal("Delta Address", delta.Address);
            var customerNumbers = customerStoreMock.Customers.Select(c => c.CustomerNumber).ToArray();
            Assert.Contains("Bravo", customerNumbers);
            Assert.Contains("Delta", customerNumbers);
            Assert.Contains("Echo", customerNumbers);
            Assert.Contains("Foxtrot", customerNumbers);
        }

        [Fact]
        public void CantDeleteCustomersWithReservedUnits()
        {
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var unitStoreMock = new UnitStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);

            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.AddNewCustomer(new NewCustomer("Bravo", "Bravo Name", "Bravo Address"));
            facade.AddNewCustomer(new NewCustomer("Charlie", "Charlie Name", "Charlie Address"));


            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            facade.CommissionNewUnit(new NewUnit("2B", 0, 0, 0, false, false, 0));
            facade.CommissionNewUnit(new NewUnit("3C", 0, 0, 0, true, false, 0));

            dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);

            facade.ReserveUnit("1A", "Alpha");
            facade.ReserveUnit("2B", "Bravo");
            facade.ReserveUnit("3C", "Charlie");

            Assert.Throws<InvalidOperationException>(() => facade.DeleteCustomer("Alpha"));
            facade.ReleaseUnit("1A", "Alpha");
            facade.DeleteCustomer("Alpha");
            Assert.Equal(2, customerStoreMock.Customers.Count());

            Assert.Throws<InvalidOperationException>(() => facade.DeleteCustomer("Bravo"));
            facade.ReleaseUnit("2B", "Bravo");
            facade.DeleteCustomer("Bravo");
            Assert.Single(customerStoreMock.Customers);
        }

        [Fact]
        public void RejectsDuplicateUnits()
        {
            var unitStoreMock = new UnitStoreMock();
            var facade = new Facade(unitStoreMock, new CustomerStoreDummy(), new TenancyStoreDummy(), new DateServiceDummy());
            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            Assert.Throws<InvalidOperationException>(() =>
            {
                facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            });
        }

        [Fact]
        public void RejectsDuplicateCustomers()
        {
            var customerStoreMock = new CustomerStoreMock();
            var facade = new Facade(new UnitStoreDummy(), customerStoreMock, new TenancyStoreDummy(), new DateServiceDummy());
            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            });
        }

        [Fact]
        public void RejectsDuplicateReservations()
        {
            var unitStoreMock = new UnitStoreMock();
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);
            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.ReserveUnit("A1", "Alpha");
            Assert.Throws<InvalidOperationException>(() =>
            {
                facade.ReserveUnit("A1", "Alpha");
            });
        }

        [Fact]
        public void CannotReleaseInvalidReservations()
        {
            var unitStoreMock = new UnitStoreMock();
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);
            facade.CommissionNewUnit(new NewUnit("1A", 10, 12, 14, true, true, 90));
            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                facade.ReleaseUnit("A1", "Alpha");
            });
            facade.ReserveUnit("A1", "Alpha");
            facade.ReleaseUnit("A1", "Alpha");
            Assert.Throws<InvalidOperationException>(() =>
            {
                facade.ReleaseUnit("A1", "Alpha");
            });
        }

        [Fact]
        public void Scenario1()
        {
            var unitStoreMock = new UnitStoreMock();
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);


            facade.CommissionNewUnit(new NewUnit("1A", 5, 5, 8, false, true, 50));
            facade.CommissionNewUnit(new NewUnit("2B", 10, 5, 8, false, true, 60));
            facade.CommissionNewUnit(new NewUnit("3C", 10, 10, 8, false, true, 70));
            facade.CommissionNewUnit(new NewUnit("4D", 4, 4, 4, true, false, 100));
            facade.CommissionNewUnit(new NewUnit("5E", 6, 6, 6, true, false, 110));
            facade.CommissionNewUnit(new NewUnit("6F", 8, 8, 8, true, false, 120));

            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.AddNewCustomer(new NewCustomer("Bravo", "Bravo Name", "Bravo Address"));
            facade.AddNewCustomer(new NewCustomer("Charlie", "Charlie Name", "Charlie Address"));
            facade.AddNewCustomer(new NewCustomer("Delta", "Delta Name", "Delta Address"));

            facade.UpdateCustomerDetails(new NewCustomer("Delta", "Delta Delta", "Address"));

            Assert.Empty(facade.GetCustomerUnits("Alpha"));
            Assert.Empty(facade.GetCustomerUnits("Bravo"));
            Assert.Empty(facade.GetCustomerUnits("Charlie"));

            var details = facade.GetCustomerDetails("Delta");
            Assert.Equal("Delta", details.CustomerNumber);
            Assert.Equal("Delta Delta", details.FullName);
            Assert.Equal("Address", details.Address);

            var find1 = facade.SearchAvailableUnits(null, null, null).ToArray();
            Assert.Equal(6, find1.Length);
            var find2 = facade.SearchAvailableUnits(true, null, 70).ToArray();
            Assert.Equal(2, find2.Length);
            var find3 = facade.SearchAvailableUnits(null, true, 750).ToArray();
            Assert.Single(find3);
            var unit = find3.Single();
            Assert.Equal("3C", unit.UnitNumber);
            dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);
            facade.ReserveUnit("3C", "Charlie");

            var charliesUnits = facade.GetCustomerUnits("Charlie");
            Assert.Single(charliesUnits);
            Assert.True(charliesUnits.Single().UnitNumber == "3C");
            Assert.True(charliesUnits.Single().ReservationDate == new DateTime(2017, 01, 01));
            Assert.True(charliesUnits.Single().AmountPaid == 0);

            dateServiceMock.CurrentDateTime = new DateTime(2017, 03, 01);
            Assert.Equal(140, facade.CalculateAmountDue("Charlie"));
            dateServiceMock.CurrentDateTime = new DateTime(2017, 04, 01);
            Assert.Equal(210, facade.CalculateAmountDue("Charlie"));
            facade.Pay("Charlie", 180);
            Assert.Equal(30, facade.CalculateAmountDue("Charlie"));
            dateServiceMock.CurrentDateTime = new DateTime(2017, 05, 01);
            Assert.Equal(100, facade.CalculateAmountDue("Charlie"));
            facade.Pay("Charlie", 100);
            Assert.Equal(0, facade.CalculateAmountDue("Charlie"));
            dateServiceMock.CurrentDateTime = new DateTime(2017, 06, 01);
            Assert.Equal(70, facade.CalculateAmountDue("Charlie"));
            var find4 = facade.SearchAvailableUnits(null, true, 750).ToArray();
            Assert.Empty(find4);
            facade.Pay("Charlie", 70);
            facade.ReleaseUnit("3C", "Charlie");
            dateServiceMock.CurrentDateTime = new DateTime(2017, 07, 01);
            Assert.Equal(0, facade.CalculateAmountDue("Charlie"));
            var find5 = facade.SearchAvailableUnits(null, true, 750).ToArray();
            Assert.Single(find5);
            var find6 = facade.SearchAvailableUnits(null, null, null).ToArray();
            Assert.Equal(6, find6.Length);
            var customerLookup = facade.ListCustomersAndTenants();
            Assert.Equal(4, customerLookup.Count());
        }

        [Fact]
        public void Scenario2()
        {
            var unitStoreMock = new UnitStoreMock();
            var customerStoreMock = new CustomerStoreMock();
            var tenancyStoreMock = new TenancyStoreMock();
            var dateServiceMock = new DateServiceMock();
            var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);

            facade.CommissionNewUnit(new NewUnit("1A", 2, 2, 2, true, true, 20));
            facade.CommissionNewUnit(new NewUnit("2B", 8, 8, 8, false, true, 80));

            facade.AddNewCustomer(new NewCustomer("Alpha", "Alpha Name", "Alpha Address"));
            facade.AddNewCustomer(new NewCustomer("Bravo", "Bravo Name", "Bravo Address"));

            dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);
            facade.ReserveUnit("1A", "Alpha");
            facade.ReserveUnit("2B", "Alpha");
            dateServiceMock.CurrentDateTime = new DateTime(2017, 03, 01);
            Assert.Equal(200, facade.CalculateAmountDue("Alpha"));
            facade.Pay("Alpha", 150);
            Assert.Equal(50, facade.CalculateAmountDue("Alpha"));
            dateServiceMock.CurrentDateTime = new DateTime(2017, 04, 01);
            Assert.Equal(150, facade.CalculateAmountDue("Alpha"));
            facade.Pay("Alpha", 50);
            Assert.Equal(100, facade.CalculateAmountDue("Alpha"));
            facade.Pay("Alpha", 120.43m);
            Assert.Equal(0, facade.CalculateAmountDue("Alpha"));
            dateServiceMock.CurrentDateTime = new DateTime(2017, 05, 01);
            Assert.Equal(100, facade.CalculateAmountDue("Alpha"));
            facade.Pay("Alpha", 100);
            Assert.Equal(0, facade.CalculateAmountDue("Alpha"));
            dateServiceMock.CurrentDateTime = new DateTime(2018, 02, 01);
            Assert.Equal(900, facade.CalculateAmountDue("Alpha"));
            facade.Pay("Alpha", 900);
            Assert.Equal(0, facade.CalculateAmountDue("Alpha"));
        }
    }
}