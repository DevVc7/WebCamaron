using LabCamaronWeb.Dto.Parametrizacion.UnidadMedida;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeUnidadMedidaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeUnidadMedidaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(UnidadMedidaVm.ActualizarUnidadMedida actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UnidadMedidaVm.ActualizarUnidadMedida, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarUnidadMedida"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<UnidadMedidaVm>> ConsultarPorId(UnidadMedidaVm.ConsultarUnidadMedida consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UnidadMedidaVm.ConsultarUnidadMedida, RespuestaConsultaGenericaVm<UnidadMedidaVm>>(
                        _configuration["Microservicios:ConsultarUnidadMedidaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<TipoUnidadMedidaVm>> ConsultarTiposUnidadMedida()
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RespuestaConsultasGenericaVm<TipoUnidadMedidaVm>>(
                        _configuration["Microservicios:ConsultarTiposUnidadesMedida"]!);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<UnidadMedidaVm>> ConsultarTodos(UnidadMedidaVm.ConsultarTodosUnidadMedida consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UnidadMedidaVm.ConsultarTodosUnidadMedida, RespuestaConsultasGenericaVm<UnidadMedidaVm>>(
                        _configuration["Microservicios:ConsultarUnidadesMedida"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(UnidadMedidaVm.CrearUnidadMedida crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UnidadMedidaVm.CrearUnidadMedida, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearUnidadMedida"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(UnidadMedidaVm.EliminarUnidadMedida eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UnidadMedidaVm.EliminarUnidadMedida, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarUnidadMedida"]!, eliminar);

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