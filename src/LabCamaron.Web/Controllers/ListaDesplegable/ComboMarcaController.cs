using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboMarca")]
    public class ComboMarcaController(ISeMarcaService seMarcaService) : BaseController
    {
        private readonly ISeMarcaService _seMarcaService = seMarcaService;

        [HttpGet("ListarMarca")]
        [Authorize]
        public async Task<IActionResult> ListarMarca(string textoContiene)
        {
            try
            {
                var consulta = await _seMarcaService.ConsultarTodos(new()
                {
                    Activo = true
                });

                List<ComboBoxCatalogoModel> resultado = [];

                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombre,
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

        [HttpGet("ListarMarcaEdit")]
        [Authorize]
        public async Task<IActionResult> ListarMarcaEdit(long id, string textoContiene)
        {
            try
            {
                var consulta = await _seMarcaService.ConsultarTodos(new()
                {
                    Activo = true
                });

                List<ComboBoxCatalogoModel> resultado = [];

                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                      .Where(e => e.Id == (id != 0 ? id : e.Id))
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombre,
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