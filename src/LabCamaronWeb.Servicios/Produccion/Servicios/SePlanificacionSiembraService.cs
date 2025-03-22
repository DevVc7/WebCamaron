using LabCamaronWeb.Dto.Produccion.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Produccion.Servicios
{
    internal class SePlanificacionSiembraService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISePlanificacionSiembraService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(PlanificacionSiembraVm.ActualizarPlanificacionSiembra actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PlanificacionSiembraVm.ActualizarPlanificacionSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarPlanificacionSiembra"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<PlanificacionSiembraVm>> ConsultarPorId(PlanificacionSiembraVm.ConsultarPlanificacionSiembra consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PlanificacionSiembraVm.ConsultarPlanificacionSiembra, RespuestaConsultaGenericaVm<PlanificacionSiembraVm>>(
                        _configuration["Microservicios:ConsultarPlanificacionSiembraCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<PlanificacionSiembraVm>> ConsultarTodos(PlanificacionSiembraVm.ConsultarTodosPlanificacionSiembra consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PlanificacionSiembraVm.ConsultarTodosPlanificacionSiembra, RespuestaConsultasGenericaVm<PlanificacionSiembraVm>>(
                        _configuration["Microservicios:ConsultarPlanificacionSiembra"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(PlanificacionSiembraVm.CrearPlanificacionSiembra crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PlanificacionSiembraVm.CrearPlanificacionSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearPlanificacionSiembra"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(PlanificacionSiembraVm.EliminarPlanificacionSiembra eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PlanificacionSiembraVm.EliminarPlanificacionSiembra, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarPlanificacionSiembra"]!, eliminar);

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