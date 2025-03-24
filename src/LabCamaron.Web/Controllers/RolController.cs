using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Configuracion.Rol;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Configuracion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class RolController(ISeRolService seRolService) : BaseController
    {
        private readonly ISeRolService _seRolService = seRolService;

        private readonly RolVm.ConsultarTodosRol _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seRolService
                  .ConsultarTodos(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                var roles = respuestaConsulta.Respuesta.EsExitosa
                  ? respuestaConsulta.Resultados : [];

                if (mostrarMensajeExito)
                {
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("Index", roles);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.Crear)]
        public IActionResult CrearRol()
        {
            var rolNuevo = new RolVm();
            return View("CrearRol", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.Crear)]
        public async Task<IActionResult> CrearRol([FromForm] RolVm.CrearRol crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<RolVm>();
                    return View("CrearRol", crearVm);
                }

                var respuestaCrear = await _seRolService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    return await Index(true);
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var rolVm = crear.Mapear<RolVm>();
                    return View("CrearRol", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.Editar)]
        public async Task<IActionResult> EditarRol(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seRolService
                  .ConsultarPorId(new()
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
                    return View("EditarRol", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new RolVm()
                    {
                        Codigo = codigo
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarRol", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.Editar)]
        public async Task<IActionResult> EditarRol(RolVm.ActualizarRol actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<RolVm>();
                    return View("EditarRol", actualizarVm);
                }

                var respuesta = await _seRolService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seRolService
                      .ConsultarPorId(new()
                      {
                          Codigo = actualizar.Codigo
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarRol", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<RolVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarRol", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarRol([FromForm] RolVm.EliminarRol eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seRolService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seRolService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seRolService
                  .ConsultarTodos(_consultarTodos);

                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                AsignarViewBagMensajeError(respuestaEliminar);
                AsignarViewBagMensajeExito(respuestaEliminar);

                return View("Index", respuestaConsulta.Resultados);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.EditarPermisos)]
        public async Task<IActionResult> EditarPermisos(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seRolService
                  .ConsultarPermisos(new()
                  {
                      Codigo = codigo
                  });

                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                return View("EditarPermisos", respuestaConsulta.Resultado);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuRol.CodigoMenu, MenuRol.Permisos.EditarPermisos)]
        public async Task<IActionResult> EditarPermisos([FromBody] PermisoRolVm.Actualizar actualizar)
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

                var respuestaActualizar = await _seRolService
                  .ActualizarPermisos(actualizar);

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
    }
}