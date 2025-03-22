using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    public class ComboCargoController(ISeCargoService seCargo) : BaseController
    {
        private readonly ISeCargoService _seCargo = seCargo;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarCargos(string textoContiene)
        {
            try
            {
                var consulta = await _seCargo.ConsultarTodos(new());

                List<ComboBoxStringModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = [.. consulta.Resultados!
                        .Select(x => new ComboBoxStringModel()
                        {
                            Orden = x.Orden,
                            Id = x.Id.ToString(),
                            Text = x.Nombre,
                        })
                        .OrderBy(e => e.Text)];

                    if (!string.IsNullOrEmpty(textoContiene))
                    {
                        resultado = [.. resultado.Where(x => x.Text.Contains(textoContiene, StringComparison.OrdinalIgnoreCase)).OrderBy(e => e.Orden)];
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