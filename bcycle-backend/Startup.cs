﻿using System.IO;
﻿using System;
using System.Collections.Generic;
using bcycle_backend.Data;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using static bcycle_backend.ProjectConstants;
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
            services.AddScoped<ShareService>();
            services.AddSingleton(new FirebaseStorage(FirebaseStorageBucket));
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

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "WebApp/build"; });

            // Environment variable format: Database=...;Data Source=host:port;User Id=...;Password=...
            // Expected format: Server=host;Port=port;Database=...;User=...;Password=...
            String envConnectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
            String connectionString;

            if (envConnectionString != null)
            {
                String[] groups = envConnectionString.Split(";", 4);
                List<string> newGroups = new List<string>();
                foreach (String group in groups)
                {
                    String[] values = group.Split("=", 2);
                    switch (values[0])
                    {
                        case "Data Source":
                            String[] hostPort = values[1].Split(":");
                            newGroups.Insert(0, $"Server={hostPort[0]};Port={hostPort[1]}");
                            break;
                        case "User Id":
                            newGroups.Add($"User={values[1]}");
                            break;
                        default:
                            newGroups.Add(group);
                            break;
                    }
                }
                connectionString = string.Join(";", newGroups);
            }
            else
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }

            services.AddDbContext<BCycleContext>(options => options.UseMySql(connectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseAuthentication();
            // app.UseHttpsRedirection();

            var uploadFullPath = Path.GetFullPath(UploadsDirectory);
            Directory.CreateDirectory(uploadFullPath);


            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadFullPath), RequestPath = $"/{UploadsDirectory}"
            });
            app.UseSpaStaticFiles();
            app.UseMvc();

            app.MapWhen(
                p => IsNotReserved(p.Request.Path),
                builder =>
                {
                    builder.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "WebApp";
                        if (env.IsDevelopment()) spa.UseReactDevelopmentServer(npmScript: "start");
                    });
                }
            );
        }

        private bool IsNotReserved(PathString path) =>
            !path.Value.StartsWith("/api") && !path.Value.StartsWith($"/{UploadsDirectory}");
    }
}
