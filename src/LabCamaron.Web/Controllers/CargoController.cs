using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Cargo;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class CargoController(ISeCargoService seCargoService) : BaseController
    {
        private readonly ISeCargoService _seCargoService = seCargoService;

        private readonly CargoVm.ConsultarTodosCargo _consultarTodos = new()
        {
            SoloActivo = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCargo.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seCargoService
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
        [AccesosMenu(MenuCargo.CodigoMenu, MenuCargo.Permisos.Editar)]
        public async Task<IActionResult> EditarCargo(CargoVm.ConsultarCargo consultar)
        {
            try
            {
                var respuestaConsulta = await _seCargoService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarCargo", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new CargoVm()
                    {
                        Id = consultar.Id ?? 0,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarCargo", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuCargo.CodigoMenu, MenuCargo.Permisos.Editar)]
        public async Task<IActionResult> EditarCargo(CargoVm.ActualizarCargo actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<CargoVm>();
                    return View("EditarCargo", actualizarVm);
                }

                var respuesta = await _seCargoService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seCargoService
                      .ConsultarPorId(new()
                      {
                          Id = actualizar.Id,
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarCargo", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<CargoVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarCargo", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}