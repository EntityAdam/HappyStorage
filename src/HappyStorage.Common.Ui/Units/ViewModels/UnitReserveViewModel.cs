using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitReserveViewModel : IUnitReserveViewModel
    {
        private readonly IFacade facade;

        public UnitReserveViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public void ReserveUnit(string unitNumber, string customerNumber)
        {
            facade.ReserveUnit(unitNumber, customerNumber);
        }
    }
}
