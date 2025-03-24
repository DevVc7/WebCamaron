using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Ente;
using LabCamaronWeb.Dto.Maestros.EntePersonal;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class EntePersonalController(ISeEnteService seEnteService, ISeEntePersonalService seEntePersonalService) : BaseController
    {
        private readonly EnteVm.ConsultarTodosEnte _consultarTodos = new();

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEntePersonal.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(string mensajeExito = "", string mensajeError = "")
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await seEnteService
                  .ConsultarPersonal(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                var roles = respuestaConsulta.Respuesta.EsExitosa
                  ? respuestaConsulta.Resultados : [];

                if (!string.IsNullOrEmpty(mensajeExito))
                {
                    AsignarViewBagMensajeExito(mensajeExito);
                }

                if (!string.IsNullOrEmpty(mensajeError))
                {
                    AsignarViewBagMensajeError(mensajeError);
                }

                return View("Index", roles);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEntePersonal.CodigoMenu, MenuEntePersonal.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarEntePersonal([FromForm] EntePersonalVm.EliminarEntePersonal eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seEnteService
                      .ConsultarPersonal(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await seEntePersonalService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seEnteService
                  .ConsultarPersonal(_consultarTodos);

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
        [AccesosMenu(MenuEntePersonal.CodigoMenu, MenuEntePersonal.Permisos.Crear)]
        public IActionResult CrearEntePersonal()
        {
            var rolNuevo = new EntePersonalVm();
            return View("CrearEntePersonal", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEntePersonal.CodigoMenu, MenuEntePersonal.Permisos.Editar)]
        public async Task<IActionResult> CrearActualizarEntePersonal(EntePersonalVm.CrearActualizarEntePersonal actualizar)
        {
            try
            {
                actualizar.Activo = true;
                var respuesta = await seEntePersonalService
                  .CrearActualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                if (respuesta.EsExitosa)
                {
                    return await Index(mensajeExito: respuesta.Mensaje);
                }
                else
                {
                    return await Index(mensajeError: respuesta.Mensaje);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEntePersonal.CodigoMenu, MenuEntePersonal.Permisos.Editar)]
        public async Task<IActionResult> EditarEntePersonal(string identificacion)
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
                    var ente = respuestaConsulta.Resultado!;
                    var modelo = ente.Mapear<EntePersonalVm>();
                    modelo.IdEnte = ente.Id;
                    modelo.Codigo = ente.Personal.Codigo;
                    modelo.IdLaboratorio = ente.Personal.IdLaboratorio;
                    modelo.NombreLaboratorio = ente.Personal.NombreLaboratorio;
                    modelo.IdModuloLaboratorio = ente.Personal.IdModuloLaboratorio;
                    modelo.NombreModuloLaboratorio = ente.Personal.NombreModuloLaboratorio;
                    modelo.IdCargo = ente.Personal.IdCargo;
                    modelo.NombreCargo = ente.Personal.NombreCargo;

                    return View("EditarEntePersonal", modelo);
                }

                return await Index(mensajeError: respuestaConsulta.Respuesta.Mensaje);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}