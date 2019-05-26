using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitFilterViewModel
    {
        bool IsClimateControlled { get; set; }
        bool IsVehicleAccessible { get; set; }
        int MinimumCubicFeet { get; set; }
    }

    public class UnitFilterViewModel : IUnitFilterViewModel
    {
        [Display(Name = "Climate Controlled?")]
        public bool IsClimateControlled { get; set; }

        [Display(Name = "Vehicle Accessible")]
        public bool IsVehicleAccessible { get; set; }
        
        [Display(Name = "Min. Cubic Feet")]
        public int MinimumCubicFeet { get; set; }
    }
}
