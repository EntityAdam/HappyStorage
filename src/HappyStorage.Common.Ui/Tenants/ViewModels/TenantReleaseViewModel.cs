using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public class TenantReleaseViewModel : ITenantReleaseViewModel
    {
        private readonly IFacade facade;

        public TenantReleaseViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public string UnitNumber { get; set; }
        
        public string CustomerNumber { get; set; }
        
        public void ReleaseUnit()
        {
            ReleaseUnit(this.UnitNumber, this.CustomerNumber);
        }

        public void ReleaseUnit(string unitNumber, string customerNumber)
        {
            facade.ReleaseUnit(unitNumber, customerNumber);
        }
    }
}
