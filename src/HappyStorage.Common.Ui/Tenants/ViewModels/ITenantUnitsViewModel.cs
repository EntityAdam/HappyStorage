using HappyStorage.Common.Ui.Tenants.Models;
using System.Collections.Generic;

namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public interface ITenantUnitsViewModel
    {
        string SelectedCustomerNumber { get; set; }
        List<TenantUnitModel> TenantUnits { get; }
    }
}