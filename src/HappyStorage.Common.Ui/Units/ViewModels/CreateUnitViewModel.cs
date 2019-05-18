using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Core;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class CreateUnitViewModel : ICreateUnitViewModel
    {
        private readonly IFacade facade;

        public CreateUnitViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public NewUnitModel NewUnit { get; set; }

        public void Create(NewUnitModel newUnit)
        {
            var unit = new NewUnit()
            {
                UnitNumber = newUnit.UnitNumber,
                Length = newUnit.Length,
                Width = newUnit.Width,
                Height = newUnit.Height,
                IsClimateControlled = newUnit.IsClimateControlled,
                IsVehicleAccessible = newUnit.IsVehicleAccessible,
                PricePerMonth = newUnit.PricePerMonth
            };
            facade.CommissionNewUnit(unit);
        }
    }
}
