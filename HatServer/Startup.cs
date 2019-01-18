using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HatServer.DAL;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using HatServer.Middleware;
using HatServer.Migrations;
using HatServer.Services;
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
using Newtonsoft.Json.Serialization;
using OldServer;
using Swashbuckle.AspNetCore.Swagger;

namespace HatServer
{
    public sealed class Startup
    {
        private const string ConnectionStringName = "AzureConnection";

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
                options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringName)));

            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringName)));

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info{Title = "Hat API", Version = "v1"}));

            services.AddScoped<FillerDbSeeder>();

            services.AddAutoMapper();

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
            services.AddScoped<IAnalyticsBusinessLogic, AnalyticsBusinessLogic>();
            
            services.AddHttpClient<IBotNotifier, BotNotifier>();
            services.AddHttpClient<IOldServerService, OldServerService>();
            services.AddHttpClient<IMongoServiceClient, MongoServiceClient>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            ConfigureAuthentication(services);

            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.Converters.Add(
                            new Newtonsoft.Json.Converters.StringEnumConverter(new CamelCaseNamingStrategy()));
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    })
                .AddFluentValidation();

            AddValidatorsToService(services);

            // Add Database Initializer
            //services.AddScoped<IDbSeeder, FillerDbInitializer>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
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
        }

        private static void AddRepositoriesToServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IPackRepository), typeof(PackRepository));
            services.AddScoped(typeof(IPhraseRepository), typeof(PhraseRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IGamePackRepository), typeof(GamePackRepository));
            services.AddScoped(typeof(IDownloadedPacksInfoRepository), typeof(DownloadedPacksInfoRepository));
            services.AddScoped(typeof(IGamePackRepository), typeof(GamePackRepository));
            services.AddScoped(typeof(IRoundRepository), typeof(RoundRepository));
            services.AddScoped(typeof(IDeviceInfoRepository), typeof(DeviceInfoRepository));
            services.AddScoped(typeof(IGameRepository), typeof(GameRepository));
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
                //app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hat API"));
            
            app.UseStaticFiles();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseAuthentication();

            //ApplyMigrationAndSeeding(app);

            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}"));
        }

        // ReSharper disable once UnusedMember.Local
        private void ApplyMigrationAndSeeding([NotNull] IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var fillerDbContext = serviceScope.ServiceProvider.GetService<FillerDbContext>();

                if (!fillerDbContext.AllMigrationsApplied())
                {
                    fillerDbContext.Database.Migrate();
                    //var fillerDbSeeder = serviceScope.ServiceProvider.GetService<FillerDbSeeder>();
                    //fillerDbSeeder.Seed(fillerDbContext);
                }

                var gameDbContext = serviceScope.ServiceProvider.GetService<GameDbContext>();

                if (!gameDbContext.AllMigrationsApplied())
                {
                    gameDbContext.Database.Migrate();
                }

                //TODO: Починить это!
                //var gameDbSeeder = new GameDbSeeder();
                //gameDbSeeder.Seed(gameDbContext);
            }
        }
    }
}