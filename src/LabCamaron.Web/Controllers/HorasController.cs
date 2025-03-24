using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Horas;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class HorasController(ISeHorasService seHorasService) : BaseController
    {
        private readonly ISeHorasService _seHorasService = seHorasService;

        private readonly HorasVm.ConsultarTodosHoras _consultarTodos = new()
        {
            Activo = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuHoras.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seHorasService
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
        [AccesosMenu(MenuHoras.CodigoMenu, MenuHoras.Permisos.Crear)]
        public IActionResult CrearHoras()
        {
            var rolNuevo = new HorasVm();
            return View("CrearHoras", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuHoras.CodigoMenu, MenuHoras.Permisos.Crear)]
        public async Task<IActionResult> CrearHoras([FromForm] HorasVm.CrearHoras crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<HorasVm>();
                    return View("CrearHoras", crearVm);
                }

                var respuestaCrear = await _seHorasService.Crear(crear);

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

                    var rolVm = crear.Mapear<HorasVm>();
                    return View("CrearHoras", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuHoras.CodigoMenu, MenuHoras.Permisos.Editar)]
        public async Task<IActionResult> EditarHoras(HorasVm.ConsultarHoras consultar)
        {
            try
            {
                var respuestaConsulta = await _seHorasService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarHoras", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new HorasVm()
                    {
                        Codigo = consultar.Codigo ?? "",
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarHoras", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuHoras.CodigoMenu, MenuHoras.Permisos.Editar)]
        public async Task<IActionResult> EditarHoras(HorasVm.ActualizarHoras actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<HorasVm>();
                    return View("EditarHoras", actualizarVm);
                }

                var respuesta = await _seHorasService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seHorasService
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

                    return View("EditarHoras", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<HorasVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarHoras", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuHoras.CodigoMenu, MenuHoras.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarHoras([FromForm] HorasVm.EliminarHoras eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _seHorasService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _seHorasService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _seHorasService
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