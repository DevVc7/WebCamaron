using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class CupoVendedorController(ISeCupoVendedorService seCupoVendedorService, ISeModuloLaboratorioService seModuloLaboratorioService) : BaseController
    {
        private readonly ISeCupoVendedorService _seCupoVendedorService = seCupoVendedorService;
        private readonly ISeModuloLaboratorioService _seModuloLaboratorioService = seModuloLaboratorioService;

        private readonly CupoVendedorVm.ConsultarCupoVendedor _consultarTodos = new()
        {
            CodigoModuloLaboratorio = null,
            IdLaboratorio = null,
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCupoVendedor.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                //Se consultan solo los roles activos
                var respuestaConsulta = await _seModuloLaboratorioService
                  .ConsultarTodos(new LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio.ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio()
                  {
                      IdLaboratorio = null,
                      SoloActivos = true
                  });
                //Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                //Procesa si la respuesa no tienen error en servicio
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
        [AccesosMenu(MenuCupoVendedor.CodigoMenu, MenuCupoVendedor.Permisos.Editar)]
        public async Task<IActionResult> EditarCupoVendedor(CupoVendedorVm.ConsultarCupoVendedor consultar)
        {
            try
            {
                var respuestaConsulta = await _seCupoVendedorService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarCupoVendedor", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new CupoVendedorVm()
                    {
                        IdModuloLaboratorio = consultar.IdLaboratorio ?? 0,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarCupoVendedor", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuCupoVendedor.CodigoMenu, MenuCupoVendedor.Permisos.Editar)]
        public async Task<IActionResult> EditarCupoVendedor([FromBody] CupoVendedorVm.CrearActualizarCupoVendedor actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<CupoVendedorVm.CrearActualizarCupoVendedor>();
                    var errores = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();
                    return Json(new { success = false, message = errores });
                    //return View("EditarCupoVendedor", actualizarVm);
                }

                var respuesta = await _seCupoVendedorService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    //return ProcesarError(respuesta);
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _seCupoVendedorService
                      .ConsultarPorId(new()
                      {
                          IdLaboratorio = actualizar.IdLaboratorio,
                          CodigoModuloLaboratorio = actualizar.CodigoModuloLaboratorio
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        //return ProcesarError(respuesta);
                        return Json(new { success = false, message = respuesta.Mensaje });
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return Json(new { success = true, message = respuestaConsulta.Respuesta.Mensaje });
                    //return View("EditarCupoVendedor", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<CupoVendedorVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return Json(new { success = false, message = respuesta.Mensaje });
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}