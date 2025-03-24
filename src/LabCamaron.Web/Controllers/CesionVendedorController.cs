using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Dto.Maestros.CesionCupoVendedor;
using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers

{
    public class CesionVendedorController(ISeCupoVendedorService seCupoVendedorService, ISeModuloLaboratorioService seModuloLaboratorioService) : BaseController
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
        [AccesosMenu(MenuCesionCupoVendedor.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(string mensajeExito = "", string mensajeError = "")
        {
            try
            {
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

                if (!string.IsNullOrEmpty(mensajeExito))
                {
                    AsignarViewBagMensajeExito(mensajeExito);
                }

                if (!string.IsNullOrEmpty(mensajeError))
                {
                    AsignarViewBagMensajeError(mensajeError);
                }

                AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("Index", roles);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString()) ;
                return ProcesarError();
            }
        }
    }
}
