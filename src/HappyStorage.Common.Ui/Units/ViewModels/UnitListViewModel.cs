using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitListViewModel : BindableBase, IUnitListViewModel
    {
        private const int defaultPageSize = 4;

        private readonly IFacade facade;

        private Pager<AvailableUnit> Pager { get; set; }

        public UnitListViewModel(IFacade facade)
        {
            this.facade = facade;
            Units.ListChanged += Units_ListChanged;

            NextPageCommand = new DelegateCommand(
                () => Next(),
                () => (Pager != null) && Pager.CanExecuteNext
            );
            PrevPageCommand = new DelegateCommand(
                () => Prev(),
                () => (Pager != null) && Pager.CanExecutePrev
            );
        }

        public BindingList<UnitLookupModel> Units { get; set; } = new BindingList<UnitLookupModel>();

        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PrevPageCommand { get; set; }

        public int CurrentPage => (Pager != null) ? Pager.CurrentPage : 0;
        public bool HasPrevPage => PrevPageCommand.CanExecute();
        public bool HasNextPage => NextPageCommand.CanExecute();

        private void Units_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Units));
        }

        public void Load()
        {
            var units = facade.SearchAvailableUnits(null, null, null);
            Pager = new Pager<AvailableUnit>(units, defaultPageSize);
            UpdateList(Pager.First());
        }

        public void Next()
        {
            UpdateList(Pager.Next());
        }

        public void Prev()
        {
            UpdateList(Pager.Prev());
        }

        private UnitListFilter filter = new();

        public UnitListFilter Filter
        {
            get => filter;
            set
            {
                SetField(ref filter, value);
                ApplyFilter();
            }
        }

        private void UpdateList(IEnumerable<AvailableUnit> page)
        {
            Units.Clear();
            foreach (var c in page)
            {
                Units.Add(new UnitLookupModel()
                {
                    UnitNumber = c.UnitNumber,
                    PricePerMonth = c.PricePerMonth
                });
            }
        }

        public void ApplyFilter(UnitListFilter filter)
        {
            this.Filter = filter;
        }

        public bool CanExecuteNext() => (Pager != null) && Pager.CanExecuteNext;

        public bool CanExecuteBack() => (Pager != null) && Pager.CanExecutePrev;

        public void JumpToPage(int? pageNum)
        {
            if (pageNum != null)
            {
                UpdateList(Pager.Page((int)pageNum));
            }
            else
            {
                UpdateList(Pager.First());
            }
        }

        public void ApplyFilter()
        {
            var filteredUnits = facade.SearchAvailableUnits(filter.IsClimateControlled, filter.IsVehicleAccessible, filter.MinimumCubicFeet);
            UpdateList(filteredUnits);
        }
    }
}