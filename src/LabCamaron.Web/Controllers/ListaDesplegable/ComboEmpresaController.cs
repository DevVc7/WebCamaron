using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    public class ComboEmpresaController(ISeEmpresaService seEmpresaService) : BaseController
    {
        private readonly ISeEmpresaService _seEmpresaService = seEmpresaService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarEmpresas(long? idEmpresa, string textoContiene)
        {
            try
            {
                var consulta = await _seEmpresaService.ConsultarTodos(new()
                {
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