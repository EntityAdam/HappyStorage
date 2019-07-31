using HappyStorage.BlazorWeb.Data;
using HappyStorage.BlazorWeb.Services;
using HappyStorage.BlazorWeb.Settings;
using HappyStorage.Common.Ui.Extensions;
using HappyStorage.Core;
using HappyStorage.FileStorage;
using HappyStorage.MemoryStorage;
using HappyStorage.SqlStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HappyStorage.BlazorWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddTransient<IFacade, Facade>();
            
            //unit store
            services.AddTransient<IUnitStore, SqlUnitStore>();
            services.AddTransient<ISqlUnitStoreSettings, SqlUnitStoreSettings>();

            //customer store
            //services.AddTransient<ICustomerStore, FileCustomerStore>();
            //services.AddTransient<IFileCustomerStoreSettings, FileCustomerStoreSettings>();
            services.AddTransient<ICustomerStore, SqlCustomerStore>();
            services.AddTransient<ISqlCustomerStoreSettings, SqlCustomerStoreSettings>();

            //tenancy store
            //services.AddTransient<ITenancyStore, MemoryTenancyStore>();
            services.AddTransient<ITenancyStore, SqlTenancyStore>();
            services.AddTransient<ISqlTenancyStoreSettings, SqlTenancyStoreSettings>();

            //date service
            services.AddTransient<IDateService, DateService>();

            services.AddHappyStorageCommonUi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}