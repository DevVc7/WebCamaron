using LabCamaronWeb.Dto.Parametrizacion.Provincia;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.Provincia.ProvinciaVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeProvinciaService
    {
        Task<RespuestaConsultaGenericaVm<ProvinciaVm>> ConsultarPorId(ConsultarProvincia consultar);

        Task<RespuestaConsultasGenericaVm<ProvinciaVm>> ConsultarTodos(ConsultarTodosProvincia consultar);
    }
}