using LabCamaron.Web.Autorizadores;
using LabCamaron.Web.Models;
using LabCamaronWeb.Dto.Parametrizacion.Laboratorio;
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
    public class LaboratorioController(ISeEmpresaService seEmpresaServices, ISeLaboratorioService seLaboratorioService) : BaseController
    {
        private readonly ISeEmpresaService _seEmpresaServices = seEmpresaServices;
        private readonly ISeLaboratorioService _seLaboratorioService = seLaboratorioService;

        private readonly LaboratorioVm.ConsultarTodosLaboratorio _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuLaboratorio.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seLaboratorioService
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
        [AccesosMenu(MenuLaboratorio.CodigoMenu, MenuLaboratorio.Permisos.Crear)]
        public IActionResult CrearLaboratorio()
        {
            var rolNuevo = new LaboratorioVm();
            return View("CrearLaboratorio", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuLaboratorio.CodigoMenu, MenuLaboratorio.Permisos.Crear)]
        public async Task<IActionResult> CrearLaboratorio([FromForm] LaboratorioVm.CrearLaboratorio crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<LaboratorioVm>();
                    return View("CrearLaboratorio", crearVm);
                }

                var respuestaCrear = await _seLaboratorioService.Crear(crear);

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

                    var rolVm = crear.Mapear<LaboratorioVm>();
                    return View("CrearLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuLaboratorio.CodigoMenu, MenuLaboratorio.Permisos.Editar)]
        public async Task<IActionResult> EditarLaboratorio(LaboratorioVm.ConsultarLaboratorio consultar)
        {
            try
            {
                var respuestaConsulta = await _seLaboratorioService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarLaboratorio", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new LaboratorioVm()
                    {
                        IdEmpresa = consultar.IdEmpresa ?? 0,
                        Codigo = consultar.Codigo ?? string.Empty,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuLaboratorio.CodigoMenu, MenuLaboratorio.Permisos.Editar)]
        public async Task<IActionResult> EditarLaboratorio(LaboratorioVm.ActualizarLaboratorio actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<LaboratorioVm>();
                    return View("EditarLaboratorio", actualizarVm);
                }

                var respuesta = await _seLaboratorioService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seLaboratorioService
                      .ConsultarPorId(new()
                      {
                          IdEmpresa = actualizar.IdEmpresa,
                          Codigo = actualizar.Codigo
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarLaboratorio", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<LaboratorioVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarLaboratorio", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuLaboratorio.CodigoMenu, MenuLaboratorio.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarLaboratorio([FromForm] LaboratorioVm.EliminarLaboratorio eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seLaboratorioService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seLaboratorioService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seLaboratorioService
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
        public async Task<IActionResult> ListarEmpresas(string textoContiene)
        {
            try
            {
                var consulta = await _seEmpresaServices.ConsultarTodos(new()
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