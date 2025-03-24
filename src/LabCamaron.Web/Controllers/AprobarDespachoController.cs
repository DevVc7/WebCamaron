using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabCamaronWeb.Dto.Comercial.AprobarDespacho;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;

namespace LabCamaron.Web.Controllers
{
    public class AprobarDespachoController(ISeAprobarDespachoService aprobarDespachoService) : BaseController
    {
        private readonly AprobarDespachoVm.ConsultarTodosAprobarDespacho _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAprobarDespacho.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await aprobarDespachoService
                    .ConsultarTodos(_consultarTodos);

                //// Procesa errores relacioados al problemas de comunicación
                //if (respuestaConsulta.Respuesta.TieneErrorServicio)
                //{
                //    return ProcesarError(respuestaConsulta.Respuesta);
                //}

                // Procesa si la respuesa no tienen error en servicio
                List<AprobarDespachoVm> asignacion = [];
                //if (respuestaConsulta.Respuesta.EsExitosa)
                //{
                //    asignacion = (respuestaConsulta.Resultados ?? []).ToList();
                //    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                //}
                //else
                //{
                //    asignacion = [];
                //    AsignarViewBagMensajeError(respuestaConsulta.Respuesta);
                //}

                return View("Index", asignacion);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAprobarDespacho.CodigoMenu, MenuAprobarDespacho.Permisos.Crear)]
        public IActionResult CrearAprobarDespacho()
        {
            var aprobarDespacho = new AprobarDespachoVm()
            {
                FechaAprobacion = DateTime.Now,
            };
            return View("CrearAprobarDespacho", aprobarDespacho);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuAprobarDespacho.CodigoMenu, MenuAprobarDespacho.Permisos.Crear)]
        public async Task<IActionResult> CrearAprobarDespacho([FromBody] AprobarDespachoVm.CrearAprobarDespacho crear)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var crearVm = crear.Mapear<AprobarDespachoVm>();
                    return View("CrearAprobarDespacho", crearVm);
                }

                var respuestaCrear = await aprobarDespachoService.Crear(crear);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaCrear.TieneErrorServicio)
                {
                    return Json(new { success = false, message = respuestaCrear.Mensaje });
                }

                // Procesamos si la respuesta es exitosa
                if (respuestaCrear.EsExitosa)
                {
                    AsignarViewBagMensajeExito(respuestaCrear.Mensaje);
                    return Json(new { success = true, message = respuestaCrear.Mensaje });
                }
                else
                {
                    AsignarViewBagMensajeError(respuestaCrear.Mensaje);
                    return Json(new { success = false, message = respuestaCrear.Mensaje });
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAprobarDespacho.CodigoMenu, MenuAprobarDespacho.Permisos.Editar)]
        public async Task<IActionResult> EditarAprobarDespacho(long id)
        {
            try
            {
                var respuestaConsulta = await aprobarDespachoService
                  .ConsultarPorId(new()
                  {
                      Id = id
                  });

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarAprobarDespacho", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new AprobarDespachoVm()
                    {
                        Id = id
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarAprobarDespacho", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}