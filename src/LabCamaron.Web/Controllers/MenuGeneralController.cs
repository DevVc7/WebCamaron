using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Dto.Configuracion.Usuario;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Configuracion;
using LabCamaronWeb.Infraestructura.Extensiones;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class MenuGeneralController(ISeUsuarioService seUsuarioService) : BaseController
    {
        private readonly ISeUsuarioService _seUsuarioService = seUsuarioService;

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuGeneral.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> EditarUsuario(string codigo)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    return View("EditarUsuario", new UsuarioVm());
                }

                // Ejecutamos el servicio
                var respuestaConsulta = await _seUsuarioService
                  .ConsultarUsuario(new()
                  {
                      Codigo = codigo,
                  });

                /// Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                var retorno = respuestaConsulta.Respuesta.EsExitosa
                  ? respuestaConsulta.Resultado
                  : new UsuarioVm();

                AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("EditarUsuario", retorno);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuGeneral.CodigoMenu, MenuGeneral.Permisos.ActualizarDatos)]
        public async Task<IActionResult> EditarUsuario([FromForm] UsuarioVm.ActualizarUsuario actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    return View("EditarUsuario", new UsuarioVm());
                }

                var respuestaProceso = await _seUsuarioService
                  .ActualizarUsuario(actualizar);

                /// Procesa errores relacioados al problemas de comunicación
                if (respuestaProceso.TieneErrorServicio)
                {
                    return ProcesarError(respuestaProceso);
                }

                // Continuamos con el proceso
                if (respuestaProceso.EsExitosa)
                {
                    var respuestaConsulta = await _seUsuarioService
                      .ConsultarUsuario(new()
                      {
                          Codigo = actualizar.Codigo,
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuestaConsulta.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsulta.Respuesta);
                    }

                    var retorno = respuestaConsulta.Respuesta.EsExitosa
                      ? respuestaConsulta.Resultado
                      : new UsuarioVm();

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta);
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);

                    // Procesamos el error en base a la respuesta
                    if (respuestaConsulta.Respuesta.EsExitosa)
                    {
                        ActualizarUsuarioSesion(respuestaConsulta.Resultado!);
                    }

                    return View("EditarUsuario", retorno);
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaProceso.Mensaje);
                    return View("EditarUsuario", new UsuarioVm());
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuGeneral.CodigoMenu, MenuGeneral.Permisos.CambiarContraseña)]
        public async Task<IActionResult> EditarCredenciales([FromForm] UsuarioVm.ActualizarClaveUsuario actualizarClave)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    return View("EditarCredenciales", new UsuarioVm());
                }

                // Ejecutamos el servicio
                var respuestaClave = await _seUsuarioService
                  .ActualizarClaveUsuario(actualizarClave);

                if (respuestaClave.EsExitosa)
                {
                    this.TempData[LoginConstantes.TipoMensajeLogin] = LoginConstantes.TipoCambioClave;
                    return RedirectToAction("CerrarSesion", "Login");
                }
                else
                {
                    // Procesa errores relacioados al problemas de comunicación
                    if (respuestaClave.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaClave);
                    }

                    // Ejecutamos el servicio
                    var respuestaConsulta = await _seUsuarioService
                      .ConsultarUsuario(new()
                      {
                          Codigo = actualizarClave.Codigo,
                      });

                    var retorno = respuestaConsulta.Respuesta.EsExitosa
                      ? respuestaConsulta.Resultado
                      : new UsuarioVm();

                    AsignarViewBagMensajeError(respuestaClave.Mensaje);

                    return View("EditarUsuario", retorno);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        private void ActualizarUsuarioSesion(UsuarioVm usuario)
        {
            // Eliminar un objeto específico de la sesión
            HttpContext.Session.Remove(SesionConstantes.Usuario);

            // Agregar un objeto a la sesión
            HttpContext.Session.Agregar(SesionConstantes.Usuario, new PermisoUsuarioVm.UsuarioVm()
            {
                Codigo = usuario.Codigo,
                Descripcion = usuario.Descripcion,
                FechaValidezContrasenia = usuario.FechaValidezContrasenia,
                RequiereNuevaContraseña = usuario.RequiereNuevaContraseña,
            });
        }
    }
}