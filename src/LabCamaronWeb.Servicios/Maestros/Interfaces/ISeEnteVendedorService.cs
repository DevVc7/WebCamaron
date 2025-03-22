using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.EnteVendedor.EnteVendedorVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEnteVendedorService
    {
        Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteVendedor crear);

        Task<RespuestaGenericaVm> Eliminar(EliminarEnteVendedor eliminar);
    }
}