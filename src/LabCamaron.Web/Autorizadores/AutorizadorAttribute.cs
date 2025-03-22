using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Extensiones;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabCamaron.Web.Autorizadores
{
    [AttributeUsage(AttributeTargets.All)]
    public class AutorizadorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "CerrarSesion"
                }));
            }
            else
            {
                var identificadorSesion = context.HttpContext.Session.Obtener<string>(SesionConstantes.IdentificadorSesion);
                if (string.IsNullOrEmpty(identificadorSesion))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "CerrarSesion"
                    }));
                    return;
                }

                var seLoginService = context.HttpContext.RequestServices.GetRequiredService<ISeLoginService>();

                var respuestasPermisos = seLoginService
                  .ObtenerPermisosSesion(new()
                  {
                      IdentificadorSesion = identificadorSesion
                  })
                  .GetAwaiter()
                  .GetResult();

                if (!respuestasPermisos.Respuesta.EsExitosa)
                {
                    ProcesarError(context, respuestasPermisos);
                    return;
                }

                context.HttpContext.Session.Eliminar(SesionConstantes.Modulos);
                context.HttpContext.Session.Eliminar(SesionConstantes.Permisos);

                context.HttpContext.Session.Agregar(SesionConstantes.Modulos, respuestasPermisos.PermisoUsuario!.Modulos);
                context.HttpContext.Session.Agregar(SesionConstantes.Permisos, respuestasPermisos.PermisoUsuario!.Detalles);
            }
        }

        private static void ProcesarError(AuthorizationFilterContext context, RespuestaPermisosVm consultaAutorizacion)
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
            else if (consultaAutorizacion.Respuesta.Codigo == Servidor.CodigoSesionInvalida)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "CerrarSesion",
                    controller = "Login",
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