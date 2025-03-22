using LabCamaronWeb.Dto.Maestros.Ente;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.Ente.EnteVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEnteService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEnteService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(ActualizarEnte actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActualizarEnte, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarEnte"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarEnte, RespuestaConsultaGenericaVm<Detallado>>(
                        _configuration["Microservicios:ConsultarEnteCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTodos(ConsultarTodosEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnte, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarEntes"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CrearEnte crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearEnte, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearEnte"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarEnte eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarEnte, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEnte"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Activar(ActivarEnte activar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActivarEnte, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActivarEnte"]!, activar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, activar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarVendedores(ConsultarTodosEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnte, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarVendedores"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTecnicos(ConsultarTodosEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnte, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarTecnicos"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarPersonal(ConsultarTodosEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnte, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarPersonal"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarClientes(ConsultarTodosEnte consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnte, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarClientes"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTodosSinRol(ConsultarTodosEnteSinRol consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosEnteSinRol, RespuestaConsultasGenericaVm<EnteVm>>(
                        _configuration["Microservicios:ConsultarEntesSinRol"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }
    }
}