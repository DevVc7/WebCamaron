using LabCamaron.Web.Autorizadores;
using LabCamaron.Web.Models;
using LabCamaronWeb.Dto.Parametrizacion.Tanque;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Parametrizacion;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers
{
    public class TanqueController(ISeModuloLaboratorioService seModuloLaboratorioServices, ISeTanqueService seTanqueService) : BaseController
    {
        private readonly ISeModuloLaboratorioService _seModuloLaboratorioServices = seModuloLaboratorioServices;
        private readonly ISeTanqueService _seTanqueService = seTanqueService;

        private readonly TanqueVm.ConsultarTodosTanque _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuTanque.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seTanqueService
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
        [AccesosMenu(MenuTanque.CodigoMenu, MenuTanque.Permisos.Crear)]
        public IActionResult CrearTanque()
        {
            var rolNuevo = new TanqueVm();
            return View("CrearTanque", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTanque.CodigoMenu, MenuTanque.Permisos.Crear)]
        public async Task<IActionResult> CrearTanque([FromForm] TanqueVm.CrearTanque crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<TanqueVm>();
                    return View("CrearTanque", crearVm);
                }

                var respuestaCrear = await _seTanqueService.Crear(crear);

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

                    var rolVm = crear.Mapear<TanqueVm>();
                    return View("CrearTanque", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuTanque.CodigoMenu, MenuTanque.Permisos.Editar)]
        public async Task<IActionResult> EditarTanque(TanqueVm.ConsultarTanque consultar)
        {
            try
            {
                var respuestaConsulta = await _seTanqueService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarTanque", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new TanqueVm()
                    {
                        IdModulo = consultar.IdModulo ?? 0,
                        Codigo = consultar.Codigo ?? string.Empty,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarTanque", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTanque.CodigoMenu, MenuTanque.Permisos.Editar)]
        public async Task<IActionResult> EditarTanque(TanqueVm.ActualizarTanque actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<TanqueVm>();
                    return View("EditarTanque", actualizarVm);
                }

                var respuesta = await _seTanqueService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seTanqueService
                      .ConsultarPorId(new()
                      {
                          IdModulo = actualizar.IdModulo,
                          Codigo = actualizar.Codigo
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarTanque", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<TanqueVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarTanque", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTanque.CodigoMenu, MenuTanque.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarTanque([FromForm] TanqueVm.EliminarTanque eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seTanqueService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seTanqueService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seTanqueService
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarModuloLaboratorios(string textoContiene)
        {
            try
            {
                var consulta = await _seModuloLaboratorioServices.ConsultarTodos(new()
                {
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombre,
                        })
                        .ToList();

                    if (!string.IsNullOrEmpty(textoContiene))
                    {
                        resultado = resultado
                          .Where(x => x.Text.Contains(textoContiene, StringComparison.OrdinalIgnoreCase))
                          .ToList();
                    }

                    return Ok(resultado);
                }
                else
                {
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ha ocurrido un error");
            }
        }
    }
}