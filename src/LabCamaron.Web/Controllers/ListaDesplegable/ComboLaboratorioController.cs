using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboLaboratorio")]
    public class ComboLaboratorioController(ISeLaboratorioService seLaboratorioService) : BaseController
    {
        private readonly ISeLaboratorioService _seLaboratorioService = seLaboratorioService;

        [HttpGet("ListarLaboratorios")]
        [Authorize]
        public async Task<IActionResult> ListarLaboratorios(long? idEmpresa, string textoContiene)
        {
            try
            {
                var consulta = await _seLaboratorioService.ConsultarTodos(new()
                {
                    IdEmpresa = idEmpresa,
                    SoloActivos = true
                });

                List<ComboBoxLaboratorioModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxLaboratorioModel()
                        {
                            Orden = x.Orden,
                            Id = x.Id,
                            Text = x.Nombre,
                            IdEmpresa = x.IdEmpresa,
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

        [HttpGet("ListarLaboratoriosEdit")]
        [Authorize]
        public async Task<IActionResult> ListarLaboratoriosEdit(long idLaboratorio, string textoContiene)
        {
            try
            {
                var consulta = await _seLaboratorioService.ConsultarTodos(new()
                {
                    IdEmpresa = null,
                    SoloActivos = true
                });

                List<ComboBoxLaboratorioModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                      .Where(e => e.Id == (idLaboratorio != 0 ? idLaboratorio : e.Id))
                        .Select(x => new ComboBoxLaboratorioModel()
                        {
                            Orden = x.Orden,
                            Id = x.Id,
                            Text = x.Nombre,
                            IdEmpresa = x.IdEmpresa,
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