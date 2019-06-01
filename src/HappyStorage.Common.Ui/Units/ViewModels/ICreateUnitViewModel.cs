using HappyStorage.Common.Ui.Units.Models;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface ICreateUnitViewModel
    {
        NewUnitModel NewUnit { get; set; }

        void Create();

        void Create(NewUnitModel newUnit);
    }
}