using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Configuracion.Usuario;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Configuracion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LabCamaronWeb.Dto.Configuracion.Usuario.UsuarioVm;

namespace LabCamaron.Web.Controllers
{
    public class UsuarioController(ISeUsuarioService seUsuarioService) : BaseController
    {
        private readonly ISeUsuarioService _seUsuarioService = seUsuarioService;

        private readonly ConsultarTodos _consultarTodos = new()
        {
            SoloActivos = false,
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los personas activos
                var respuestaConsulta = await _seUsuarioService
                  .ConsultarUsuarios(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                var personas = respuestaConsulta.Respuesta.EsExitosa
                  ? respuestaConsulta.Resultados : [];

                if (mostrarMensajeExito)
                {
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("Index", personas);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.Crear)]
        public IActionResult CrearUsuario()
        {
            var personaNuevo = new UsuarioVm();
            return View("CrearUsuario", personaNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.Crear)]
        public async Task<IActionResult> CrearUsuario([FromForm] CrearUsuario crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<UsuarioVm>();
                    return View("CrearUsuario", crearVm);
                }

                var respuestaCrear = await _seUsuarioService.CrearUsuario(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    return await EditarUsuario(crear.Codigo);
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var personasVm = crear.Mapear<UsuarioVm>();
                    return View("CrearUsuario", personasVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.Editar)]
        public async Task<IActionResult> EditarUsuario(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seUsuarioService
                  .ConsultarUsuario(new()
                  {
                      Codigo = codigo
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarUsuario", respuestaConsulta.Resultado);
                }
                else
                {
                    var personasVm = new UsuarioVm()
                    {
                        Codigo = codigo
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarUsuario", personasVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.Editar)]
        public async Task<IActionResult> EditarUsuario(ActualizarUsuario actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<UsuarioVm>();
                    return View("EditarUsuario", actualizarVm);
                }

                var respuesta = await _seUsuarioService
                  .ActualizarUsuario(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seUsuarioService
                      .ConsultarUsuario(new()
                      {
                          Codigo = actualizar.Codigo,
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarUsuario", respuestaConsulta.Resultado);
                }
                else
                {
                    var personasVm = actualizar.Mapear<UsuarioVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarUsuario", personasVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.ReestablecerContrasenia)]
        public async Task<IActionResult> ReestablecerContrasenia(ReestablecerContrasenia reestablecer)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = reestablecer.Mapear<UsuarioVm>();
                    return View("EditarUsuario", actualizarVm);
                }

                var respuesta = await _seUsuarioService
                  .ReestablecerClaveUsuario(reestablecer);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seUsuarioService
                      .ConsultarUsuario(new()
                      {
                          Codigo = reestablecer.Codigo,
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarUsuario", respuestaConsulta.Resultado);
                }
                else
                {
                    var personasVm = reestablecer.Mapear<UsuarioVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarUsuario", personasVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.EditarRoles)]
        public async Task<IActionResult> EditarRolesUsuario([FromBody] ActualizarRol actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    return Json(new
                    {
                        EsError = true,
                        EsFatal = false,
                        Mensaje = ViewBag.MensajeError
                    });
                }

                var respuestaActualizar = await _seUsuarioService
                  .ActualizarRolesUsuario(actualizar);

                if (respuestaActualizar.TieneErrorServicio)
                {
                    return Json(new
                    {
                        EsError = false,
                        EsFatal = true,
                        respuestaActualizar.Mensaje,
                    });
                }

                // Si se proceso normalmente
                return Json(new
                {
                    EsError = !respuestaActualizar.EsExitosa,
                    EsFatal = false,
                    respuestaActualizar.Mensaje,
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    EsError = true,
                    EsFatal = false,
                    Mensaje = "Ha ocurrido una excepción",
                });
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.EditarModulos)]
        public async Task<IActionResult> EditarModuloLaboratorioUsuario([FromBody] ActualizarModuloLaboratorio actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    return Json(new
                    {
                        EsError = true,
                        EsFatal = false,
                        Mensaje = ViewBag.MensajeError
                    });
                }

                var respuestaActualizar = await _seUsuarioService
                  .ActualizarModulosLaboratorioUsuario(actualizar);

                if (respuestaActualizar.TieneErrorServicio)
                {
                    return Json(new
                    {
                        EsError = false,
                        EsFatal = true,
                        respuestaActualizar.Mensaje,
                    });
                }

                // Si se proceso normalmente
                return Json(new
                {
                    EsError = !respuestaActualizar.EsExitosa,
                    EsFatal = false,
                    respuestaActualizar.Mensaje,
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    EsError = true,
                    EsFatal = false,
                    Mensaje = "Ha ocurrido una excepción",
                });
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUsuario.CodigoMenu, MenuUsuario.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarUsuario(EliminarUsuario eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    return await Index(true);
                }

                var respuesta = await _seUsuarioService
                  .EliminarUsuario(eliminar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    return await Index(true);
                }
                else
                {
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return await Index(true);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}