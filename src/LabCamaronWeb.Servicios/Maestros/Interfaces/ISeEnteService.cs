using LabCamaronWeb.Dto.Maestros.Ente;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Ente.EnteVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeEnteService
    {
        Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarEnte consultar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTodos(ConsultarTodosEnte consultar);

        Task<RespuestaGenericaVm> Crear(CrearEnte crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarEnte actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarEnte eliminar);

        Task<RespuestaGenericaVm> Activar(ActivarEnte activar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarVendedores(ConsultarTodosEnte consultar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarClientes(ConsultarTodosEnte consultar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarPersonal(ConsultarTodosEnte consultar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTecnicos(ConsultarTodosEnteTecnico consultar);

        Task<RespuestaConsultasGenericaVm<EnteVm>> ConsultarTodosSinRol(ConsultarTodosEnteSinRol consultar);
    }
}