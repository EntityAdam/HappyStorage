using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitFilterViewModel
    {
        bool IsClimateControlled { get; }
        bool IsVehicleAccessible { get; }
        int MinimumCubicFeet { get; }
    }

    public class UnitFilterViewModel : IUnitFilterViewModel
    {
        [Display(Name = "Climate Controlled?")]
        public bool IsClimateControlled => true;
        [Display(Name = "Vehicle Accessible")]
        public bool IsVehicleAccessible => false;
        [Display(Name = "Min. Cubic Feet")]
        public int MinimumCubicFeet => 10;
    }
}
