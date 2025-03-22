using LabCamaronWeb.Dto.Parametrizacion.Ciudad;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeCiudadService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeCiudadService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaConsultaGenericaVm<CiudadVm>> ConsultarPorId(CiudadVm.ConsultarCiudad consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CiudadVm.ConsultarCiudad, RespuestaConsultaGenericaVm<CiudadVm>>(
                        _configuration["Microservicios:ConsultarCiudadCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<CiudadVm>> ConsultarTodos(CiudadVm.ConsultarTodosCiudad consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CiudadVm.ConsultarTodosCiudad, RespuestaConsultasGenericaVm<CiudadVm>>(
                        _configuration["Microservicios:ConsultarCiudades"]!, consultar);

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