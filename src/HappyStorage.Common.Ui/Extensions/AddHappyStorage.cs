using Microsoft.Extensions.DependencyInjection;
using HappyStorage.Common.Ui.Customers;

namespace HappyStorage.Common.Ui.Extensions
{
    public static class AddHappyStorageExtension
    {
        public static void AddHappyStorageCommonUi(this IServiceCollection services)
        {
            services
                .AddTransient<ICustomerListViewModel, CustomerListViewModel>()
                .AddTransient<ICustomerCreateViewModel, CustomerCreateViewModel>();
        }
    }
}

