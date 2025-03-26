using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Comercial.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers.Comercial
{
    public class PlanificacionSiembraController(ISePlanificacionSiembraService sePlanificacionSiembraService) : BaseController
    {
        private readonly ISePlanificacionSiembraService _sePlanificacionSiembraService = sePlanificacionSiembraService;

        private readonly PlanificacionSiembraVm.ConsultarTodosPlanificacionSiembra _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _sePlanificacionSiembraService
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

                //AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("Views/Comercial/PlanificacionSiembra/Index.cshtml", roles);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Crear)]
        public IActionResult CrearPlanificacionSiembra()
        {
            var rolNuevo = new PlanificacionSiembraVm();
            return View("Views/Comercial/PlanificacionSiembra/CrearPlanificacionSiembra.cshtml", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Crear)]
        public async Task<IActionResult> CrearPlanificacionSiembra([FromBody] PlanificacionSiembraVm.CrearPlanificacionSiembra crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<PlanificacionSiembraVm>();
                    var textos = ModelState.SelectMany(e => e.Value!.Errors.Select(x => x.ErrorMessage));
                    return Json(new { success = false, message = textos });
                    return View("Views/Comercial/PlanificacionSiembra/CrearPlanificacionSiembra.cshtml", crearVm);
                }

                var respuestaCrear = await _sePlanificacionSiembraService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return Json(new { success = false, message = respuestaCrear.Mensaje });
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    AsignarViewBagMensajeExito(respuestaCrear.Mensaje);
                    return Json(new { success = true, message = respuestaCrear.Mensaje });
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);
                    return Json(new { success = false, message = respuestaCrear.Mensaje });
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Crear)]
        public IActionResult AgregarDetallePlanificacionSiembra()
        {
            var detalle = new PlanificacionSiembraDetalleVm() { FechaCosecha = DateTime.Now, FechaSiembra = DateTime.Now.AddDays(20) };
            return PartialView("Views/Comercial/PlanificacionSiembra/ModalDetallesPartialView.cshtml", detalle);
        }


        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Editar)]
        public async Task<IActionResult> EditarPlanificacionSiembra(PlanificacionSiembraVm.ConsultarPlanificacionSiembraId consultar)
        {
            try
            {
                var respuestaConsulta = await _sePlanificacionSiembraService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("Views/Comercial/PlanificacionSiembra/EditarPlanificacionSiembra.cshtml", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new PlanificacionSiembraVm()
                    {
                        Id = consultar.Id ?? 0,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("Views/Comercial/PlanificacionSiembra/EditarPlanificacionSiembra.cshtml", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Editar)]
        public async Task<IActionResult> EditarPlanificacionSiembra([FromBody] PlanificacionSiembraVm.ActualizarPlanificacionSiembra actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<PlanificacionSiembraVm>();
                    var textos = ModelState.SelectMany(e => e.Value!.Errors.Select(x => x.ErrorMessage));
                    return Json(new { success = false, message = textos });
                }

                var respuesta = await _sePlanificacionSiembraService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _sePlanificacionSiembraService
                      .ConsultarPorId(new()
                      {
                          Id = actualizar.Id
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return Json(new { success = true, message = respuesta.Mensaje });
                }
                else
                {
                    var rolVm = actualizar.Mapear<PlanificacionSiembraVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return Json(new { success = false, message = respuesta.Mensaje });
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarPlanificacionSiembra([FromForm] PlanificacionSiembraVm.EliminarPlanificacionSiembra eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _sePlanificacionSiembraService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Views/Comercial/PlanificacionSiembra/Index.cshtml", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _sePlanificacionSiembraService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _sePlanificacionSiembraService
                  .ConsultarTodos(_consultarTodos);

                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                AsignarViewBagMensajeError(respuestaEliminar);
                AsignarViewBagMensajeExito(respuestaEliminar);

                return View("Views/Comercial/PlanificacionSiembra/Index.cshtml", respuestaConsulta.Resultados);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}