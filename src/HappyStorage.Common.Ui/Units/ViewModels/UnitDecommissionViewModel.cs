using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitDecommissionViewModel : IUnitDecommissionViewModel
    {
        private readonly IFacade facade;

        public UnitDecommissionViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public string UnitNumber { get; set; }

        public void Decommission(string unitNumber)
        {
            facade.DecommissionUnit(unitNumber);
        }
    }
}
