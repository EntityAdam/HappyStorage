namespace HappyStorage.Core.Models
{
    public record NewUnit(string UnitNumber, int Length, int Width, int Height, bool IsClimateControlled, bool IsVehicleAccessible, decimal PricePerMonth);
    
    //public class NewUnit
    //{
    //    public string UnitNumber { get; set; }
    //    public int Length { get; set; }
    //    public int Width { get; set; }
    //    public int Height { get; set; }
    //    public bool IsClimateControlled { get; set; }
    //    public bool IsVehicleAccessible { get; set; }
    //    public decimal PricePerMonth { get; set; }
    //}
}