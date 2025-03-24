using LabCamaronWeb.Dto.Configuracion.Rol;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Configuracion.Servicios
{
    internal class SeRolService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeRolService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(RolVm.ActualizarRol actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RolVm.ActualizarRol, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarRol"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> ActualizarPermisos(PermisoRolVm.Actualizar actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PermisoRolVm.Actualizar, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarPermisoRol"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<PermisoRolVm>> ConsultarPermisos(PermisoRolVm.Consultar consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<PermisoRolVm.Consultar, RespuestaConsultaGenericaVm<PermisoRolVm>>(
                        _configuration["Microservicios:ConsultarPermisoRol"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultaGenericaVm<RolVm>> ConsultarPorId(RolVm.ConsultarRol consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RolVm.ConsultarRol, RespuestaConsultaGenericaVm<RolVm>>(
                        _configuration["Microservicios:ConsultarRolCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<RolVm>> ConsultarTodos(RolVm.ConsultarTodosRol consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RolVm.ConsultarTodosRol, RespuestaConsultasGenericaVm<RolVm>>(
                        _configuration["Microservicios:ConsultarRoles"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(RolVm.CrearRol crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RolVm.CrearRol, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearRol"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(RolVm.EliminarRol eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<RolVm.EliminarRol, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarRol"]!, eliminar);

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