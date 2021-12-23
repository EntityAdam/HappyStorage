namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public interface ITenantLockViewModel
    {
        string UnitNumber { get; set; }

        string CustomerNumber { get; set; }

        public void LockUnit();
    }
}
