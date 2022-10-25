using FluentValidation.AspNetCore;
using Hangfire;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.ScheduleJobs;
using InstaAutoPost.UI.Core.ScheduleJobs.Main;
using InstaAutoPost.UI.WebUI.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI
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
            services.AddHangfire(_ => _.UseSqlServerStorage(Configuration.GetValue<string>("ConnectionString")));
            services.AddControllersWithViews();


            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRssRunnerService, RssRunnerService>();
            services.AddScoped<ISourceContentService, SourceContentService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAutoJobService, AutoJobService>();
            services.AddScoped<ICategoryTypeService, CategoryTypeService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<ISocialMediaAccountsCategoryTypeService, SocialMediaAccountsCategoryTypeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            new ScheduleJobRunner().RemoveAll();
            new ScheduleJobRunner().RunJobs(env.ContentRootPath);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Post}/{action=Index}/{id?}");
            });
           
        }
    }
}
