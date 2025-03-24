using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Servicios.Comercial.Servicios;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using LabCamaronWeb.Servicios.Produccion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Produccion
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosProduccion(this IServiceCollection services)
        {
            services.AddScoped<ISeSiembraService, SeSiembraService>();
            services.AddScoped<ISeAsignacionClienteService, SeAsignacionClienteService>();
            services.AddScoped<ISePlanificacionSiembraService, SePlanificacionSiembraService>();

            return services;
        }
    }
}