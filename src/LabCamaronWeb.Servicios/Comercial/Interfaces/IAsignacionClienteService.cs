using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Comercial.AsignacionCliente.AsignacionClienteVm;

namespace LabCamaronWeb.Servicios.Comercial.Interfaces
{
    public interface IAsignacionClienteService
    {
        Task<RespuestaConsultasGenericaVm<AsignacionClienteVm>> ConsultarTodos(ConsultarTodosAsignacionCliente consultar);
    }
}