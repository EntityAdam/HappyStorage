using HappyStorage.Common.Ui.Units.Models;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitCreateViewModel
    {
        NewUnitModel NewUnit { get; set; }

        void Create();

        void Create(NewUnitModel newUnit);
    }
}