using Microsoft.Extensions.DependencyInjection;
using HappyStorage.Common.Ui.Customers;
using HappyStorage.Common.Ui.Units.ViewModels;
using HappyStorage.Common.Ui.Tenants.ViewModels;

namespace HappyStorage.Common.Ui.Extensions
{
    public static class AddHappyStorageExtension
    {
        public static void AddHappyStorageCommonUi(this IServiceCollection services)
        {
            services
                .AddTransient<ICustomerListViewModel, CustomerListViewModel>()
                .AddTransient<ICustomerCreateViewModel, CustomerCreateViewModel>()
                .AddTransient<IUnitCreateViewModel, UnitCreateViewModel>()
                .AddTransient<IUnitDecommissionViewModel, UnitDecommissionViewModel>()
                .AddTransient<IUnitReserveViewModel, UnitReserveViewModel>()
                .AddTransient<IUnitListViewModel, UnitListViewModel>()
                .AddTransient<ITenantUnitsViewModel, TenantUnitsViewModel>()
                .AddTransient<ITenantReleaseViewModel, TenantReleaseViewModel>();
        }
    }
}

