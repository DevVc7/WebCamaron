using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    public class ComboColorController(ISeColorService seColorService) : BaseController
    {
        private readonly ISeColorService _seColorService = seColorService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarColor(string textoContiene)
        {
            try
            {
                var consulta = await _seColorService.ConsultarTodos(new()
                {
                    Activo = true
                });

                List<ComboBoxColorModel> resultado = [];

                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxColorModel()
                        {
                            Id = x.Id,
                            Text = x.Nombre,
                            CodigoHexadecimal = x.CodigoHexadecimal
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