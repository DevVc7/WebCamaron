using LabCamaronWeb.Dto.Maestros.Horas;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeHorasService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeHorasService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(HorasVm.ActualizarHoras actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<HorasVm.ActualizarHoras, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarHoras"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<HorasVm>> ConsultarPorId(HorasVm.ConsultarHoras consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<HorasVm.ConsultarHoras, RespuestaConsultaGenericaVm<HorasVm>>(
                        _configuration["Microservicios:ConsultarHorasCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<HorasVm>> ConsultarTodos(HorasVm.ConsultarTodosHoras consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<HorasVm.ConsultarTodosHoras, RespuestaConsultasGenericaVm<HorasVm>>(
                        _configuration["Microservicios:ConsultarHoras"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(HorasVm.CrearHoras crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<HorasVm.CrearHoras, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearHoras"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(HorasVm.EliminarHoras eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<HorasVm.EliminarHoras, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarHoras"]!, eliminar);

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