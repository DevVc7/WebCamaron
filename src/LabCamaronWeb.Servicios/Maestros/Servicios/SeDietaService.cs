using LabCamaronWeb.Dto.Maestros.Dieta;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeDietaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeDietaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(DietaVm.ActualizarDieta actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DietaVm.ActualizarDieta, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarDieta"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<DietaVm.Detallado>> ConsultarPorId(DietaVm.ConsultarDieta consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DietaVm.ConsultarDieta, RespuestaConsultaGenericaVm<DietaVm.Detallado>>(
                        _configuration["Microservicios:ConsultarDieta"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<DietaVm>> ConsultarTodos(DietaVm.ConsultarDietas consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DietaVm.ConsultarDietas, RespuestaConsultasGenericaVm<DietaVm>>(
                        _configuration["Microservicios:ConsultarDietas"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(DietaVm.CrearDieta crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DietaVm.CrearDieta, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearDieta"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(DietaVm.EliminarDieta eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DietaVm.EliminarDieta, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarDieta"]!, eliminar);

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