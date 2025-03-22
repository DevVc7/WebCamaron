using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboTipoUnidadMedida")]
    public class ComboTipoUnidadMedidaController(ISeUnidadMedidaService seUnidadMedidaService) : BaseController
    {
        private readonly ISeUnidadMedidaService _seUnidadMedidaService = seUnidadMedidaService;

        [HttpGet("ListarTiposUnidadMedida")]
        [Authorize]
        public async Task<IActionResult> ListarTiposUnidadMedida(string? codigoTipoUnidad, string textoContiene)
        {
            try
            {
                var consulta = await _seUnidadMedidaService.ConsultarTiposUnidadMedida();

                List<ComboBoxStringModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                      .Where(e => e.Codigo == (!string.IsNullOrEmpty(codigoTipoUnidad) ? codigoTipoUnidad : e.Codigo))
                        .Select(x => new ComboBoxStringModel()
                        {
                            Id = x.Codigo,
                            Text = x.Descripcion,
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

        [HttpGet("ListarUnidadesMedida")]
        [Authorize]
        public async Task<IActionResult> ListarUnidadesMedida(string codigoTipoUnidad, string textoContiene, string? codigoUnidadMedida)
        {
            try
            {
                var consulta = await _seUnidadMedidaService.ConsultarTodos(new()
                {
                    SoloActivos = true,
                });

                List<ComboBoxUnidadMedidaModel> resultado = [];

                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                      .Where(x => x.Tipo == (!string.IsNullOrEmpty(codigoTipoUnidad) ? codigoTipoUnidad : x.Tipo) && x.Codigo == (!string.IsNullOrEmpty(codigoUnidadMedida) ? codigoUnidadMedida : x.Codigo))
                        .Select(x => new ComboBoxUnidadMedidaModel()
                        {
                            Id = x.Codigo,
                            Text = x.Nombre,
                            TipoUnidad = x.Tipo,
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