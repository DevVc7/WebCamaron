﻿using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Dto.Maestros.CesionCupoVendedor;
using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Comercial;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Maestros;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers

{
    public class CesionVendedorController(ISeCupoVendedorService seCupoVendedorService) : BaseController
    {
        private readonly ISeCupoVendedorService _seCupoVendedorService = seCupoVendedorService;

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
                // Se consultan solo los roles activos
                var respuestaConsulta = await _seCupoVendedorService
                  .ConsultarTodos(_consultarTodos);

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
            catch (Exception e)
            {
                throw new Exception(e.ToString()) ;
                return ProcesarError();
            }
        }
    }
}
