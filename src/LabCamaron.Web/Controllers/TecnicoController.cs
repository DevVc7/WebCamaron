using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Personal;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Constantes.Procesos;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class TecnicoController(ISePersonalService sePersonalService, ISeCargoService seCargoService) : BaseController
    {
        private readonly PersonalVm.ConsultarTodosPersonal _consultarTodos = new()
        {
            SoloActivos = true,
            CodigoCargo = ConstantesCargo.Tecnico
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await sePersonalService
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

        #region Operaciones de Tecnico

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Crear)]
        public async Task<IActionResult> CrearTecnico()
        {
            var cargos = await seCargoService
                .ConsultarPorCodigo(new()
                {
                    Codigo = ConstantesCargo.Tecnico
                });

            var rolNuevo = new PersonalVm() { 
                CodigoCargo = cargos.Resultado?.Codigo ?? "",
                NombreCargo = cargos.Resultado?.Nombre ?? "",
            };
            return View("CrearTecnico", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Crear)]
        public async Task<IActionResult> CrearTecnico([FromForm] PersonalVm.CrearPersonal crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<PersonalVm>();
                    return View("CrearTecnico", crearVm);
                }

                var respuestaCrear = await sePersonalService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return ProcesarError(respuestaCrear);
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    var respuestaConsulta = await sePersonalService
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
                        return View("EditarTecnico", respuestaConsulta.Resultado);
                    }
                    else
                    {
                        return await Index(false);
                    }
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                    var rolVm = crear.Mapear<PersonalVm>();
                    return View("CrearTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Editar)]
        public async Task<IActionResult> EditarTecnico(string identificacion)
        {
            try
            {
                var respuestaConsulta = await sePersonalService
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
                    return View("EditarTecnico", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new PersonalVm()
                    {
                        Identificacion = identificacion
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Editar)]
        public async Task<IActionResult> EditarTecnico(PersonalVm.ActualizarPersonal actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<PersonalVm>();
                    return View("EditarTecnico", actualizarVm);
                }

                var respuesta = await sePersonalService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await sePersonalService
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

                    return View("EditarTecnico", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<PersonalVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarTecnico", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarTecnico([FromForm] PersonalVm.EliminarPersonal eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await sePersonalService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await sePersonalService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await sePersonalService
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
        [AccesosMenu(MenuTecnico.CodigoMenu, MenuTecnico.Permisos.Activar)]
        public async Task<IActionResult> ActivarTecnico([FromForm] PersonalVm.ActivarPersonal activar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await sePersonalService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaActivar = await sePersonalService
                  .Activar(activar);

                if (respuestaActivar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaActivar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await sePersonalService
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

        #endregion Operaciones de Tecnico
    }
}