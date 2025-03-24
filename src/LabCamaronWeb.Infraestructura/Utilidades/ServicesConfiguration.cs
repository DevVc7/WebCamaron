using LabCamaronWeb.Infraestructura.Utilidades.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Infraestructura.Utilidades
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigurarUtilidades(this IServiceCollection services)
        {
            services.AddScoped<IOperacionHttpServicio, OperacionHttpServicio>();
            return services;
        }
    }
}