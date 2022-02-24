using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Services;
using Bug_Tracker_V1._0.BusinessLogic.Interfaces;
using Bug_Tracker_V1._0.BusinessLogic.Services;
using Bug_Tracker_V1._0.Data;
using Bug_Tracker_V1._0.DataAccess.Repositories;
using Bug_Tracker_V1._0.Facades;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Services;
using BugTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0
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

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();

            services.AddRazorPages();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserProjectRepository, UserProjectRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITicketHistoryRepository, TicketHistoryRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserProjectService, UserProjectService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITicketHistoryService, TicketHistoryService>();
            services.AddScoped<IAttachmentService, AttachmentService>();


            services.AddScoped<IProjectFacade, ProjectFacade>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Project}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
