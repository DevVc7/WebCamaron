using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboEstadioLarva")]
    public class ComboEstadioLarvaController(ISeEstadioLarvaService seEstadioLarva) : BaseController
    {
        [HttpGet("ListarEstadioLarva")]
        [Authorize]
        public async Task<IActionResult> ListarEstadioLarva(string textoContiene)
        {
            try
            {
                var consulta = await seEstadioLarva.ConsultarTodos(new()
                {
                    SoloActivo = true
                });

                List<ComboBoxCatalogoModel> resultado = [];

                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = $"{x.Nombre} (Del {x.DiaDesde} Al {x.DiaHasta})",
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

                    return Ok(resultado);
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