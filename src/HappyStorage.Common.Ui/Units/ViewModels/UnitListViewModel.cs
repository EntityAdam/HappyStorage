using HappyStorage.Common.Ui.Customers;
using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitListViewModel : BindableBase, IUnitListViewModel
    {
        private const int defaultPageSize = 10;

        private readonly IFacade facade;

        private Pager<AvailableUnit> Pager { get; set; }

        public UnitListViewModel(IFacade facade)
        {
            this.facade = facade;
            Units.ListChanged += Units_ListChanged;
        }

        private UnitListFilter filter;
        public UnitListFilter Filter 
        { 
            get => filter;
            set => SetField(ref filter, value); //TODO: Do we need to update the binding list?
        }

        private void Units_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Units));
        }

        public BindingList<UnitLookupModel> Units { get; set; } = new BindingList<UnitLookupModel>();

        public void Load()
        {
            var units = facade.FindAvailableUnits(null, null, null);
            Pager = new Pager<AvailableUnit>(units, defaultPageSize);
            Update(Pager.Next());
        }

        private void Update(IEnumerable<AvailableUnit> page)
        {
            Units.Clear();
            foreach (var unit in page)
            {
                Units.Add(new UnitLookupModel
                {
                    UnitNumber = unit.UnitNumber,
                    PricePerMonth = unit.PricePerMonth
                });
            }
        }

        public bool CanExecuteNext() => (Pager != null) ? Pager.CanExecuteNext : false;

        public bool CanExecuteBack() => (Pager != null) ? Pager.CanExecuteBack : false;

        public void ApplyFilter(bool? isVehicleAccessible, bool? isClimateControlled, int? minimumCubicFeet)
        {
            throw new System.NotImplementedException();
        }
    }
}
