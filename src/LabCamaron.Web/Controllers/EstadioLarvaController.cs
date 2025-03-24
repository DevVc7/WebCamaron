using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.EstadioLarva;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class EstadioLarvaController(ISeEstadioLarvaService seEstadioLarvaService) : BaseController
    {
        private readonly ISeEstadioLarvaService _seEstadioLarvaService = seEstadioLarvaService;

        private readonly EstadioLarvaVm.ConsultarTodosEstadioLarva _consultarTodos = new()
        {
            SoloActivo = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seEstadioLarvaService
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
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, MenuEstadioLarva.Permisos.Crear)]
        public IActionResult CrearEstadioLarva()
        {
            var rolNuevo = new EstadioLarvaVm();
            return View("CrearEstadioLarva", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, MenuEstadioLarva.Permisos.Crear)]
        public async Task<IActionResult> CrearEstadioLarva([FromForm] EstadioLarvaVm.CrearEstadioLarva crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<EstadioLarvaVm>();
                    return View("CrearEstadioLarva", crearVm);
                }

                var respuestaCrear = await _seEstadioLarvaService.Crear(crear);

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

                    var rolVm = crear.Mapear<EstadioLarvaVm>();
                    return View("CrearEstadioLarva", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, MenuEstadioLarva.Permisos.Editar)]
        public async Task<IActionResult> EditarEstadioLarva(EstadioLarvaVm.ConsultarEstadioLarva consultar)
        {
            try
            {
                var respuestaConsulta = await _seEstadioLarvaService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarEstadioLarva", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new EstadioLarvaVm()
                    {
                        Id = consultar.Id ?? 0,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarEstadioLarva", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, MenuEstadioLarva.Permisos.Editar)]
        public async Task<IActionResult> EditarEstadioLarva(EstadioLarvaVm.ActualizarEstadioLarva actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<EstadioLarvaVm>();
                    return View("EditarEstadioLarva", actualizarVm);
                }

                var respuesta = await _seEstadioLarvaService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seEstadioLarvaService
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

                    return View("EditarEstadioLarva", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<EstadioLarvaVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarEstadioLarva", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEstadioLarva.CodigoMenu, MenuEstadioLarva.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarEstadioLarva([FromForm] EstadioLarvaVm.EliminarEstadioLarva eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seEstadioLarvaService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seEstadioLarvaService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seEstadioLarvaService
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