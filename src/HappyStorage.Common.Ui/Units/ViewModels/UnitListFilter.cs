using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    //TODO: Move to models
    public class UnitListFilter
    {
        [Display(Name = "Climate Controlled?")]
        public bool IsClimateControlled { get; set; }

        [Display(Name = "Vehicle Accessible?")]
        public bool IsVehicleAccessible { get; set; }

        [Display(Name = "Min. Cubic Feet")]
        public int MinimumCubicFeet { get; set; }
    }
}