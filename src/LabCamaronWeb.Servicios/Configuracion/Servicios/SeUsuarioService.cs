using LabCamaronWeb.Dto.Configuracion.Usuario;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Configuracion.Servicios
{
    internal class SeUsuarioService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeUsuarioService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> ActualizarClaveUsuario(UsuarioVm.ActualizarClaveUsuario actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ActualizarClaveUsuario, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarClaveUsuario"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> ActualizarUsuario(UsuarioVm.ActualizarUsuario actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ActualizarUsuario, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarUsuario"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<UsuarioVm>> ConsultarUsuario(UsuarioVm.ConsultarUsuario consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ConsultarUsuario, RespuestaConsultaGenericaVm<UsuarioVm>>(
                        _configuration["Microservicios:ConsultarUsuario"]!, consultar);

                return respuesta!;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<UsuarioVm>> ConsultarUsuarios(UsuarioVm.ConsultarTodos consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ConsultarTodos, RespuestaConsultasGenericaVm<UsuarioVm>>(
                        _configuration["Microservicios:ConsultarUsuarios"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> CrearUsuario(UsuarioVm.CrearUsuario crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.CrearUsuario, RespuestaGenericaVm>(_configuration["Microservicios:CrearUsuario"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> ReestablecerClaveUsuario(UsuarioVm.ReestablecerContrasenia actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ReestablecerContrasenia, RespuestaGenericaVm>(
                        _configuration["Microservicios:ReestablecerClaveUsuario"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> ActualizarRolesUsuario(UsuarioVm.ActualizarRol actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ActualizarRol, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarRolUsuario"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> EliminarUsuario(UsuarioVm.EliminarUsuario eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.EliminarUsuario, RespuestaGenericaVm>(_configuration["Microservicios:EliminarUsuario"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> ActualizarModulosLaboratorioUsuario(UsuarioVm.ActualizarModuloLaboratorio actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<UsuarioVm.ActualizarModuloLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarModuloLaboratorioUsuario"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}