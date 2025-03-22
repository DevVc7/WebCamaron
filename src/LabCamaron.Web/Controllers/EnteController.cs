using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Ente;
using LabCamaronWeb.Dto.Maestros.EnteCliente;
using LabCamaronWeb.Dto.Maestros.EntePersonal;
using LabCamaronWeb.Dto.Maestros.EnteTecnico;
using LabCamaronWeb.Dto.Maestros.EnteVendedor;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class EnteController(ISeEnteService seEnteService, ISeEnteVendedorService seEnteVendedorService,
       ISeEnteClienteService seEnteClienteService, ISeEnteTecnicoService seEnteTecnicoService,
       ISeEntePersonalService seEntePersonalService) : BaseController
    {
        private readonly EnteVm.ConsultarTodosEnte _consultarTodos = new();

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await seEnteService
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

        #region Operaciones de Ente

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Crear)]
        public IActionResult CrearEnte()
        {
            var rolNuevo = new EnteVm();
            return View("CrearEnte", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Crear)]
        public async Task<IActionResult> CrearEnte([FromForm] EnteVm.CrearEnte crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<EnteVm>();
                    return View("CrearEnte", crearVm);
                }

                var respuestaCrear = await seEnteService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    var respuestaConsulta = await seEnteService
                      .ConsultarPorId(new()
                      {
                          Identificacion = crear.Identificacion
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuestaConsulta.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsulta.Respuesta);
                    }

                    if (respuestaConsulta.Respuesta.EsExitosa)
                    {
                        this.ViewBag.MensajeExito = respuestaConsulta.Respuesta.Mensaje;
                        return View("EditarEnte", respuestaConsulta.Resultado);
                    }
                    else
                    {
                        return await Index(false);
                    }
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var rolVm = crear.Mapear<EnteVm>();
                    return View("CrearEnte", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Editar)]
        public async Task<IActionResult> EditarEnte(string identificacion)
        {
            try
            {
                var respuestaConsulta = await seEnteService
                  .ConsultarPorId(new()
                  {
                      Identificacion = identificacion
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarEnte", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new EnteVm()
                    {
                        Identificacion = identificacion
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarEnte", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Editar)]
        public async Task<IActionResult> EditarEnte(EnteVm.ActualizarEnte actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<EnteVm>();
                    return View("EditarEnte", actualizarVm);
                }

                var respuesta = await seEnteService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await seEnteService
                      .ConsultarPorId(new()
                      {
                          Identificacion = actualizar.Identificacion
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarEnte", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<EnteVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarEnte", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarEnte([FromForm] EnteVm.EliminarEnte eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seEnteService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await seEnteService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seEnteService
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

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.Activar)]
        public async Task<IActionResult> ActivarEnte([FromForm] EnteVm.ActivarEnte activar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seEnteService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaActivar = await seEnteService
                  .Activar(activar);

                if (respuestaActivar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaActivar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seEnteService
                  .ConsultarTodos(_consultarTodos);

                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaActivar);
                }

                AsignarViewBagMensajeError(respuestaActivar);
                AsignarViewBagMensajeExito(respuestaActivar);

                return View("Index", respuestaConsulta.Resultados);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        #endregion Operaciones de Ente

        #region Operaciones de Vendedor

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.EditarVendedor)]
        public async Task<IActionResult> EditarVendedorEnte(EnteVendedorVm.CrearActualizarEnteVendedor actualizar)
        {
            try
            {
                this.ViewBag.TabRoles = true;

                var respuestaConsulta = await seEnteService
                    .ConsultarPorId(new()
                    {
                        Identificacion = actualizar.Identificacion
                    });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var modelo = respuestaConsulta.Resultado!;
                    modelo.Vendedor = actualizar.Mapear<EnteVendedorVm>();
                    return View("EditarEnte", modelo);
                }

                var respuesta = await seEnteVendedorService
                  .CrearActualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Consultamos la entidad actualizada
                respuestaConsulta = await seEnteService
                  .ConsultarPorId(new()
                  {
                      Identificacion = actualizar.Identificacion
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeExito(respuesta.Mensaje);
                return View("EditarEnte", respuestaConsulta.Resultado);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        #endregion Operaciones de Vendedor

        #region Operaciones de Cliente

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.EditarCliente)]
        public async Task<IActionResult> EditarClienteEnte(EnteClienteVm.CrearActualizarEnteCliente actualizar)
        {
            try
            {
                this.ViewBag.TabRoles = true;

                var respuestaConsulta = await seEnteService
                    .ConsultarPorId(new()
                    {
                        Identificacion = actualizar.Identificacion
                    });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var modelo = respuestaConsulta.Resultado!;
                    modelo.Cliente = actualizar.Mapear<EnteClienteVm>();
                    return View("EditarEnte", modelo);
                }

                var respuesta = await seEnteClienteService
                  .CrearActualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Consultamos la entidad actualizada
                respuestaConsulta = await seEnteService
                  .ConsultarPorId(new()
                  {
                      Identificacion = actualizar.Identificacion
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeExito(respuesta.Mensaje);
                return View("EditarEnte", respuestaConsulta.Resultado);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        #endregion Operaciones de Cliente

        #region Operaciones de Tecnico

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.EditarTecnico)]
        public async Task<IActionResult> EditarTecnicoEnte(EnteTecnicoVm.CrearActualizarEnteTecnico actualizar)
        {
            try
            {
                this.ViewBag.TabRoles = true;

                var respuestaConsulta = await seEnteService
                    .ConsultarPorId(new()
                    {
                        Identificacion = actualizar.Identificacion
                    });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var modelo = respuestaConsulta.Resultado!;
                    modelo.Tecnico = actualizar.Mapear<EnteTecnicoVm>();
                    return View("EditarEnte", modelo);
                }

                var respuesta = await seEnteTecnicoService
                  .CrearActualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Consultamos la entidad actualizada
                respuestaConsulta = await seEnteService
                  .ConsultarPorId(new()
                  {
                      Identificacion = actualizar.Identificacion
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeExito(respuesta.Mensaje);
                return View("EditarEnte", respuestaConsulta.Resultado);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        #endregion Operaciones de Tecnico

        #region Operaciones de Personal

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnte.CodigoMenu, MenuEnte.Permisos.EditarPersonal)]
        public async Task<IActionResult> EditarPersonalEnte(EntePersonalVm.CrearActualizarEntePersonal actualizar)
        {
            try
            {
                this.ViewBag.TabRoles = true;

                var respuestaConsulta = await seEnteService
                    .ConsultarPorId(new()
                    {
                        Identificacion = actualizar.Identificacion
                    });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var modelo = respuestaConsulta.Resultado!;
                    modelo.Personal = actualizar.Mapear<EntePersonalVm>();
                    return View("EditarEnte", modelo);
                }

                var respuesta = await seEntePersonalService
                  .CrearActualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Consultamos la entidad actualizada
                respuestaConsulta = await seEnteService
                  .ConsultarPorId(new()
                  {
                      Identificacion = actualizar.Identificacion
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                AsignarViewBagMensajeExito(respuesta.Mensaje);
                return View("EditarEnte", respuestaConsulta.Resultado);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        #endregion Operaciones de Personal
    }
}