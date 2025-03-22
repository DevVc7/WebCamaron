using LabCamaronWeb.Dto.Maestros.EstadioLarva;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEstadioLarvaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEstadioLarvaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(EstadioLarvaVm.ActualizarEstadioLarva actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EstadioLarvaVm.ActualizarEstadioLarva, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarEstadioLarva"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<EstadioLarvaVm>> ConsultarPorId(EstadioLarvaVm.ConsultarEstadioLarva consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EstadioLarvaVm.ConsultarEstadioLarva, RespuestaConsultaGenericaVm<EstadioLarvaVm>>(
                        _configuration["Microservicios:ConsultarEstadioLarvaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EstadioLarvaVm>> ConsultarTodos(EstadioLarvaVm.ConsultarTodosEstadioLarva consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EstadioLarvaVm.ConsultarTodosEstadioLarva, RespuestaConsultasGenericaVm<EstadioLarvaVm>>(
                        _configuration["Microservicios:ConsultarEstadioLarva"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(EstadioLarvaVm.CrearEstadioLarva crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EstadioLarvaVm.CrearEstadioLarva, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearEstadioLarva"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EstadioLarvaVm.EliminarEstadioLarva eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EstadioLarvaVm.EliminarEstadioLarva, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEstadioLarva"]!, eliminar);

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