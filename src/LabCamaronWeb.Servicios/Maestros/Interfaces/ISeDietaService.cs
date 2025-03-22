using LabCamaronWeb.Dto.Maestros.Dieta;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Dieta.DietaVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeDietaService
    {
        Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarDieta consultar);

        Task<RespuestaConsultasGenericaVm<DietaVm>> ConsultarTodos(ConsultarDietas consultar);

        Task<RespuestaGenericaVm> Crear(CrearDieta crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarDieta actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarDieta eliminar);
    }
}