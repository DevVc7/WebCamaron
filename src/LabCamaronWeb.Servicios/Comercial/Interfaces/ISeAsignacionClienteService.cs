using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Comercial.AsignacionCliente.AsignacionClienteVm;

namespace LabCamaronWeb.Servicios.Comercial.Interfaces
{
    public interface ISeAsignacionClienteService
    {
        Task<RespuestaConsultasGenericaVm<AsignacionClienteVm>> ConsultarTodos(ConsultarTodosAsignacionCliente consultar);
        Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarAsignacionCliente consultar);
        Task<RespuestaGenericaVm> Crear(CrearAsignacionCliente crear);
        Task<RespuestaGenericaVm> Actualizar(ActualizarAsignacionCliente actualizar);
        Task<RespuestaGenericaVm> Eliminar(EliminarAsignacionCliente eliminar);
    }
}