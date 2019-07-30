using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Units.Models
{
    public class NewUnitModel
    {
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; }

        [Display(Name = "Length")]
        public int Length { get; set; }

        [Display(Name = "Width")]
        public int Width { get; set; }

        [Display(Name = "Height")]
        public int Height { get; set; }

        [Display(Name = "Climate Controlled?")]
        public bool IsClimateControlled { get; set; }

        [Display(Name = "Vehicle Accessible?")]
        public bool IsVehicleAccessible { get; set; }

        [Display(Name = "Monthly Cost")]
        public decimal PricePerMonth { get; set; }
    }
}