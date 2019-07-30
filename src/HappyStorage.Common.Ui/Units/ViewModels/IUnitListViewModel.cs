using HappyStorage.Common.Ui.Units.Models;
using Prism.Commands;
using System.ComponentModel;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitListViewModel
    {
        BindingList<UnitLookupModel> Units { get; set; }

        DelegateCommand NextPageCommand { get; }
        DelegateCommand PrevPageCommand { get; }
        int CurrentPage { get; }
        bool HasNextPage { get; }
        bool HasPrevPage { get; }

        void JumpToPage(int? pageNum);

        UnitListFilter Filter { get; set; }

        void Load();

        void ApplyFilter(UnitListFilter filter);

        void ApplyFilter();
    }
}