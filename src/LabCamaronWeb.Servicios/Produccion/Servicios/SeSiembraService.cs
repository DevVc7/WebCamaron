using LabCamaronWeb.Dto.Produccion.Siembra;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Produccion.Servicios
{
    internal class SeSiembraService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeSiembraService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(SiembraVm.ActualizarSiembra actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<SiembraVm.ActualizarSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarSiembra"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<SiembraVm>> ConsultarPorId(SiembraVm.ConsultarSiembra consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<SiembraVm.ConsultarSiembra, RespuestaConsultaGenericaVm<SiembraVm>>(
                        _configuration["Microservicios:ConsultarSiembraCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<SiembraVm>> ConsultarTodos(SiembraVm.ConsultarTodosSiembra consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<SiembraVm.ConsultarTodosSiembra, RespuestaConsultasGenericaVm<SiembraVm>>(
                        _configuration["Microservicios:ConsultarPlanificacionSiembras"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(SiembraVm.CrearSiembra crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<SiembraVm.CrearSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearSiembra"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(SiembraVm.EliminarSiembra eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<SiembraVm.EliminarSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarSiembra"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}