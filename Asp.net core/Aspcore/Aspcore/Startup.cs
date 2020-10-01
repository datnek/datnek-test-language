using System.Text;
using Aspcore.Migrations;
using Aspcore.Services;
using Aspcore.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace Aspcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // start 
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ILanguagesService, LanguagesService>();

            // services.Configure<AppSettings>(appSettingsSection);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder


                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin()
                    .WithOrigins(
                                     "http://localhost:4200",
                                     "https://localhost:4200"
                                    );
            }));


        

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });




            // services.AddDbContext<FdContext>(ServiceLifetime.Transient);
            services.AddDbContext<DatnekContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);


            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            if (appSettings != null)
            {
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                        /*  ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidIssuer = "https://localhost:44350",
                          ValidAudience = "https://localhost:44350",
                          */
                    };
                });
            }



            services.AddControllersWithViews()
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .AddNewtonsoftJson(options =>
           options.SerializerSettings.ContractResolver =
             new DefaultContractResolver());

            // end

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseDeveloperExceptionPage();
                 app.UseExceptionHandler("/Home/Error");
                 app.UseHsts();
            }


            // using Microsoft.AspNetCore.HttpOverrides;

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseWebSockets();


            /*
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });
             app.UseMvc();
            */


            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
