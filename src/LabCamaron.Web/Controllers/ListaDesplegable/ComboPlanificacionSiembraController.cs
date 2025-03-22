using LabCamaron.Web.Models;
using LabCamaronWeb.Dto.Produccion.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    [Route("ComboPlanificacionSiembra")]
    public class ComboPlanificacionSiembraController(ISePlanificacionSiembraService sePlanificacionSiembra,
        ISeModuloLaboratorioService _seModuloLaboratorioService) : BaseController
    {
        [HttpGet("ListarPlanificacionSiembra")]
        [Authorize]
        public async Task<IActionResult> ListarPlanificacionSiembra(string textoContiene)
        {
            try
            {
                var consulta = await sePlanificacionSiembra.ConsultarTodos(new()
                {

                });

                var modulos = await _seModuloLaboratorioService
                  .ConsultarTodos(new LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio.ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio()
                  {
                      SoloActivos = true
                  });

                List<PlanificacionSiembraVm> dataList = [];
                Random random = new Random(); // Instancia de Random
                int i = 0;
                foreach (var item in modulos.Resultados)
                {
                    i++;
                    var texto = "" + i;
                    dataList.Add(new PlanificacionSiembraVm()
                    {
                        Id = i,
                        NombreLaboratorio = item.NombreLaboratorio,
                        NombreModulo = item.Nombre,
                        Codigo = texto.PadLeft(2, '0'),
                        CantidadBruta = random.Next(2000, 5000), // Número entero entre 2000 y 5000
                        CantidadFacturada = random.Next(1000, 3000), // Número entero entre 1000 y 3000
                        Densidad = (decimal)Math.Round(random.NextDouble() * (3.5 - 1.5) + 1.5, 3),
                        FechaPlanificacion = DateTime.Now,
                    });
                }



                List<ComboBoxCatalogoModel> resultado = [];
                //if (consulta.Respuesta.EsExitosa)
                if (true)
                {
                    //resultado = consulta.Resultados!
                    resultado = dataList
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = $"{x.Codigo} - {x.FechaPlanificacion:dd/MM/yyyy}",
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
