namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public interface ITenantReleaseViewModel
    {
        string UnitNumber { get; set; }

        string CustomerNumber { get; set; }

        void ReleaseUnit();

        void ReleaseUnit(string unitNumber, string customerNumber);
    }
}