using Microsoft.Extensions.DependencyInjection;
using HappyStorage.Common.Ui.Customers;
using HappyStorage.Common.Ui.Units.ViewModels;

namespace HappyStorage.Common.Ui.Extensions
{
    public static class AddHappyStorageExtension
    {
        public static void AddHappyStorageCommonUi(this IServiceCollection services)
        {
            services
                .AddTransient<ICustomerListViewModel, CustomerListViewModel>()
                .AddTransient<ICustomerCreateViewModel, CustomerCreateViewModel>()
                .AddTransient<ICreateUnitViewModel, CreateUnitViewModel>()
                .AddTransient<IUnitDecommissionViewModel, UnitDecommissionViewModel>()
                .AddTransient<IUnitReserveViewModel, UnitReserveViewModel>()
                .AddTransient<IUnitListViewModel, UnitListViewModel>();
        }
    }
}

