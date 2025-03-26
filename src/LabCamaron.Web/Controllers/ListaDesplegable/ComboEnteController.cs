using LabCamaron.Web.Models;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Constantes.Procesos;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using LabCamaronWeb.Infraestructura.Extensiones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabCamaron.Web.Controllers.ListaDesplegable
{
    public class ComboEnteController(ISeClienteService seClienteServices, ISePersonalService sePersonalService, IHttpContextAccessor contextAccessor) : BaseController
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarEntesVendedor(long? idEmpresa, string textoContiene)
        {
            try
            {
                var consulta = await sePersonalService.ConsultarTodos(new()
                {
                    CodigoCargo = ConstantesCargo.Vendedor,
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombres
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarEntesClientes(long? idEmpresa, string textoContiene)
        {
            try
            {
                var consulta = await seClienteServices.ConsultarTodos(new()
                {
                    SoloActivos = true
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.RazonSocial,
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarEntesTecnicos(long? idModulo, string textoContiene)
        {
            try
            {
                var consulta = await sePersonalService.ConsultarTodos(new()
                {
                    CodigoCargo = ConstantesCargo.Tecnico,
                    SoloActivos = true,
                    IdModulo = idModulo,
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombres,
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarEntesVendedorUsuarioSistema(string textoContiene)
        {
            try
            {
                var codigoUsuario = HttpContext.Session.Obtener<string>(SesionConstantes.CodigoUsuario);
                var consulta = await sePersonalService.ConsultarTodos(new()
                {
                    CodigoCargo = ConstantesCargo.Vendedor,
                    SoloActivos = true,
                    UsuarioSistema = codigoUsuario
                });

                List<ComboBoxCatalogoModel> resultado = [];
                if (consulta.Respuesta.EsExitosa)
                {
                    resultado = consulta.Resultados!
                        .Select(x => new ComboBoxCatalogoModel()
                        {
                            Id = x.Id,
                            Text = x.Nombres,
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