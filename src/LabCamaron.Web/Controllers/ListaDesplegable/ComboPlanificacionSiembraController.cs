using LabCamaron.Web.Models;
using LabCamaronWeb.Dto.Produccion.Siembra;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboPlanificacionSiembra")]
    public class ComboPlanificacionSiembraController(ISePlanificacionSiembraService sePlanificacionSiembra) : BaseController
    {
        [HttpGet("ListarPlanificacionSiembra")]
        [Authorize]
        public async Task<IActionResult> ListarPlanificacionSiembra(string textoContiene)
        {
            try
            {
                var consulta = await sePlanificacionSiembra.ConsultarTodos(new()
                {
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    //resultado = consulta.Resultados!
                    resultado = (consulta.Resultados ?? [])
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = $"{x.Codigo}",
                        })
                        .OrderBy(e => e.Text)
                        .ToList();

                    if (!string.IsNullOrEmpty(textoContiene))
                    {
                        resultado = resultado
                          .Where(x => x.Text.Contains(textoContiene, StringComparison.OrdinalIgnoreCase))
                          .OrderBy(e => e.Text)
                          .ToList();
                    }

                    return Ok(resultado.OrderBy(x => x.Text));
                }
                else
                {
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ha ocurrido un error");
            }
        }
    }
}
