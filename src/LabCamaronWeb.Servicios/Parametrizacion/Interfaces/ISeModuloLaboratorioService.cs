using LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio.ModuloLaboratorioVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeModuloLaboratorioService
    {
        Task<RespuestaConsultaGenericaVm<ModuloLaboratorioVm>> ConsultarPorId(ConsultarModuloLaboratorio consultar);

        Task<RespuestaConsultasGenericaVm<ModuloLaboratorioVm>> ConsultarTodos(ConsultarTodosModuloLaboratorio consultar);

        Task<RespuestaGenericaVm> Crear(CrearModuloLaboratorio crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarModuloLaboratorio actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarModuloLaboratorio eliminar);
    }
}