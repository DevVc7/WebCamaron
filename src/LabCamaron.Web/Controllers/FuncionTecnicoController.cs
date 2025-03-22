using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.FuncionTecnico;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class FuncionTecnicoController(ISeFuncionTecnicoService seFuncionTecnicoService) : BaseController
    {
        private readonly ISeFuncionTecnicoService _seFuncionTecnicoService = seFuncionTecnicoService;

        private readonly FuncionTecnicoVm.ConsultarTodosFuncionTecnico _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seFuncionTecnicoService
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
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, MenuFuncionTecnico.Permisos.Crear)]
        public IActionResult CrearFuncionTecnico()
        {
            var rolNuevo = new FuncionTecnicoVm();
            return View("CrearFuncionTecnico", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, MenuFuncionTecnico.Permisos.Crear)]
        public async Task<IActionResult> CrearFuncionTecnico([FromForm] FuncionTecnicoVm.CrearFuncionTecnico crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<FuncionTecnicoVm>();
                    return View("CrearFuncionTecnico", crearVm);
                }

                var respuestaCrear = await _seFuncionTecnicoService.Crear(crear);

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

                    var rolVm = crear.Mapear<FuncionTecnicoVm>();
                    return View("CrearFuncionTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, MenuFuncionTecnico.Permisos.Editar)]
        public async Task<IActionResult> EditarFuncionTecnico(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seFuncionTecnicoService
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
                    return View("EditarFuncionTecnico", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new FuncionTecnicoVm()
                    {
                        Codigo = codigo
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarFuncionTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, MenuFuncionTecnico.Permisos.Editar)]
        public async Task<IActionResult> EditarFuncionTecnico(FuncionTecnicoVm.ActualizarFuncionTecnico actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<FuncionTecnicoVm>();
                    return View("EditarFuncionTecnico", actualizarVm);
                }

                var respuesta = await _seFuncionTecnicoService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seFuncionTecnicoService
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

                    return View("EditarFuncionTecnico", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<FuncionTecnicoVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarFuncionTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuFuncionTecnico.CodigoMenu, MenuFuncionTecnico.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarFuncionTecnico([FromForm] FuncionTecnicoVm.EliminarFuncionTecnico eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seFuncionTecnicoService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seFuncionTecnicoService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seFuncionTecnicoService
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