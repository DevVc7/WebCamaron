using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Cliente;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Constantes.Procesos;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class ClienteController(ISeClienteService seClienteService) : BaseController
    {
        private readonly ClienteVm.ConsultarTodosCliente _consultarTodos = new();

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await seClienteService
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

        #region Operaciones de Cliente

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Crear)]
        public IActionResult CrearCliente()
        {
            var rolNuevo = new ClienteVm.Detallado();
            return View("CrearCliente", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Crear)]
        public async Task<IActionResult> CrearCliente([FromForm] ClienteVm.CrearCliente crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<ClienteVm.Detallado>();
                    return View("CrearCliente", crearVm);
                }

                var respuestaCrear = await seClienteService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    var respuestaConsulta = await seClienteService
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
                        return View("EditarCliente", respuestaConsulta.Resultado);
                    }
                    else
                    {
                        return await Index(false);
                    }
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var rolVm = crear.Mapear<ClienteVm.Detallado>();
                    return View("CrearCliente", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Editar)]
        public async Task<IActionResult> EditarCliente(string identificacion)
        {
            try
            {
                var respuestaConsulta = await seClienteService
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
                    return View("EditarCliente", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new ClienteVm.Detallado()
                    {
                        Identificacion = identificacion
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarCliente", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Editar)]
        public async Task<IActionResult> EditarCliente(ClienteVm.ActualizarCliente actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<ClienteVm.Detallado>();
                    return View("EditarCliente", actualizarVm);
                }

                var respuesta = await seClienteService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await seClienteService
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

                    return View("EditarCliente", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<ClienteVm.Detallado>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarCliente", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarCliente([FromForm] ClienteVm.EliminarCliente eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seClienteService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await seClienteService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seClienteService
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
        [AccesosMenu(MenuCliente.CodigoMenu, MenuCliente.Permisos.Activar)]
        public async Task<IActionResult> ActivarCliente([FromForm] ClienteVm.ActivarCliente activar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seClienteService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaActivar = await seClienteService
                  .Activar(activar);

                if (respuestaActivar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaActivar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seClienteService
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

        #endregion Operaciones de Cliente
    }
}