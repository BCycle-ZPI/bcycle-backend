using System.IO;
using bcycle_backend.Data;
using bcycle_backend.Security;
using bcycle_backend.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using static bcycle_backend.Security.FirebaseAuthDefaults;

namespace bcycle_backend
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            FirebaseApp.Create(
                new AppOptions {Credential = GoogleCredential.GetApplicationDefault()}
            );

            services.AddScoped<TripService>();
            services.AddScoped<GroupTripService>();
            services.AddScoped<StatsService>();
            services.AddScoped<UserService>();
            services.AddSingleton<IAuthorizationHandler, AnonymousAuthorizationHandler>();
            services.AddSingleton(Configuration);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAuthorization();

            services
                .AddAuthentication(FirebaseAuthScheme)
                .AddScheme<AuthenticationSchemeOptions, FirebaseAuthHandler>(FirebaseAuthScheme, null);

            services.AddSpaStaticFiles(configuration =>
                configuration.RootPath = "public"
            );

            // TODO: switch to SQL Server in production?
            services.AddDbContext<BCycleContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseAuthentication();
            // app.UseHttpsRedirection();

            var uploadPath = Configuration.GetValue<string>("UploadPath");
            var uploadFullPath = Path.GetFullPath(uploadPath);
            Directory.CreateDirectory(uploadFullPath);


            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadFullPath),
                RequestPath = "/uploads"
            });
            app.UseSpaStaticFiles();
            app.UseMvc();

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api") && !x.Request.Path.Value.StartsWith("/uploads"),
                builder =>
                {
                    builder.UseSpa(spa => { });
                });
        }
    }
}
