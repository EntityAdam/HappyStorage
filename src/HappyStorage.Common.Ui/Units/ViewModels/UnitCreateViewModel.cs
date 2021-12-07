﻿using HappyStorage.Common.Ui.Units.Models;
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
            var unit = new NewUnit(newUnit.UnitNumber, newUnit.Length, newUnit.Width, newUnit.Height, newUnit.IsClimateControlled, newUnit.IsVehicleAccessible, newUnit.PricePerMonth);
            facade.CommissionNewUnit(unit);
        }
    }
}