using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabCamaronWeb.Dto.Maestros.Dieta;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Servicios.Comercial.Interfaces;

namespace LabCamaron.Web.Controllers
{
    public class AsignacionClienteController(IAsignacionClienteService asignacionClienteService) : BaseController
    {
        private readonly AsignacionClienteVm.ConsultarTodosAsignacionCliente _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await asignacionClienteService
                    .ConsultarTodos(_consultarTodos);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesa si la respuesa no tienen error en servicio
                List<AsignacionClienteVm> asignacion;
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    asignacion = (respuestaConsulta.Resultados ?? []).ToList();
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }
                else
                {
                    asignacion = [];
                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta);
                }

                return View("Index", asignacion);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuAsignacionCliente.CodigoMenu, MenuAsignacionCliente.Permisos.Crear)]
        public IActionResult CrearAsignacionCliente()
        {
            var asignacionCliente = new AsignacionClienteVm.Detallado();
            return View("CrearAsignacionCliente", asignacionCliente);
        }
    }
}