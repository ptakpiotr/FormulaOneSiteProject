using FormulaOneSite.Data;
using FormulaOneSite.Data.ApiAccess;
using FormulaOneSite.Emailing;
using FormulaOneSite.Hangfire;
using FormulaOneSite.Hubs;
using FormulaOneSite.Middlewares;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<PostsDbContext>(options=>
                options.UseNpgsql(Configuration.GetConnectionString("PostsConnection"))
                );

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "uploads")));

            services.AddTransient<IEmailSender,EmailSender>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();
            services.AddSignalR();
            services.AddHangfire((configuration)=>configuration.UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection")));


            services.AddScoped<IHangfireJobs, HangfireJobs>();
            services.AddScoped<IPostsRepo, PostsRepo>();
            services.AddScoped<ICommentRepo,CommentRepo>();
            services.AddScoped<IResultsApiAccess, ResultsApiAccess>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSerilogRequestLogging();

            //potentially unsafe
            app.UseCors((opts)=> {
                opts.AllowAnyOrigin().AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseHangfireDashboard("/dashboard");
            }

            app.UseHangfireServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDeleteNotConfirmed();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<CommentsHub>("/comments");
            });
        }
    }
}
