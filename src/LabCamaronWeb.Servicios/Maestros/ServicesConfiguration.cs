using LabCamaronWeb.Servicios.Maestros.Interfaces;
using LabCamaronWeb.Servicios.Maestros.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace LabCamaronWeb.Servicios.Maestros
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegistrarServiciosMaestros(this IServiceCollection services)
        {
            services.AddScoped<ISeParametroAmbientalService, SeParametroAmbientalService>();
            services.AddScoped<ISeCategoriaService, SeCategoriaService>();
            services.AddScoped<ISeCargoService, SeCargoService>();
            services.AddScoped<ISeMarcaService, SeMarcaService>();
            services.AddScoped<ISeColorService, SeColorService>();
            services.AddScoped<ISeFuncionTecnicoService, SeFuncionTecnicoService>();
            services.AddScoped<ISeEstadioLarvaService, SeEstadioLarvaService>();
            services.AddScoped<ISeCupoVendedorService, SeCupoVendedorService>();
            services.AddScoped<ISeHorasService, SeHorasService>();
            services.AddScoped<ISeInsumosService, SeInsumosService>();
            services.AddScoped<ISeDietaService, SeDietaService>();
            services.AddScoped<ISeClienteService, SeClienteService>();
            services.AddScoped<ISePersonalService, SePersonalService>();

            return services;
        }
    }
}