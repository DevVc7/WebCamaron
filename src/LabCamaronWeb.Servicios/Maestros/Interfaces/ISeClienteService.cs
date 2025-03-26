using LabCamaronWeb.Dto.Maestros.Cliente;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Cliente.ClienteVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeClienteService
    {
        Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarCliente consultar);

        Task<RespuestaConsultasGenericaVm<ClienteVm>> ConsultarTodos(ConsultarTodosCliente consultar);

        Task<RespuestaGenericaVm> Crear(CrearCliente crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarCliente actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarCliente eliminar);

        Task<RespuestaGenericaVm> Activar(ActivarCliente activar);
    }
}