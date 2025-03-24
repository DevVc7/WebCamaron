using LabCamaronWeb.Dto.Comercial.AprobarDespacho;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Comercial.AprobarDespacho.AprobarDespachoVm;

namespace LabCamaronWeb.Servicios.Comercial.Interfaces
{
    public interface ISeAprobarDespachoService
    {

        Task<RespuestaConsultasGenericaVm<AprobarDespachoVm>> ConsultarTodos(ConsultarTodosAprobarDespacho consultar);
        Task<RespuestaConsultaGenericaVm<AprobarDespachoVm>> ConsultarPorId(ConsultarAprobarDespacho consultar);
        Task<RespuestaGenericaVm> Crear(CrearAprobarDespacho crear);
    }
}
