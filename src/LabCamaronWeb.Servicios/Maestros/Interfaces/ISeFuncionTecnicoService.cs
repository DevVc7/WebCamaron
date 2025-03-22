using LabCamaronWeb.Dto.Maestros.FuncionTecnico;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.FuncionTecnico.FuncionTecnicoVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeFuncionTecnicoService
    {
        Task<RespuestaConsultaGenericaVm<FuncionTecnicoVm>> ConsultarPorId(ConsultarFuncionTecnico consultar);

        Task<RespuestaConsultasGenericaVm<FuncionTecnicoVm>> ConsultarTodos(ConsultarTodosFuncionTecnico consultar);

        Task<RespuestaGenericaVm> Crear(CrearFuncionTecnico crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarFuncionTecnico actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarFuncionTecnico eliminar);
    }
}