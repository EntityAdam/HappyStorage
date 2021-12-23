using HappyStorage.Common.Ui.Customers.ViewModels;
using HappyStorage.Common.Ui.Tenants.ViewModels;
using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
                .AddTransient<ICustomerDetailsViewModel, CustomerDetailsViewModel>()
                .AddTransient<ICustomerListViewModel, CustomerListViewModel>()
                .AddTransient<ITenantLockViewModel, TenantLockViewModel>()
                .AddTransient<ITenantReleaseViewModel, TenantReleaseViewModel>()
                .AddTransient<ITenantUnitsViewModel, TenantUnitsViewModel>()
                .AddTransient<IUnitCreateViewModel, UnitCreateViewModel>()
                .AddTransient<IUnitDecommissionViewModel, UnitDecommissionViewModel>()
                .AddTransient<IUnitListViewModel, UnitListViewModel>()
                .AddTransient<IUnitReserveViewModel, UnitReserveViewModel>();       
        }
    }
}