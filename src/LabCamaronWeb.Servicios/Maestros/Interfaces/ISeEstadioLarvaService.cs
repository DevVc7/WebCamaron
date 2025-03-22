using LabCamaronWeb.Dto.Maestros.EstadioLarva;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.EstadioLarva.EstadioLarvaVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEstadioLarvaService
    {
        Task<RespuestaConsultaGenericaVm<EstadioLarvaVm>> ConsultarPorId(ConsultarEstadioLarva consultar);

        Task<RespuestaConsultasGenericaVm<EstadioLarvaVm>> ConsultarTodos(ConsultarTodosEstadioLarva consultar);

        Task<RespuestaGenericaVm> Crear(CrearEstadioLarva crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarEstadioLarva actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarEstadioLarva eliminar);
    }
}