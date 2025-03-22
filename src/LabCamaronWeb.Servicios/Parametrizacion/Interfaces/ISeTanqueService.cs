using LabCamaronWeb.Dto.Parametrizacion.Tanque;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.Tanque.TanqueVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeTanqueService
    {
        Task<RespuestaConsultaGenericaVm<TanqueVm>> ConsultarPorId(ConsultarTanque consultar);

        Task<RespuestaConsultasGenericaVm<TanqueVm>> ConsultarTodos(ConsultarTodosTanque consultar);

        Task<RespuestaGenericaVm> Crear(CrearTanque crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarTanque actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarTanque eliminar);
    }
}