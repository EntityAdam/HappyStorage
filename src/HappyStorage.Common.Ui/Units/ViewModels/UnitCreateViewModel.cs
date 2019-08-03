using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Core;
using HappyStorage.Core.Models;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitCreateViewModel : IUnitCreateViewModel
    {
        private readonly IFacade facade;

        public UnitCreateViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public NewUnitModel NewUnit { get; set; } = new NewUnitModel();

        public void Create()
        {
            Create(this.NewUnit);
        }

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