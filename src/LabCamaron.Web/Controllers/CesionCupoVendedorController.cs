using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Dto.Maestros.CesionCupoVendedor;
using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Servicios.Comercial.Servicios;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers

{
    public class CesionCupoVendedorController(ISeModuloCesionService seCupoVendedorService) : BaseController
    {
        private readonly AsignacionClienteVm.ConsultarTodosAsignacionCliente _consultarTodos = new()
        {
            SoloActivos = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuCesionCupoVendedor.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await seCupoVendedorService
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
            catch (Exception e)
            {
                throw new Exception(e.ToString()) ;
                return ProcesarError();
            }
        }

        //[HttpGet]
        //[Authorize]
        //[AccesosMenu(MenuCesionCupoVendedor.CodigoMenu, MenuCesionCupoVendedor.Permisos.Editar)]
        //public async Task<IActionResult> EditarCesionCupoVendedor(long id)
        //{
        //    try
        //    {
        //        var respuestaConsulta = await asignacionClienteService
        //          .ConsultarPorId(new()
        //          {
        //              Id = id
        //          });

        //        // Procesa errores relacioados al problemas de comunicación
        //        if (respuestaConsulta.Respuesta.TieneErrorServicio)
        //        {
        //            return ProcesarError(respuestaConsulta.Respuesta);
        //        }

        //        // Procesamos el error
        //        if (respuestaConsulta.Respuesta.EsExitosa)
        //        {
        //            return View("EditarAsignacionCliente", respuestaConsulta.Resultado);
        //        }
        //        else
        //        {
        //            var rolVm = new AsignacionClienteVm()
        //            {
        //                Id = id
        //            };

        //            AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
        //            return View("EditarAsignacionCliente", rolVm);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return ProcesarError();
        //    }
        //}
    }
}
