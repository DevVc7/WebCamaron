using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Parametrizacion.Empresa;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Parametrizacion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class EmpresaController(ISeEmpresaService seEmpresaService) : BaseController
    {
        private readonly ISeEmpresaService _seEmpresaService = seEmpresaService;

        private readonly EmpresaVm.ConsultarTodosEmpresa _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEmpresa.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seEmpresaService
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
        [AccesosMenu(MenuEmpresa.CodigoMenu, MenuEmpresa.Permisos.Crear)]
        public IActionResult CrearEmpresa()
        {
            var rolNuevo = new EmpresaVm();
            return View("CrearEmpresa", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEmpresa.CodigoMenu, MenuEmpresa.Permisos.Crear)]
        public async Task<IActionResult> CrearEmpresa([FromForm] EmpresaVm.CrearEmpresa crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<EmpresaVm>();
                    return View("CrearEmpresa", crearVm);
                }

                var respuestaCrear = await _seEmpresaService.Crear(crear);

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

                    var rolVm = crear.Mapear<EmpresaVm>();
                    return View("CrearEmpresa", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEmpresa.CodigoMenu, MenuEmpresa.Permisos.Editar)]
        public async Task<IActionResult> EditarEmpresa(string codigo)
        {
            try
            {
                var respuestaConsulta = await _seEmpresaService
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
                    return View("EditarEmpresa", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new EmpresaVm()
                    {
                        Codigo = codigo
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarEmpresa", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEmpresa.CodigoMenu, MenuEmpresa.Permisos.Editar)]
        public async Task<IActionResult> EditarEmpresa(EmpresaVm.ActualizarEmpresa actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<EmpresaVm>();
                    return View("EditarEmpresa", actualizarVm);
                }

                var respuesta = await _seEmpresaService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seEmpresaService
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

                    return View("EditarEmpresa", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<EmpresaVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarEmpresa", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEmpresa.CodigoMenu, MenuEmpresa.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarEmpresa([FromForm] EmpresaVm.EliminarEmpresa eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seEmpresaService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seEmpresaService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seEmpresaService
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