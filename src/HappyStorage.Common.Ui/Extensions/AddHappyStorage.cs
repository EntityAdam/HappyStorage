using Microsoft.Extensions.DependencyInjection;
using HappyStorage.Common.Ui.Units.ViewModels;
using HappyStorage.Common.Ui.Tenants.ViewModels;
using HappyStorage.Common.Ui.Customers.ViewModels;

namespace HappyStorage.Common.Ui.Extensions
{
    public static class AddHappyStorageExtension
    {
        public static void AddHappyStorageCommonUi(this IServiceCollection services)
        {
            //Keep me sorted alphabetic!
            services
                .AddTransient<ICustomerCreateViewModel, CustomerCreateViewModel>()
                .AddTransient<ICustomerDeleteViewModel, CustomerDeleteViewModel>()
                .AddTransient<ICustomerListViewModel, CustomerListViewModel>()
                .AddTransient<ITenantReleaseViewModel, TenantReleaseViewModel>()
                .AddTransient<ITenantUnitsViewModel, TenantUnitsViewModel>()
                .AddTransient<IUnitCreateViewModel, UnitCreateViewModel>()
                .AddTransient<IUnitDecommissionViewModel, UnitDecommissionViewModel>()
                .AddTransient<IUnitListViewModel, UnitListViewModel>()
                .AddTransient<IUnitReserveViewModel, UnitReserveViewModel>();
        }
    }
}

