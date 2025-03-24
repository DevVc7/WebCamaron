using LabCamaronWeb.Dto.Parametrizacion.Ciudad;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.Ciudad.CiudadVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeCiudadService
    {
        Task<RespuestaConsultaGenericaVm<CiudadVm>> ConsultarPorId(ConsultarCiudad consultar);

        Task<RespuestaConsultasGenericaVm<CiudadVm>> ConsultarTodos(ConsultarTodosCiudad consultar);
    }
}