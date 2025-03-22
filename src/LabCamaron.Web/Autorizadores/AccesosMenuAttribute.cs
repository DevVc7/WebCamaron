using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Extensiones;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabCamaron.Web.Autorizadores
{
    [AttributeUsage(AttributeTargets.All)]
    public class AccesosMenuAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool Validar;
        private readonly string CodigoMenu;
        private readonly string Permiso;

        public AccesosMenuAttribute(string codigoMenu, string permiso)
        {
            this.CodigoMenu = codigoMenu;
            this.Permiso = permiso;
            Validar = true;
        }

        public AccesosMenuAttribute()
        {
            Permiso = string.Empty;
            CodigoMenu = string.Empty;
            Validar = false;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var usuarioAutenticado = context.HttpContext!.Session.Obtener<PermisoUsuarioVm.UsuarioVm>(SesionConstantes.Usuario);
                var seLoginService = context.HttpContext.RequestServices.GetRequiredService<ISeLoginService>();
                var configuraciones = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                if (Validar)
                {
                    var identificadorSesion = context.HttpContext.Session.Obtener<string>(SesionConstantes.IdentificadorSesion)!;
                    var consultaAutorizacion = seLoginService
                      .AutorizarAccion(new()
                      {
                          CodigoMenu = CodigoMenu,
                          CodigoPermiso = Permiso,
                          IdentificadorSesion = identificadorSesion,
                      })
                      .GetAwaiter()
                      .GetResult();

                    if (!consultaAutorizacion.Respuesta.EsExitosa)
                    {
                        ProcesarError(context, consultaAutorizacion);
                        return;
                    }

                    // Si el usuario no tiene autorización, redirigimos a la página de error
                    if (!consultaAutorizacion.TieneAutorizacion)
                    {
                        var values = new RouteValueDictionary(new
                        {
                            action = "ErrorAutorizacion",
                            controller = "Home",
                        });
                        context.Result = new RedirectToRouteResult(values);
                    }
                }
            }
            catch (Exception)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "ErrorComun",
                    controller = "Home",
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        private static void ProcesarError(AuthorizationFilterContext context, RespuestaAutorizacionAccionVm consultaAutorizacion)
        {
            // Si no fue posible consultar los roles del usuario, retornamos
            if (consultaAutorizacion.Respuesta.Codigo == Servidor.CodigoMetodoNoAutorizado)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "ErrorAutorizacion",
                    controller = "Home",
                });
                context.Result = new RedirectToRouteResult(values);
            }
            else if (consultaAutorizacion.Respuesta.Codigo == Servidor.CodigoServicioNoDisponible)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "ErrorMantenimiento",
                    controller = "Home",
                });
                context.Result = new RedirectToRouteResult(values);
            }
            else
            {
                var values = new RouteValueDictionary(new
                {
                    action = "ErrorComun",
                    controller = "Home",
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }
    }
}