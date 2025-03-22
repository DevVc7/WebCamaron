using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.ParametroAmbiental;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class ParametroAmbientalController(ISeParametroAmbientalService seParametroAmbientalService) : BaseController
    {
        private readonly ISeParametroAmbientalService _seParametroAmbientalService = seParametroAmbientalService;

        private readonly ParametroAmbientalVm.ConsultarTodosParametroAmbiental _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seParametroAmbientalService
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
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, MenuParametroAmbiental.Permisos.Crear)]
        public IActionResult CrearParametroAmbiental()
        {
            var rolNuevo = new ParametroAmbientalVm();
            return View("CrearParametroAmbiental", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, MenuParametroAmbiental.Permisos.Crear)]
        public async Task<IActionResult> CrearParametroAmbiental([FromForm] ParametroAmbientalVm.CrearParametroAmbiental crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<ParametroAmbientalVm>();
                    return View("CrearParametroAmbiental", crearVm);
                }

                var respuestaCrear = await _seParametroAmbientalService.Crear(crear);

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

                    var rolVm = crear.Mapear<ParametroAmbientalVm>();
                    return View("CrearParametroAmbiental", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, MenuParametroAmbiental.Permisos.Editar)]
        public async Task<IActionResult> EditarParametroAmbiental(ParametroAmbientalVm.ConsultarParametroAmbiental consultar)
        {
            try
            {
                var respuestaConsulta = await _seParametroAmbientalService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarParametroAmbiental", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new ParametroAmbientalVm()
                    {
                        IdLaboratorio = consultar.IdLaboratorio ?? 0,
                        Codigo = consultar.Codigo ?? string.Empty,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarParametroAmbiental", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, MenuParametroAmbiental.Permisos.Editar)]
        public async Task<IActionResult> EditarParametroAmbiental(ParametroAmbientalVm.ActualizarParametroAmbiental actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<ParametroAmbientalVm>();
                    return View("EditarParametroAmbiental", actualizarVm);
                }

                var respuesta = await _seParametroAmbientalService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seParametroAmbientalService
                      .ConsultarPorId(new()
                      {
                          IdLaboratorio = actualizar.IdLaboratorio,
                          Codigo = actualizar.Codigo
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarParametroAmbiental", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<ParametroAmbientalVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarParametroAmbiental", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuParametroAmbiental.CodigoMenu, MenuParametroAmbiental.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarParametroAmbiental([FromForm] ParametroAmbientalVm.EliminarParametroAmbiental eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seParametroAmbientalService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seParametroAmbientalService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seParametroAmbientalService
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