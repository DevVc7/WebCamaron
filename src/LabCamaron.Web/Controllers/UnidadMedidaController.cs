using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Parametrizacion.UnidadMedida;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Parametrizacion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class UnidadMedidaController(ISeUnidadMedidaService seUnidadMedidaService) : BaseController
    {
        private readonly ISeUnidadMedidaService _seUnidadMedidaService = seUnidadMedidaService;

        private readonly UnidadMedidaVm.ConsultarTodosUnidadMedida _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seUnidadMedidaService
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
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, MenuUnidadMedida.Permisos.Crear)]
        public IActionResult CrearUnidadMedida()
        {
            var rolNuevo = new UnidadMedidaVm();
            return View("CrearUnidadMedida", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, MenuUnidadMedida.Permisos.Crear)]
        public async Task<IActionResult> CrearUnidadMedida([FromForm] UnidadMedidaVm.CrearUnidadMedida crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<UnidadMedidaVm>();
                    return View("CrearUnidadMedida", crearVm);
                }

                var respuestaCrear = await _seUnidadMedidaService.Crear(crear);

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

                    var rolVm = crear.Mapear<UnidadMedidaVm>();
                    return View("CrearUnidadMedida", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, MenuUnidadMedida.Permisos.Editar)]
        public async Task<IActionResult> EditarUnidadMedida(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seUnidadMedidaService
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
                    return View("EditarUnidadMedida", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new UnidadMedidaVm()
                    {
                        Codigo = codigo
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarUnidadMedida", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, MenuUnidadMedida.Permisos.Editar)]
        public async Task<IActionResult> EditarUnidadMedida(UnidadMedidaVm.ActualizarUnidadMedida actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<UnidadMedidaVm>();
                    return View("EditarUnidadMedida", actualizarVm);
                }

                var respuesta = await _seUnidadMedidaService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seUnidadMedidaService
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

                    return View("EditarUnidadMedida", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<UnidadMedidaVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarUnidadMedida", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuUnidadMedida.CodigoMenu, MenuUnidadMedida.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarUnidadMedida([FromForm] UnidadMedidaVm.EliminarUnidadMedida eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seUnidadMedidaService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seUnidadMedidaService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seUnidadMedidaService
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