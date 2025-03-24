using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using LabCamaronWeb.Servicios.Parametrizacion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Parametrizacion
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosParametrizacion(this IServiceCollection services)
        {
            services.AddScoped<ISeEmpresaService, SeEmpresaService>();
            services.AddScoped<ISeLaboratorioService, SeLaboratorioService>();
            services.AddScoped<ISeModuloLaboratorioService, SeModuloLaboratorioService>();
            services.AddScoped<ISeTanqueService, SeTanqueService>();

            services.AddScoped<ISeProvinciaService, SeProvinciaService>();
            services.AddScoped<ISeCiudadService, SeCiudadService>();
            services.AddScoped<ISeUnidadMedidaService, SeUnidadMedidaService>();

            return services;
        }
    }
}