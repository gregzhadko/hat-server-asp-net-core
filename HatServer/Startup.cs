using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using HatServer.DAL;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using HatServer.Middleware;
using HatServer.Migrations;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;

namespace HatServer
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [UsedImplicitly]
        public void ConfigureServices([NotNull] IServiceCollection services)
        {
            services.AddDbContext<FillerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StatisticsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ServerUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<FillerDbContext>()
                .AddDefaultTokenProviders();

            AddRepositoriesToServices(services);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });


            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(true));
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    })
                .AddFluentValidation();

            AddValidatorsToService(services);

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        private static void AddRepositoriesToServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IPackRepository), typeof(PackRepository));
            services.AddScoped(typeof(IPhraseRepository), typeof(PhraseRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        }

        private static void AddValidatorsToService(IServiceCollection services)
        {
            services.AddTransient<IValidator<Pack>, PackValidator>();
            services.AddTransient<IValidator<PostPhraseItemRequest>, PostPhraseItemRequestValidator>();
            services.AddTransient<IValidator<PutPhraseItemRequest>, PutPhraseItemRequestValidator>();
            services.AddTransient<IValidator<PostReviewRequest>, PostReviewRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
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
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseMiddleware(typeof(StatisticsMiddleware));

            app.UseAuthentication();

            ApplyMigrationAndSeeding(app);

            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}"));
        }

        private void ApplyMigrationAndSeeding([NotNull] IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var fillerContext = serviceScope.ServiceProvider.GetService<FillerDbContext>();

                //var userManager = serviceScope.ServiceProvider.GetService<UserManager<ServerUser>>();
                //var dbInitializer = new DbInitializer(context, userManager);
                //dbInitializer.Initialize();

                if (!fillerContext.AllMigrationsApplied())
                {
                    fillerContext.Database.Migrate();

                    //var userManager = serviceScope.ServiceProvider.GetService<UserManager<ServerUser>>();
                    //var dbInitializer = serviceScope.ServiceProvider.GetService<IDbInitializer>();
                    //dbInitializer.Initialize(fillerContext, userManager, Configuration);
                }

                var statisticsContext = serviceScope.ServiceProvider.GetService<StatisticsDbContext>();
                if (!statisticsContext.AllMigrationsApplied())
                {
                    statisticsContext.Database.Migrate();
                }
            }
        }
    }
}
