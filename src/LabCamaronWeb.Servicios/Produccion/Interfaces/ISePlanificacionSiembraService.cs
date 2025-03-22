using LabCamaronWeb.Dto.Produccion.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Produccion.PlanificacionSiembra.PlanificacionSiembraVm;

namespace LabCamaronWeb.Servicios.Produccion.Interfaces
{
    public interface ISePlanificacionSiembraService
    {
        Task<RespuestaConsultaGenericaVm<PlanificacionSiembraVm>> ConsultarPorId(ConsultarPlanificacionSiembra consultar);

        Task<RespuestaConsultasGenericaVm<PlanificacionSiembraVm>> ConsultarTodos(ConsultarTodosPlanificacionSiembra consultar);

        Task<RespuestaGenericaVm> Crear(CrearPlanificacionSiembra crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarPlanificacionSiembra actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarPlanificacionSiembra eliminar);
    }
}