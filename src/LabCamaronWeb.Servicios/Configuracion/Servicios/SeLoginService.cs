using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Configuracion.Servicios
{
    internal class SeLoginService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeLoginService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaAutorizacionAccionVm> AutorizarAccion(DetallePermisoVm.AutorizarAccion autorizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DetallePermisoVm.AutorizarAccion, RespuestaAutorizacionAccionVm>(
                        _configuration["Microservicios:ValidarAutorizacion"]!, autorizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, autorizar);
                return new()
                {
                    Respuesta = RespuestaGenericaVm.Excepcion(),
                };
            }
        }

        public async Task<RespuestaLoginVm> Login(LoginVm login)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicio<LoginVm, RespuestaLoginVm>(_configuration["Microservicios:IniciarSesion"]!, login);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, login);
                return new()
                {
                    Respuesta = RespuestaGenericaVm.Excepcion(),
                };
            }
        }

        public async Task<RespuestaPermisosVm> ObtenerPermisosSesion(DetallePermisoVm.ConsultarPermisos consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<DetallePermisoVm.ConsultarPermisos, RespuestaPermisosVm>(_configuration["Microservicios:ObtenerPermisosSesion"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new()
                {
                    Respuesta = RespuestaGenericaVm.Excepcion(),
                };
            }
        }
    }
}