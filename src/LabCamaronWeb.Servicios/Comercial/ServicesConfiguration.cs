using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Servicios.Comercial.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Comercial
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosComercial(this IServiceCollection services)
        {
            services.AddScoped<IAsignacionClienteService, AsignacionClienteService>();

            return services;
        }
    }
}