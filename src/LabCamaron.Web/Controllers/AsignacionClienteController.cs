using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;

namespace LabCamaron.Web.Controllers
{
    public class AsignacionClienteController(ISeAsignacionClienteService asignacionClienteService) : BaseController
    {
        private readonly AsignacionClienteVm.ConsultarTodosAsignacionCliente _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await asignacionClienteService
                    .ConsultarTodos(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                List<AsignacionClienteVm> asignacion;
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    asignacion = (respuestaConsulta.Resultados ?? []).ToList();
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }
                else
                {
                    asignacion = [];
                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta);
                }

                return View("Index", asignacion);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Crear)]
        public IActionResult CrearAsignacionCliente()
        {
            var asignacionCliente = new AsignacionClienteVm.Detallado();
            return View("CrearAsignacionCliente", asignacionCliente);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Crear)]
        public async Task<IActionResult> CrearAsignacionCliente([FromBody] AsignacionClienteVm.CrearAsignacionCliente crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<AsignacionClienteVm.Detallado>();
                    return View("CrearAsignacionCliente", crearVm);
                }

                var respuestaCrear = await asignacionClienteService.Crear(crear);

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
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Editar)]
        public async Task<IActionResult> EditarAsignacionCliente(long id)
        {
            try
            {
                var respuestaConsulta = await asignacionClienteService
                  .ConsultarPorId(new()
                  {
                      Id = id
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarAsignacionCliente", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new AsignacionClienteVm()
                    {
                        Id = id
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarAsignacionCliente", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Editar)]
        public async Task<IActionResult> EditarAsignacionCliente([FromBody] AsignacionClienteVm.ActualizarAsignacionCliente actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<AsignacionClienteVm>();
                    return View("EditarAsignacionCliente", actualizarVm);
                }

                var respuesta = await asignacionClienteService
                    .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    return Json(new { success = true, message = respuesta.Mensaje });
                }
                else
                {
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
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarAsignacionCliente([FromForm] AsignacionClienteVm.EliminarAsignacionCliente eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await asignacionClienteService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await asignacionClienteService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await asignacionClienteService
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
    }
}