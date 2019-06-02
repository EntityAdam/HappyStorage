using HappyStorage.Common.Ui.Tenants.Models;
using HappyStorage.Common.Ui.Units.Models;
using System.Collections.Generic;
using System.Text;

namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    public interface ITenantUnitsViewModel
    {
        string SelectedCustomerNumber { get; set; }
        List<TenantUnitModel> TenantUnits { get; }
    }
}
