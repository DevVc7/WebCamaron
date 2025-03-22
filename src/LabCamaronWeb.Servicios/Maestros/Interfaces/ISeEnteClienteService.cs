using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.EnteCliente.EnteClienteVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEnteClienteService
    {
        Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteCliente crear);

        Task<RespuestaGenericaVm> Eliminar(EliminarEnteCliente eliminar);
    }
}