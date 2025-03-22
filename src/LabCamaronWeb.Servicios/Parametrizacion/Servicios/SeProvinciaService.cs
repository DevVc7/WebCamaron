using LabCamaronWeb.Dto.Parametrizacion.Provincia;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeProvinciaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeProvinciaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaConsultaGenericaVm<ProvinciaVm>> ConsultarPorId(ProvinciaVm.ConsultarProvincia consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ProvinciaVm.ConsultarProvincia, RespuestaConsultaGenericaVm<ProvinciaVm>>(
                        _configuration["Microservicios:ConsultarProvinciaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<ProvinciaVm>> ConsultarTodos(ProvinciaVm.ConsultarTodosProvincia consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ProvinciaVm.ConsultarTodosProvincia, RespuestaConsultasGenericaVm<ProvinciaVm>>(
                        _configuration["Microservicios:ConsultarProvincias"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }
    }
}