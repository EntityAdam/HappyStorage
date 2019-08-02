using HappyStorage.Common.Ui.Extensions;
using HappyStorage.Core;
using HappyStorage.SqlStorage;
using HappyStorage.Web.Services;
using HappyStorage.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HappyStorage.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
            });

            services.AddTransient<IFacade, Facade>();
            services.AddTransient<IUnitStore, SqlUnitStore>();
            services.AddTransient<ISqlUnitStoreSettings, SqlUnitStoreSettings>();
            services.AddTransient<ICustomerStore, SqlCustomerStore>();
            services.AddTransient<ISqlCustomerStoreSettings, SqlCustomerStoreSettings>();
            services.AddTransient<ITenancyStore, SqlTenancyStore>();
            services.AddTransient<ISqlTenancyStoreSettings, SqlTenancyStoreSettings>();
            services.AddTransient<IDateService, DateService>();
            services.AddHappyStorageCommonUi();

            services.AddRazorPages()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}