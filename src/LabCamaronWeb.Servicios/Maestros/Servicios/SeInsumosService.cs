using LabCamaronWeb.Dto.Maestros.Insumos;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeInsumosService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeInsumosService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(InsumosVm.ActualizarInsumos actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<InsumosVm.ActualizarInsumos, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarInsumos"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<InsumosVm>> ConsultarPorId(InsumosVm.ConsultarInsumos consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<InsumosVm.ConsultarInsumos, RespuestaConsultaGenericaVm<InsumosVm>>(
                        _configuration["Microservicios:ConsultarInsumosCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<InsumosVm>> ConsultarTodos(InsumosVm.ConsultarTodosInsumos consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<InsumosVm.ConsultarTodosInsumos, RespuestaConsultasGenericaVm<InsumosVm>>(
                        _configuration["Microservicios:ConsultarInsumos"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(InsumosVm.CrearInsumos crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<InsumosVm.CrearInsumos, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearInsumos"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(InsumosVm.EliminarInsumos eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<InsumosVm.EliminarInsumos, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarInsumos"]!, eliminar);

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