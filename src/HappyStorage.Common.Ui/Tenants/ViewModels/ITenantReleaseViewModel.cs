namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public interface ITenantReleaseViewModel
    {
        //TODO: Should provide thorough details of the Unit and Customer before releasing tenancy
        //public IUnitDetailsViewModel UnitDetailsViewModel { get; set; }
        //public ICustomerDetailsViewModel CustomerDetailsViewModel { get; set; }

        string UnitNumber { get; set; }
        string CustomerNumber { get; set; }
        void ReleaseUnit();
        void ReleaseUnit(string unitNumber, string customerNumber);
    }
}
