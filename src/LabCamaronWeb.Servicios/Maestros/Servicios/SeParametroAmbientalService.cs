using LabCamaronWeb.Dto.Maestros.ParametroAmbiental;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeParametroAmbientalService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeParametroAmbientalService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(ParametroAmbientalVm.ActualizarParametroAmbiental actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ParametroAmbientalVm.ActualizarParametroAmbiental, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarParametroAmbiental"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<ParametroAmbientalVm>> ConsultarPorId(ParametroAmbientalVm.ConsultarParametroAmbiental consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ParametroAmbientalVm.ConsultarParametroAmbiental, RespuestaConsultaGenericaVm<ParametroAmbientalVm>>(
                        _configuration["Microservicios:ConsultarParametroAmbientalCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<ParametroAmbientalVm>> ConsultarTodos(ParametroAmbientalVm.ConsultarTodosParametroAmbiental consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ParametroAmbientalVm.ConsultarTodosParametroAmbiental, RespuestaConsultasGenericaVm<ParametroAmbientalVm>>(
                        _configuration["Microservicios:ConsultarParametrosAmbientales"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(ParametroAmbientalVm.CrearParametroAmbiental crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ParametroAmbientalVm.CrearParametroAmbiental, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearParametroAmbiental"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(ParametroAmbientalVm.EliminarParametroAmbiental eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ParametroAmbientalVm.EliminarParametroAmbiental, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarParametroAmbiental"]!, eliminar);

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