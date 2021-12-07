namespace HappyStorage.Core.Models
{
    public record NewUnit(string UnitNumber, int Length, int Width, int Height, bool IsClimateControlled, bool IsVehicleAccessible, decimal PricePerMonth);
}