using LabCamaronWeb.Dto.Maestros.Insumos;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Insumos.InsumosVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeInsumosService
    {
        Task<RespuestaConsultaGenericaVm<InsumosVm>> ConsultarPorId(ConsultarInsumos consultar);

        Task<RespuestaConsultasGenericaVm<InsumosVm>> ConsultarTodos(ConsultarTodosInsumos consultar);

        Task<RespuestaGenericaVm> Crear(CrearInsumos crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarInsumos actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarInsumos eliminar);
    }
}