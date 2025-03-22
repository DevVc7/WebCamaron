using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.EnteTecnico.EnteTecnicoVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEnteTecnicoService
    {
        Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteTecnico crear);

        Task<RespuestaGenericaVm> Eliminar(EliminarEnteTecnico eliminar);
    }
}