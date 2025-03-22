using LabCamaronWeb.Infraestructura.Utilidades;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Infraestructura
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarInfraestructura(this IServiceCollection services)
        {
            services.ConfigurarUtilidades();
            return services;
        }
    }
}