using LabCamaronWeb.Dto.Maestros.ParametroAmbiental;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.ParametroAmbiental.ParametroAmbientalVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeParametroAmbientalService
    {
        Task<RespuestaConsultaGenericaVm<ParametroAmbientalVm>> ConsultarPorId(ConsultarParametroAmbiental consultar);

        Task<RespuestaConsultasGenericaVm<ParametroAmbientalVm>> ConsultarTodos(ConsultarTodosParametroAmbiental consultar);

        Task<RespuestaGenericaVm> Crear(CrearParametroAmbiental crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarParametroAmbiental actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarParametroAmbiental eliminar);
    }
}