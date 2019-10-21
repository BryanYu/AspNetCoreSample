using System;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AspNetCoreRazorSample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreRazorSample
{
    public class Startup
    {
        private readonly IHostEnvironment _host;
        private IWebHostEnvironment _env;

        public Startup(IHostEnvironment host ,IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _host = host;
            _env = env;
        }

        public IConfiguration Configuration { get; }


        // Startup 先呼叫 ConfigureServices 用來設定應用程式服務'
        // 多次呼叫會彼此附加
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddRazorPages();

            services.AddDbContext<RazorPageMovieContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazorPageMovieContext")));

            
        }

        // Startup 再呼叫 Configure 設定應用程式對http要求的回應方式
        // 多次呼叫會使用最後一次呼叫
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }


    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSetOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["options"];
            if (!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Items["option"] = WebUtility.HtmlEncode(option);
            }

            await _next(httpContext);
        }
    }

    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<RequestSetOptionsMiddleware>();
                next(builder);
            };
        }
    }
}
