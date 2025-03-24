using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Ente;
using LabCamaronWeb.Dto.Maestros.EnteCliente;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class EnteClienteController(ISeEnteService seEnteService, ISeEnteClienteService seEnteClienteService) : BaseController
    {
        private readonly EnteVm.ConsultarTodosEnte _consultarTodos = new();

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuEnteCliente.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(string mensajeExito = "", string mensajeError = "")
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await seEnteService
                  .ConsultarClientes(_consultarTodos);

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
        [AccesosMenu(MenuEnteCliente.CodigoMenu, MenuEnteCliente.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarEnteCliente([FromForm] EnteClienteVm.EliminarEnteCliente eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await seEnteService
                      .ConsultarClientes(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await seEnteClienteService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await seEnteService
                  .ConsultarClientes(_consultarTodos);

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
        [AccesosMenu(MenuEnteCliente.CodigoMenu, MenuEnteCliente.Permisos.Crear)]
        public IActionResult CrearEnteCliente()
        {
            var rolNuevo = new EnteClienteVm();
            return View("CrearEnteCliente", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuEnteCliente.CodigoMenu, MenuEnteCliente.Permisos.Editar)]
        public async Task<IActionResult> CrearActualizarEnteCliente(EnteClienteVm.CrearActualizarEnteCliente actualizar)
        {
            try
            {
                actualizar.Activo = true;
                var respuesta = await seEnteClienteService
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
        [AccesosMenu(MenuEnteCliente.CodigoMenu, MenuEnteCliente.Permisos.Editar)]
        public async Task<IActionResult> EditarEnteCliente(string identificacion)
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
                    var modelo = ente.Mapear<EnteClienteVm>();
                    modelo.IdEnte = ente.Id;
                    modelo.Codigo = ente.Cliente.Codigo;
                    modelo.Contacto = ente.Cliente.Contacto;

                    return View("EditarEnteCliente", modelo);
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