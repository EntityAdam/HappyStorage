using Bogus;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using HappyStorage.SqlStorage;

var unitDimensions = new[] { 5, 10, 15, 20 };
var unitPrices = new[] { 50m, 100m, 150m, 200m };

Randomizer.Seed = new Random(555789);

var customerRules = new Faker<FakeCustomer>()
    .StrictMode(true)
    .RuleFor(c => c.CustomerNumber, f => Guid.NewGuid().ToString())
    .RuleFor(c => c.FullName, (f, u) => $"{f.Name.FirstName()} {f.Name.LastName()}")
    .RuleFor(c => c.Address, f => $"{f.Address.StreetAddress()}, {f.Address.City()}, {f.Address.StateAbbr()} {f.Address.ZipCode()}");

var unitRules = new Faker<FakeUnit>()
    .StrictMode(true)
    .RuleFor(c => c.UnitNumber, f => Guid.NewGuid().ToString())
    .RuleFor(c => c.IsClimateControlled, f => f.Random.Bool())
    .RuleFor(c => c.IsVehicleAccessible, f => f.Random.Bool())
    .RuleFor(c => c.Height, f => 10)
    .RuleFor(c => c.Length, f => f.PickRandom(unitDimensions))
    .RuleFor(c => c.Width, f => f.PickRandom(unitDimensions))
    .RuleFor(c => c.PricePerMonth, f => f.PickRandom(unitPrices));

var customers = customerRules.GenerateForever();
var units = unitRules.GenerateForever();

var facade = new Facade(
    new SqlUnitStore(new SqlUnitStoreSettings()),
    new SqlCustomerStore(new SqlCustomerStoreSettings()),
    new SqlTenancyStore(new SqlTenancyStoreSettings()),
    new DateService());

for (int i = 0; i < 1000; i++)
{
    facade.AddNewCustomer(customers.Skip(i).Take(1).Select(c => new NewCustomer(c.CustomerNumber, c.FullName, c.Address)).Single());
}

for (int i = 0; i < 1000; i++)
{
    facade.CommissionNewUnit(units.Skip(i).Take(1).Select(c => new NewUnit(c.UnitNumber, c.Length, c.Width, c.Height, c.IsClimateControlled, c.IsVehicleAccessible, c.PricePerMonth)).Single());
}

public class FakeCustomer
{
    public string? CustomerNumber { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
}

public class FakeUnit
{
    public string? UnitNumber { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsClimateControlled { get; set; }
    public bool IsVehicleAccessible { get; set; }
    public decimal PricePerMonth { get; set; }
}

public class SqlUnitStoreSettings : ISqlUnitStoreSettings
{
    public string GetConnectionString() => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HappyStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}
public class SqlCustomerStoreSettings : ISqlCustomerStoreSettings
{
    public string GetConnectionString() => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HappyStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}
public class SqlTenancyStoreSettings : ISqlTenancyStoreSettings
{
    public string GetConnectionString() => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HappyStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}
public class DateService : IDateService
{
    public DateTime GetCurrentDateTime() => DateTime.UtcNow;
}