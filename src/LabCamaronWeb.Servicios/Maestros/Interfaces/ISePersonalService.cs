using LabCamaronWeb.Dto.Maestros.Personal;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Personal.PersonalVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISePersonalService
    {
        Task<RespuestaConsultaGenericaVm<PersonalVm>> ConsultarPorId(ConsultarPersonal consultar);

        Task<RespuestaConsultasGenericaVm<PersonalVm>> ConsultarTodos(ConsultarTodosPersonal consultar);

        Task<RespuestaGenericaVm> Crear(CrearPersonal crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarPersonal actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarPersonal eliminar);

        Task<RespuestaGenericaVm> Activar(ActivarPersonal activar);
    }
}