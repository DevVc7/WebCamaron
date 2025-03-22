using LabCamaronWeb.Dto.Parametrizacion.Laboratorio;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.Laboratorio.LaboratorioVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeLaboratorioService
    {
        Task<RespuestaConsultaGenericaVm<LaboratorioVm>> ConsultarPorId(ConsultarLaboratorio consultar);

        Task<RespuestaConsultasGenericaVm<LaboratorioVm>> ConsultarTodos(ConsultarTodosLaboratorio consultar);

        Task<RespuestaGenericaVm> Crear(CrearLaboratorio crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarLaboratorio actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarLaboratorio eliminar);
    }
}