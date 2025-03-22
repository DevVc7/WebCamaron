using LabCamaronWeb.Servicios.Comercial;
using LabCamaronWeb.Servicios.Configuracion;
using LabCamaronWeb.Servicios.Maestros;
using LabCamaronWeb.Servicios.Parametrizacion;
using LabCamaronWeb.Servicios.Produccion;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServicios(this IServiceCollection services)
        {
            services.RegistrarServiciosConfiguracion();
            services.RegistrarServiciosParametrizacion();
            services.RegistrarServiciosMaestros();
            services.RegistrarServiciosProduccion();
            services.RegistrarServiciosComercial();

            return services;
        }
    }
}