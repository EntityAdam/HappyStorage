namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitDecommissionViewModel
    {
        string UnitNumber { get; set; }

        void Decommission(string unitNumber);
    }
}