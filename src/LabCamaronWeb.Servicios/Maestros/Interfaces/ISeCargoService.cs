using LabCamaronWeb.Dto.Maestros.Cargo;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Cargo.CargoVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeCargoService
    {
        Task<RespuestaConsultaGenericaVm<CargoVm>> ConsultarPorId(ConsultarCargo consultar);

        Task<RespuestaConsultasGenericaVm<CargoVm>> ConsultarTodos(ConsultarTodosCargo consultar);

        Task<RespuestaGenericaVm> Crear(CrearCargo crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarCargo actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarCargo eliminar);
    }
}