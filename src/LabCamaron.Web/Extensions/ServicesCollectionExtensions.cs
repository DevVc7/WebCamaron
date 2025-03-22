using LabCamaronWeb.Infraestructura.Constantes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

namespace LabCamaron.Web.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AgregarAutenticacion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["JwtToken"];
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        }
                    };
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["UrlMicroservicio"]!; // Reemplaza con la URL de tu API
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"]!, // Reemplaza con el emisor del token
                        ValidAudience = configuration["JWT:ValidAudience"]!, // Reemplaza con el público del token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)) // Reemplaza con tu clave secreta
                    };
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Configura las opciones de la sesión, como el tiempo de expiración
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddAuthorizationBuilder()
                .AddPolicy("MyPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IServiceCollection AgregarFactoryHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(InformacionGateway.Nombre)
                .ConfigureHttpClient((client) =>
                {
                    client.BaseAddress = new Uri(configuration["UrlMicroservicio"]!);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                });

            return services;
        }

        public static IServiceCollection ConfigurarSerilog(
            this IServiceCollection servicesollection, WebApplicationBuilder builder)
        {
            var urlSeq = builder.Configuration["SeqUrl"]!;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.LifeTime", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.EntityFrameWork.Database", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("NombreAplicacion", InformacionApi.Nombre)
                .Enrich.WithCorrelationId()
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.Async(s => s.Seq(urlSeq));

            Log.Logger = logger.CreateLogger();
            builder.Host.UseSerilog(Log.Logger);
            servicesollection.AddSingleton(Log.Logger);

            var messageTemplate = $"{InformacionApi.Nombre} Iniciada!!";
            Log.Logger.Information(messageTemplate);

            return servicesollection;
        }
    }
}