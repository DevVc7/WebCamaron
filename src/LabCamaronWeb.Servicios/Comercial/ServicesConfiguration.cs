using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Servicios.Comercial.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Comercial
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosComercial(this IServiceCollection services)
        {
            services.AddScoped<ISePlanificacionSiembraService, SePlanificacionSiembraService>();
            services.AddScoped<ISeAsignacionClienteService, SeAsignacionClienteService>();
            services.AddScoped<ISeAprobarDespachoService, SeAprobarDespachoService>();
            services.AddScoped<ISeModuloCesionService, SeModuloCesionService>();
            return services;
        }
    }
}