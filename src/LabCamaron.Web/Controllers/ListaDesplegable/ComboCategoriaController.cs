using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboCategoria")]
    public class ComboCategoriaController(ISeCategoriaService seCategoriaService) : BaseController
    {
        private readonly ISeCategoriaService _seCategoriaService = seCategoriaService;

        [HttpGet("ListarCategoria")]
        [Authorize]
        public async Task<IActionResult> ListarCategoria(string textoContiene)
        {
            try
            {
                var consulta = await _seCategoriaService.ConsultarTodos(new()
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

        [HttpGet("ListarCategoriaEdit")]
        [Authorize]
        public async Task<IActionResult> ListarCategoriaEdit(long id, string textoContiene)
        {
            try
            {
                var consulta = await _seCategoriaService.ConsultarTodos(new()
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