using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Dto.Produccion.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Constantes.Menus;
using LabCamaronWeb.Infraestructura.Constantes.Menus.Produccion;
using LabCamaronWeb.Infraestructura.Utilidades.Mapeador;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using LabCamaronWeb.Servicios.Produccion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class PlanificacionSiembraController(ISePlanificacionSiembraService sePlanificacionSiembraService, ISeLaboratorioService seLaboratorioService,
        ISeModuloLaboratorioService seModuloLaboratorioService) : BaseController
    {
        private readonly ISePlanificacionSiembraService _sePlanificacionSiembraService = sePlanificacionSiembraService;
        private readonly ISeLaboratorioService _seLaboratorioService = seLaboratorioService;
        private readonly ISeModuloLaboratorioService _seModuloLaboratorioService = seModuloLaboratorioService;

        private readonly PlanificacionSiembraVm.ConsultarTodosPlanificacionSiembra _consultarTodos = new()
        {
            Activo = true
        };

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, PermisoGeneral.Ver)]
        public async Task<IActionResult> Index(bool mostrarMensajeExito = false)
        {
            try
            {
                // Se consultan solo los roles activos
                var respuestaConsulta = await _sePlanificacionSiembraService
                  .ConsultarTodos(_consultarTodos);
                var laboratorios = await _seLaboratorioService
                  .ConsultarTodos(new LabCamaronWeb.Dto.Parametrizacion.Laboratorio.LaboratorioVm.ConsultarTodosLaboratorio() { });
                var modulos = await _seModuloLaboratorioService
                  .ConsultarTodos(new LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio.ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio()
                  {
                  });
                Random random = new Random(); // Instancia de Random

                var dataList = new List<PlanificacionSiembraVm>()
                {
                };
                var i = 0;
                foreach (var item in modulos.Resultados)
                {
                    i++;
                    var texto = "" + i;
                    dataList.Add(new PlanificacionSiembraVm()
                    {
                        NombreLaboratorio = item.NombreLaboratorio,
                        NombreModulo = item.Nombre,
                        Codigo = texto.PadLeft(2, '0'),
                        CantidadBruta = random.Next(2000, 5000), // Número entero entre 2000 y 5000
                        CantidadFacturada = random.Next(1000, 3000), // Número entero entre 1000 y 3000
                        Densidad = (decimal)Math.Round(random.NextDouble() * (3.5 - 1.5) + 1.5, 3),
                        FechaPlanificacion = DateTime.Now,
                    });
                }

                respuestaConsulta.Resultados = dataList;
                // Procesa errores relacioados al problemas de comunicación
                //if (respuestaConsulta.Respuesta.TieneErrorServicio)
                //{
                //  return ProcesarError(respuestaConsulta.Respuesta);
                //}

                // Procesa si la respuesa no tienen error en servicio
                var roles = respuestaConsulta.Respuesta.EsExitosa
                  ? respuestaConsulta.Resultados : [];
                roles = dataList;

                if (mostrarMensajeExito)
                {
                    AsignarViewBagMensajeExito(respuestaConsulta.Respuesta);
                }

                //AsignarViewBagMensajeError(respuestaConsulta.Respuesta);

                return View("Index", roles);
            }
            catch
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Crear)]
        public IActionResult CrearPlanificacionSiembra()
        {
            var rolNuevo = new PlanificacionSiembraVm();
            return View("CrearPlanificacionSiembra", rolNuevo);
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Crear)]
        public async Task<IActionResult> CrearPlanificacionSiembra([FromForm] PlanificacionSiembraVm.CrearPlanificacionSiembra crear)
        {
            try
            {
                AsignarViewBagMensajeExito("Se ha ingresado con exito");
                return await Index(true);

                // Validamos el modelo
                //        if (!ModelState.IsValid)
                //{
                //  AsignarViewBagMensajeError(ModelState);
                //  var crearVm = crear.Mapear<PlanificacionSiembraVm>();
                //  return View("CrearPlanificacionSiembra", crearVm);
                //}

                //var respuestaCrear = await _sePlanificacionSiembraService.Crear(crear);

                //// Procesa errores relacioados al problemas de comunicación
                //if (respuestaCrear.TieneErrorServicio)
                //{
                //  return ProcesarError(respuestaCrear);
                //}

                //// Procesamos si la respuesta es exitosa
                //if (respuestaCrear.EsExitosa)
                //{
                //  return await Index(true);
                //}
                //else
                //{
                //  AsignarViewBagMensajeError(respuestaCrear.Mensaje);

                //  var rolVm = crear.Mapear<PlanificacionSiembraVm>();
                //  return View("CrearPlanificacionSiembra", rolVm);
                //}
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpGet]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Editar)]
        public async Task<IActionResult> EditarPlanificacionSiembra(PlanificacionSiembraVm.ConsultarPlanificacionSiembra consultar)
        {
            try
            {
                var respuestaConsulta = await _sePlanificacionSiembraService
                  .ConsultarPorId(consultar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaConsulta.Respuesta);
                }

                // Procesamos el error
                if (respuestaConsulta.Respuesta.EsExitosa)
                {
                    return View("EditarPlanificacionSiembra", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = new PlanificacionSiembraVm()
                    {
                        Id = consultar.Id ?? 0,
                    };

                    AsignarViewBagMensajeError(respuestaConsulta.Respuesta.Mensaje);
                    return View("EditarPlanificacionSiembra", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Editar)]
        public async Task<IActionResult> EditarPlanificacionSiembra(PlanificacionSiembraVm.ActualizarPlanificacionSiembra actualizar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);
                    var actualizarVm = actualizar.Mapear<PlanificacionSiembraVm>();
                    return View("EditarPlanificacionSiembra", actualizarVm);
                }

                var respuesta = await _sePlanificacionSiembraService
                  .Actualizar(actualizar);

                // Procesa errores relacioados al problemas de comunicación
                if (respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuesta);
                }

                // Procesamos el error
                if (respuesta.EsExitosa)
                {
                    var respuestaConsulta = await _sePlanificacionSiembraService
                      .ConsultarPorId(new()
                      {
                          Id = actualizar.Id
                      });

                    // Procesa errores relacioados al problemas de comunicación
                    if (respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuesta);
                    }

                    AsignarViewBagMensajeExito(respuesta.Mensaje);

                    return View("EditarPlanificacionSiembra", respuestaConsulta.Resultado);
                }
                else
                {
                    var rolVm = actualizar.Mapear<PlanificacionSiembraVm>();
                    AsignarViewBagMensajeError(respuesta.Mensaje);
                    return View("EditarPlanificacionSiembra", rolVm);
                }
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }

        [HttpPost]
        [Authorize]
        [AccesosMenu(MenuPlanificacionSiembra.CodigoMenu, MenuPlanificacionSiembra.Permisos.Eliminar)]
        public async Task<IActionResult> EliminarPlanificacionSiembra([FromForm] PlanificacionSiembraVm.EliminarPlanificacionSiembra eliminar)
        {
            try
            {
                // Validamos el modelo
                if (!ModelState.IsValid)
                {
                    AsignarViewBagMensajeError(ModelState);

                    // si existe un error de modelo, retornamos la vista con los roles
                    var respuestaConsultaError = await _sePlanificacionSiembraService
                      .ConsultarTodos(_consultarTodos);

                    if (respuestaConsultaError.Respuesta.TieneErrorServicio)
                    {
                        return ProcesarError(respuestaConsultaError.Respuesta);
                    }

                    return View("Index", respuestaConsultaError.Resultados);
                }

                // Procesamos la eliminación
                var respuestaEliminar = await _sePlanificacionSiembraService
                  .Eliminar(eliminar);

                if (respuestaEliminar.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                // Procesamos la respuesta de consulta
                var respuestaConsulta = await _sePlanificacionSiembraService
                  .ConsultarTodos(_consultarTodos);

                if (respuestaConsulta.Respuesta.TieneErrorServicio)
                {
                    return ProcesarError(respuestaEliminar);
                }

                AsignarViewBagMensajeError(respuestaEliminar);
                AsignarViewBagMensajeExito(respuestaEliminar);

                return View("Index", respuestaConsulta.Resultados);
            }
            catch (Exception)
            {
                return ProcesarError();
            }
        }
    }
}