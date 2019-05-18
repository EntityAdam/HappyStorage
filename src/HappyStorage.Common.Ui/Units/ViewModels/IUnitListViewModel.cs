using System.ComponentModel;
using HappyStorage.Common.Ui.Units.Models;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitListViewModel
    {
        BindingList<UnitLookupModel> Units { get; set; }

        UnitListFilter Filter { get; set; }

        bool CanExecuteBack();
        
        bool CanExecuteNext();
        
        void Load();
        void ApplyFilter(bool? isVehicleAccessible, bool? isClimateControlled, int? minimumCubicFeet);
    }
}