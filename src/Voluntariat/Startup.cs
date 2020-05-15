using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using Voluntariat.Data;
using Voluntariat.Data.Repositories;
using Voluntariat.Models;
using Voluntariat.Services;
using Voluntariat.Services.CloudFileServices;

namespace Voluntariat
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
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new[]
                {
                new CultureInfo("en"),
                new CultureInfo("ro")
            };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = bool.Parse(Configuration["RequireConfirmedAccount"]))
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<Framework.Identity.IdentityAuthorizationFilter>();

                options.EnableEndpointRouting = false;
            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            services.AddRazorPages();


            services.AddScoped<IVolunteerMatchingService, VolunteerMatchingService>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IVolunteerService, VolunteerService>();

            services.AddScoped<IScheduledTask, ScheduledTask>();
            services.AddScoped<ITaskScheduler, TaskScheduler>();
            InjectSecretRelatedServices(services);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/dist";
            });

            AddSwaggerGen(services);
        }

        private void InjectSecretRelatedServices(IServiceCollection services)
        {
            bool noUserSecrets = bool.Parse(Configuration["NoUserSecrets"]);
            if (!noUserSecrets)
            {
                services.AddSingleton<IEmailSender, EmailSender>();
                services.AddSingleton<ISecureCloudFileManager, SecureCloudFileManager>();
                services.AddSingleton<IPublicCloudFileManager, PublicCloudFileManager>();

                services.AddHttpClient<TwilioVerifyClient>(client =>
                {
                    client.BaseAddress = new Uri("https://api.authy.com/");
                    client.DefaultRequestHeaders.Add("X-Authy-API-Key", Configuration["Twillio:Authy:ApiKey"]);
                });
            }
            else
            {
                services.AddSingleton<ISecureCloudFileManager, FakeCloudFileManager>();
                services.AddSingleton<IPublicCloudFileManager, FakeCloudFileManager>();
                Configuration["RequireConfirmedAccount"] = "false";
            }
        }

        // test commit 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                UpdateDatabase(app);

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // some more changes

            StartScheduleTasks(app);

            ApplicationDbInitializer.SeedUsers(userManager);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSpaStaticFiles(new StaticFileOptions() { RequestPath = new PathString("/angular") });

            app.UseRouting();
            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                
            });

            app.UseSpa(spa =>
            {
                //spa.Options.DefaultPage = new PathString("/angular");
                spa.Options.SourcePath = "wwwroot";
            });
        }

        private static void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "3.0" });
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ApplicationDbContext applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    applicationDbContext.Database.Migrate();
                }
            }
        }

        private void StartScheduleTasks(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<ITaskScheduler>();
                service.RunBackgoundTasks();
            }
        }
    }
}