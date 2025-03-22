using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Insumos;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class InsumosController(ISeInsumosService seInsumosService) : BaseController
    {
        private readonly ISeInsumosService _seInsumosService = seInsumosService;

        private readonly InsumosVm.ConsultarTodosInsumos _consultarTodos = new()
        {
            Activo = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuInsumos.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seInsumosService
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
        [AccesosMenu(MenuInsumos.CodigoMenu, MenuInsumos.Permisos.Crear)]
        public IActionResult CrearInsumos()
        {
            var rolNuevo = new InsumosVm.CrearInsumos();
            return View("CrearInsumos", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuInsumos.CodigoMenu, MenuInsumos.Permisos.Crear)]
        public async Task<IActionResult> CrearInsumos([FromForm] InsumosVm.CrearInsumos crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<InsumosVm>();
                    return View("CrearInsumos", crearVm);
                }

                var respuestaCrear = await _seInsumosService.Crear(crear);

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

                    return View("CrearInsumos", crear);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuInsumos.CodigoMenu, MenuInsumos.Permisos.Editar)]
        public async Task<IActionResult> EditarInsumos(InsumosVm.ConsultarInsumos consultar)
        {
            try
            {
                var respuestaConsulta = await _seInsumosService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    var actualizar = new InsumosVm.ActualizarInsumos()
                    {
                        Id = respuestaConsulta.Resultado!.Id,
                        Codigo = respuestaConsulta.Resultado.Codigo,
                        IdCategoria = respuestaConsulta.Resultado.IdCategoria,
                        IdLaboratorio = respuestaConsulta.Resultado.IdLaboratorio,
                        IdMarca = respuestaConsulta.Resultado.IdMarca,
                        Nombre = respuestaConsulta.Resultado.NombreInsumo,
                        Presentacion = (decimal)respuestaConsulta.Resultado.Presentacion,
                        Sku = respuestaConsulta.Resultado.Sku,
                        UnidadMedida = respuestaConsulta.Resultado.UnidadMedida,
                    };

                    return View("EditarInsumos", actualizar);
                }
                else
                {
                    var rolVm = new InsumosVm.ActualizarInsumos()
                    {
                        Id = consultar.Id,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarInsumos", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuInsumos.CodigoMenu, MenuInsumos.Permisos.Editar)]
        public async Task<IActionResult> EditarInsumos(InsumosVm.ActualizarInsumos actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<InsumosVm>();
                    return View("EditarInsumos", actualizarVm);
                }

                var respuesta = await _seInsumosService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seInsumosService
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

                    var actualizarData = new InsumosVm.ActualizarInsumos()
                    {
                        Id = respuestaConsulta.Resultado!.Id,
                        Codigo = respuestaConsulta.Resultado.Codigo,
                        IdCategoria = respuestaConsulta.Resultado.IdCategoria,
                        IdLaboratorio = respuestaConsulta.Resultado.IdLaboratorio,
                        IdMarca = respuestaConsulta.Resultado.IdMarca,
                        Nombre = respuestaConsulta.Resultado.NombreInsumo,
                        Presentacion = (decimal)respuestaConsulta.Resultado.Presentacion,
                        Sku = respuestaConsulta.Resultado.Sku,
                        UnidadMedida = respuestaConsulta.Resultado.UnidadMedida,
                    };
                    return View("EditarInsumos", actualizarData);
                }
                else
                {
                    var rolVm = actualizar.Mapear<InsumosVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarInsumos", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuInsumos.CodigoMenu, MenuInsumos.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarInsumos([FromForm] InsumosVm.EliminarInsumos eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seInsumosService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seInsumosService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seInsumosService
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