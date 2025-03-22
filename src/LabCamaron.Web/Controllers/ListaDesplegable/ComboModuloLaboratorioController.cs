using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    public class ComboModuloLaboratorioController(ISeModuloLaboratorioService seModuloLaboratorioService) : BaseController
    {
        private readonly ISeModuloLaboratorioService _seModuloLaboratorioService = seModuloLaboratorioService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarModuloLaboratorios(long? idLaboratorio, string textoContiene, bool esCascada = true)
        {
            try
            {
                if (esCascada && !idLaboratorio.HasValue)
                {
                    return Ok(Array.Empty<ComboBoxCatalogoModel>());
                }

                var consulta = await _seModuloLaboratorioService.ConsultarTodos(new()
                {
                    IdLaboratorio = idLaboratorio,
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = [.. consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Orden = x.Orden,
                            Id = x.Id,
                            Text = x.Nombre,
                        })
                        .OrderBy(e => e.Text)];

                    if (!string.IsNullOrEmpty(textoContiene))
                    {
                        resultado = [.. resultado
                            .Where(x => x.Text.Contains(textoContiene, StringComparison.OrdinalIgnoreCase))
                            .OrderBy(e => e.Text)];
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