namespace HappyStorage.Core.Models
{
    public record CustomerLookup(string CustomerNumber, string FullName, int? UnitsReservedCount);

    //public class CustomerLookup
    //{
    //    public string CustomerNumber { get; set; }
    //    public string FullName { get; set; }
    //    public int UnitsReservedCount { get; set; }
    //}
}