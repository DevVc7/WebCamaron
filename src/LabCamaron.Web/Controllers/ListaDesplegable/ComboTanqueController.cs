using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboTanque")]
    public class ComboTanqueController(ISeTanqueService _seTanqueService) : BaseController
    {
        [HttpGet("ListarTanques")]
        [Authorize]
        public async Task<IActionResult> ListarTanques(long? idModulo, string textoContiene)
        {
            try
            {
                var consulta = await _seTanqueService.ConsultarTodos(new()
                {
                    IdModulo = idModulo,
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Orden = x.Orden,
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