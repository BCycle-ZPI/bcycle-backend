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
            services.AddSingleton<IAuthorizationHandler, AnonymousAuthorizationHandler>();
            services.AddSingleton(Configuration);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAuthorization();

            services
                .AddAuthentication(FirebaseAuthScheme)
                .AddScheme<AuthenticationSchemeOptions, FirebaseAuthHandler>(FirebaseAuthScheme, null);

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
            app.UseMvc();
        }
    }
}
