using LabCamaronWeb.Dto.Parametrizacion.Tanque;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeTanqueService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeTanqueService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(TanqueVm.ActualizarTanque actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<TanqueVm.ActualizarTanque, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarTanque"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<TanqueVm>> ConsultarPorId(TanqueVm.ConsultarTanque consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<TanqueVm.ConsultarTanque, RespuestaConsultaGenericaVm<TanqueVm>>(
                        _configuration["Microservicios:ConsultarTanqueCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<TanqueVm>> ConsultarTodos(TanqueVm.ConsultarTodosTanque consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<TanqueVm.ConsultarTodosTanque, RespuestaConsultasGenericaVm<TanqueVm>>(
                        _configuration["Microservicios:ConsultarTanques"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(TanqueVm.CrearTanque crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<TanqueVm.CrearTanque, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearTanque"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(TanqueVm.EliminarTanque eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<TanqueVm.EliminarTanque, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarTanque"]!, eliminar);

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