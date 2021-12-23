using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public class TenantLockViewModel : ITenantLockViewModel
    {
        private readonly IFacade facade;

        public TenantLockViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public string UnitNumber { get; set; }

        public string CustomerNumber { get; set; }

        public void LockUnit()
        {
            Lock(this.UnitNumber, this.CustomerNumber);
        }

        public void Lock(string unitNumber, string customerNumber)
        {
            facade.LockUnit(unitNumber, customerNumber);
        }
    }
}