using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Color.ColorVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeColorService
    {
        Task<RespuestaConsultaGenericaVm<ColorVm>> ConsultarPorId(ConsultarColor consultar);

        Task<RespuestaConsultasGenericaVm<ColorVm>> ConsultarTodos(ConsultarTodosColor consultar);

        Task<RespuestaGenericaVm> Crear(CrearColor crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarColor actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarColor eliminar);
    }
}