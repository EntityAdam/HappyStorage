namespace HappyStorage.Common.Ui.Units.Models
{
    public class NewUnitModel
    {
        //TODO: DataAnnotations
        public string UnitNumber { get; internal set; }
     
        public int Length { get; internal set; }
        
        public int Width { get; internal set; }

        public int Height { get; internal set; }

        public bool IsClimateControlled { get; internal set; }
        
        public bool IsVehicleAccessible { get; internal set; }
        
        public decimal PricePerMonth { get; internal set; }
    }
}
