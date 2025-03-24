using LabCamaronWeb.Dto.Produccion.Siembra;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Produccion.Siembra.SiembraVm;

namespace LabCamaronWeb.Servicios.Produccion.Interfaces
{
    public interface ISeSiembraService
    {
        Task<RespuestaConsultaGenericaVm<SiembraVm>> ConsultarPorId(ConsultarSiembra consultar);

        Task<RespuestaConsultasGenericaVm<SiembraVm>> ConsultarTodos(ConsultarTodosSiembra consultar);

        Task<RespuestaGenericaVm> Crear(CrearSiembra crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarSiembra actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarSiembra eliminar);
    }
}