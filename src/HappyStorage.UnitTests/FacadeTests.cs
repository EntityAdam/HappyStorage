using System;
using System.Linq;
using Xunit;
using HappyStorage.Core;

namespace HappyStorage.UnitTests
{
	public class FacadeTests
	{
		[Fact]
		public void CommissionsAndDecommissionsUnitsCorrectly()
		{
			var unitStoreMock = new UnitStoreMock();
			var facade = new Facade(unitStoreMock, new CustomerStoreDummy(), new TenancyStoreDummy(), new DateServiceDummy());
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "1A",
				Length = 10,
				Width = 12,
				Height = 14,
				IsClimateControlled = true,
				IsVehicleAccessible = true,
				PricePerMonth = 90
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "2B",
				IsClimateControlled = false,
				IsVehicleAccessible = false,
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "3C",
				IsClimateControlled = true,
				IsVehicleAccessible = false,
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "4D",
				IsClimateControlled = false,
				IsVehicleAccessible = true,
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "5E",
				IsClimateControlled = false,
				IsVehicleAccessible = false,
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "6F",
				IsClimateControlled = false,
				IsVehicleAccessible = false,
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "7G"
			});
			facade.DecommissionUnit("7G");
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "8H"
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "9I"
			});
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
		public void AddsAndDeletesCustomersCorrectly()
		{
			var customerStoreMock = new CustomerStoreMock();
			var facade = new Facade(new UnitStoreMock(), customerStoreMock, new TenancyStoreDummy(), new DateServiceDummy());
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Alpha",
				FullName = "Alpha Name", 
				Address = "Alpha Address"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Bravo",
				FullName = "Bravo Name",
				Address = "Bravo Address"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Charlie",
				FullName = "Charlie Name",
				Address = "Charlie Address"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Delta",
				FullName = "Delta Name",
				Address = "Delta Address"
			});
			facade.DeleteCustomer("Charlie");
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Echo",
				FullName = "Echo Name",
				Address = "Echo Address"
			});
			facade.DeleteCustomer("Alpha");
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Foxtrot",
				FullName = "Foxtrot Name",
				Address = "Foxtrot Address"
			});
			Assert.Equal(4, customerStoreMock.Customers.Count);
			var delta = customerStoreMock.Customers.Single(c => c.CustomerNumber == "Delta");
			Assert.Equal("Delta Name", delta.FullName);
			Assert.Equal("Delta Address", delta.Address);
			var customerNumbers = customerStoreMock.Customers.Select(c => c.CustomerNumber).ToArray();
			Assert.Contains("Bravo", customerNumbers);
			Assert.Contains("Delta", customerNumbers);
			Assert.Contains("Echo", customerNumbers);
			Assert.Contains("Foxtrot", customerNumbers);
		}

		[Fact]
		public void Scenario1()
		{
			var unitStoreMock = new UnitStoreMock();
			var customerStoreMock = new CustomerStoreMock();
			var tenancyStoreMock = new TenancyStoreMock();
			var dateServiceMock = new DateServiceMock();
			var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "A1",
				Length = 5,
				Width = 5,
				Height = 8,
				IsClimateControlled = false,
				IsVehicleAccessible = true,
				PricePerMonth = 50
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "B2",
				Length = 10,
				Width = 5,
				Height = 8,
				IsClimateControlled = false,
				IsVehicleAccessible = true,
				PricePerMonth = 60
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "C3",
				Length = 10,
				Width = 10,
				Height = 8,
				IsClimateControlled = false,
				IsVehicleAccessible = true,
				PricePerMonth = 70
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "D4",
				Length = 4,
				Width = 4,
				Height = 4,
				IsClimateControlled = true,
				IsVehicleAccessible = false,
				PricePerMonth = 100
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "E5",
				Length = 6,
				Width = 6,
				Height = 6,
				IsClimateControlled = true,
				IsVehicleAccessible = false,
				PricePerMonth = 110
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "F6",
				Length = 8,
				Width = 8,
				Height = 8,
				IsClimateControlled = true,
				IsVehicleAccessible = false,
				PricePerMonth = 120
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Alpha"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Bravo"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Charlie"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Delta"
			});
			var find1 = facade.FindAvailableUnits(null, null, null).ToArray();
			Assert.Equal(6, find1.Length);
			var find2 = facade.FindAvailableUnits(true, null, 70).ToArray();
			Assert.Equal(2, find2.Length);
			var find3 = facade.FindAvailableUnits(null, true, 750).ToArray();
			Assert.Single(find3);
			var unit = find3.Single();
			Assert.Equal("C3", unit.UnitNumber);
			dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);
			facade.ReserveUnit("C3", "Charlie");
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
			var find4 = facade.FindAvailableUnits(null, true, 750).ToArray();
			Assert.Empty(find4);
			facade.Pay("Charlie", 70);
			facade.ReleaseUnit("C3", "Charlie");
			dateServiceMock.CurrentDateTime = new DateTime(2017, 07, 01);
			Assert.Equal(0, facade.CalculateAmountDue("Charlie"));
			var find5 = facade.FindAvailableUnits(null, true, 750).ToArray();
			Assert.Single(find5);
			var find6 = facade.FindAvailableUnits(null, null, null).ToArray();
			Assert.Equal(6, find6.Length);


			var customerLookup = facade.ListCustomers();
			Assert.Equal(4, customerLookup.Count());
		}

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
				facade.CommissionNewUnit(new NewUnit());
			});
			Assert.Throws<ArgumentException>(() =>
			{
				facade.DecommissionUnit(String.Empty);
			});
			Assert.Throws<ArgumentException>(() =>
			{
				facade.AddNewCustomer(new NewCustomer());
			});
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
		public void RejectsDuplicateUnits()
		{
			var unitStoreMock = new UnitStoreMock();
			var facade = new Facade(unitStoreMock, new CustomerStoreDummy(), new TenancyStoreDummy(), new DateServiceDummy());
			facade.CommissionNewUnit(new NewUnit() { UnitNumber = "Number" });
			Assert.Throws<InvalidOperationException>(() =>
			{
				facade.CommissionNewUnit(new NewUnit() { UnitNumber = "Number" });
			});
		}

		[Fact]
		public void RejectsDuplicateCustomers()
		{
			var customerStoreMock = new CustomerStoreMock();
			var facade = new Facade(new UnitStoreDummy(), customerStoreMock, new TenancyStoreDummy(), new DateServiceDummy());
			facade.AddNewCustomer(new NewCustomer() { CustomerNumber = "Number" });
			Assert.Throws<InvalidOperationException>(() =>
			{
				facade.AddNewCustomer(new NewCustomer() { CustomerNumber = "Number" });
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
			facade.CommissionNewUnit(new NewUnit() { UnitNumber = "A1" });
			facade.AddNewCustomer(new NewCustomer() { CustomerNumber = "Alpha" });
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
			facade.CommissionNewUnit(new NewUnit() { UnitNumber = "A1" });
			facade.AddNewCustomer(new NewCustomer() { CustomerNumber = "Alpha" });
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
		public void Scenario2()
		{
			var unitStoreMock = new UnitStoreMock();
			var customerStoreMock = new CustomerStoreMock();
			var tenancyStoreMock = new TenancyStoreMock();
			var dateServiceMock = new DateServiceMock();
			var facade = new Facade(unitStoreMock, customerStoreMock, tenancyStoreMock, dateServiceMock);
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "A1",
				Length = 2,
				Width = 2,
				Height = 2,
				IsClimateControlled = true,
				IsVehicleAccessible = false,
				PricePerMonth = 20
			});
			facade.CommissionNewUnit(new NewUnit()
			{
				UnitNumber = "B2",
				Length = 8,
				Width = 8,
				Height = 8,
				IsClimateControlled = false,
				IsVehicleAccessible = true,
				PricePerMonth = 80
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Alpha"
			});
			facade.AddNewCustomer(new NewCustomer()
			{
				CustomerNumber = "Bravo"
			});
			dateServiceMock.CurrentDateTime = new DateTime(2017, 01, 01);
			facade.ReserveUnit("A1", "Alpha");
			facade.ReserveUnit("B2", "Alpha");
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
