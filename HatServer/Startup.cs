﻿using FluentValidation;
using FluentValidation.AspNetCore;
using HatServer.DAL;
using HatServer.Data;
using HatServer.Migrations;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace HatServer
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [UsedImplicitly]
        public void ConfigureServices([NotNull] IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ServerUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<PhraseItem>), typeof(PhraseRepository));
            services.AddScoped(typeof(IPackRepository), typeof(PackRepository));
            services.AddScoped(typeof(IPhraseRepository), typeof(PhraseRepository));

            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    })
                .AddFluentValidation();

            AddValidatorsToService(services);

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        private static void AddValidatorsToService(IServiceCollection services)
        {
            services.AddTransient<IValidator<Pack>, PackValidator>();
            services.AddTransient<IValidator<PhraseItem>, PhraseValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure([NotNull] IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            ApplyMigrationAndSeeding(app);

            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}"));
        }

        private static void ApplyMigrationAndSeeding([NotNull] IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                //var userManager = serviceScope.ServiceProvider.GetService<UserManager<ServerUser>>();
                //var dbInitializer = new DbInitializer(context, userManager);
                //dbInitializer.Initialize();

                if (!context.AllMigrationsApplied())
                {
                    context.Database.Migrate();

                    //var userManager = serviceScope.ServiceProvider.GetService<UserManager<ServerUser>>();
                    //var dbInitializer = new DbInitializer(context, userManager);
                    //dbInitializer.Initialize();
                }
            }
        }
    }
}
