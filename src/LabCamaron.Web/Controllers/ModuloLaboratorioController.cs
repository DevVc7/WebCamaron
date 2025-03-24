using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Parametrizacion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class ModuloLaboratorioController(ISeLaboratorioService seLaboratorioServices, ISeModuloLaboratorioService seModuloLaboratorioService) : BaseController
    {
        private readonly ISeLaboratorioService _seLaboratorioServices = seLaboratorioServices;
        private readonly ISeModuloLaboratorioService _seModuloLaboratorioService = seModuloLaboratorioService;

        private readonly ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seModuloLaboratorioService
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
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, MenuModuloLaboratorio.Permisos.Crear)]
        public IActionResult CrearModuloLaboratorio()
        {
            var rolNuevo = new ModuloLaboratorioVm();
            return View("CrearModuloLaboratorio", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, MenuModuloLaboratorio.Permisos.Crear)]
        public async Task<IActionResult> CrearModuloLaboratorio([FromForm] ModuloLaboratorioVm.CrearModuloLaboratorio crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<ModuloLaboratorioVm>();
                    return View("CrearModuloLaboratorio", crearVm);
                }

                var respuestaCrear = await _seModuloLaboratorioService.Crear(crear);

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

                    var rolVm = crear.Mapear<ModuloLaboratorioVm>();
                    return View("CrearModuloLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, MenuModuloLaboratorio.Permisos.Editar)]
        public async Task<IActionResult> EditarModuloLaboratorio(ModuloLaboratorioVm.ConsultarModuloLaboratorio consultar)
        {
            try
            {
                var respuestaConsulta = await _seModuloLaboratorioService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarModuloLaboratorio", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new ModuloLaboratorioVm()
                    {
                        IdLaboratorio = consultar.IdLaboratorio ?? 0,
                        Codigo = consultar.Codigo ?? string.Empty,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarModuloLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, MenuModuloLaboratorio.Permisos.Editar)]
        public async Task<IActionResult> EditarModuloLaboratorio(ModuloLaboratorioVm.ActualizarModuloLaboratorio actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<ModuloLaboratorioVm>();
                    return View("EditarModuloLaboratorio", actualizarVm);
                }

                var respuesta = await _seModuloLaboratorioService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seModuloLaboratorioService
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

                    return View("EditarModuloLaboratorio", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<ModuloLaboratorioVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarModuloLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuModuloLaboratorio.CodigoMenu, MenuModuloLaboratorio.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarModuloLaboratorio([FromForm] ModuloLaboratorioVm.EliminarModuloLaboratorio eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seModuloLaboratorioService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seModuloLaboratorioService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seModuloLaboratorioService
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