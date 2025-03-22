using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using LabCamaronWeb.Servicios.Configuracion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Configuracion
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosConfiguracion(this IServiceCollection services)
        {
            services.AddScoped<ISeLoginService, SeLoginService>();
            services.AddScoped<ISeUsuarioService, SeUsuarioService>();
            services.AddScoped<ISeRolService, SeRolService>();

            return services;
        }
    }
}