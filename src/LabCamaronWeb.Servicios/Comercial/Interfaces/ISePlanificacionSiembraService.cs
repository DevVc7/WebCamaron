using LabCamaronWeb.Dto.Comercial.PlanificacionSiembra;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Comercial.PlanificacionSiembra.PlanificacionSiembraVm;

namespace LabCamaronWeb.Servicios.Comercial.Interfaces
{
    public interface ISePlanificacionSiembraService
    {
        Task<RespuestaConsultaGenericaVm<PlanificacionSiembraVm>> ConsultarPorId(ConsultarPlanificacionSiembraId consultar);

        Task<RespuestaConsultasGenericaVm<PlanificacionSiembraVm>> ConsultarTodos(ConsultarTodosPlanificacionSiembra consultar);
        Task<RespuestaConsultasGenericaVm<PlanificacionSiembraVm>> ConsultarRangoFecha(ConsultarTodosRangoFecha consultar);

        Task<RespuestaGenericaVm> Crear(CrearPlanificacionSiembra crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarPlanificacionSiembra actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarPlanificacionSiembra eliminar);
    }
}