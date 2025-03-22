using LabCamaronWeb.Dto.Maestros.Horas;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Horas.HorasVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeHorasService
    {
        Task<RespuestaConsultaGenericaVm<HorasVm>> ConsultarPorId(ConsultarHoras consultar);

        Task<RespuestaConsultasGenericaVm<HorasVm>> ConsultarTodos(ConsultarTodosHoras consultar);

        Task<RespuestaGenericaVm> Crear(CrearHoras crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarHoras actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarHoras eliminar);
    }
}