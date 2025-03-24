using LabCamaronWeb.Dto.Comercial.AprobarDespacho;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Comercial.Servicios
{
    public class SeAprobarDespachoService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeAprobarDespachoService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;


        public async Task<RespuestaConsultaGenericaVm<AprobarDespachoVm>> ConsultarPorId(AprobarDespachoVm.ConsultarAprobarDespacho consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AprobarDespachoVm.ConsultarAprobarDespacho, RespuestaConsultaGenericaVm<AprobarDespachoVm>>(
                        _configuration["Microservicios:ConsultarAprobarDespacho"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<AprobarDespachoVm>> ConsultarTodos(AprobarDespachoVm.ConsultarTodosAprobarDespacho consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AprobarDespachoVm.ConsultarTodosAprobarDespacho, RespuestaConsultasGenericaVm<AprobarDespachoVm>>(
                        _configuration["Microservicios:ConsultarAprobarDespachos"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(AprobarDespachoVm.CrearAprobarDespacho crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AprobarDespachoVm.CrearAprobarDespacho, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearAprobarDespacho"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}