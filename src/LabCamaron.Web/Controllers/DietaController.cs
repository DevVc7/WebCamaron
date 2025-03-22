using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Dieta;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class DietaController(ISeDietaService _seDietaService) : BaseController
    {
        private readonly DietaVm.ConsultarDietas _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seDietaService
                    .ConsultarTodos(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                List<DietaVm> dietas;
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    dietas = (respuestaConsulta.Resultados ?? []).ToList();
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }
                else
                {
                    dietas = [];
                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta);
                }

                return View("Index", dietas);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, MenuDieta.Permisos.Crear)]
        public IActionResult CrearDieta()
        {
            var dietaNueva = new DietaVm.Detallado();
            return View("CrearDieta", dietaNueva);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, MenuDieta.Permisos.Crear)]
        public async Task<IActionResult> CrearDieta([FromForm] DietaVm.CrearDieta crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<DietaVm.Detallado>();
                    return View("CrearDieta", crearVm);
                }

                var respuestaCrear = await _seDietaService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    AsignarViewBagMensajeExito(respuestaCrear.Mensaje);
                    return await Index();
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var rolVm = crear.Mapear<DietaVm.Detallado>();
                    return View("CrearDieta", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, MenuDieta.Permisos.Editar)]
        public async Task<IActionResult> EditarDieta(long id)
        {
            try
            {
                var respuestaConsulta = await _seDietaService
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
                    return View("EditarDieta", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new DietaVm()
                    {
                        Id = id
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarDieta", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, MenuDieta.Permisos.Editar)]
        public async Task<IActionResult> EditarDieta(DietaVm.ActualizarDieta actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<DietaVm>();
                    return View("EditarDieta", actualizarVm);
                }

                var respuesta = await _seDietaService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seDietaService
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

                    return View("EditarDieta", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<DietaVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarDieta", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuDieta.CodigoMenu, MenuDieta.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarDieta([FromForm] DietaVm.EliminarDieta eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seDietaService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seDietaService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seDietaService
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