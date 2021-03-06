using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AspNetCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BookstoreDatabaseSettings>(
                Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

            services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IAUserInterface, UserService>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IBUserInterface, UserService>());

            services.AddSingleton<BookService>();
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => options.UseCamelCasing(true));

            services.Configure<MyOptions>(Configuration);

            services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
            {
                myOptions.Option1 = "value1_configured_by_delegate";
                myOptions.Option2 = 500;
            });

            services.Configure<MySubOptions>(Configuration.GetSection("subsection"));


            services.Configure<MyOptions>("named_option_1", Configuration);
            services.Configure<MyOptions>("named_option_2", myOptions =>
                {
                    myOptions.Option1 = "named_options_2_value1_from_action";
                });

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.Configure<BookstoreDatabaseSettings>(
                Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

            services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IAUserInterface, UserService>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IBUserInterface, UserService>());

            services.AddSingleton<BookService>();
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => options.UseCamelCasing(true));

            services.Configure<MyOptions>(Configuration);

            services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
            {
                myOptions.Option1 = "value1_configured_by_delegate";
                myOptions.Option2 = 500;
            });

            services.Configure<MySubOptions>(Configuration.GetSection("subsection"));


            services.Configure<MyOptions>("named_option_1", Configuration);
            services.Configure<MyOptions>("named_option_2", myOptions =>
            {
                myOptions.Option1 = "named_options_2_value1_from_action";
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
