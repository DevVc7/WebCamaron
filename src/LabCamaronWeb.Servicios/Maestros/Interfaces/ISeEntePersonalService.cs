using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.EntePersonal.EntePersonalVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEntePersonalService
    {
        Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEntePersonal crear);

        Task<RespuestaGenericaVm> Eliminar(EliminarEntePersonal eliminar);
    }
}