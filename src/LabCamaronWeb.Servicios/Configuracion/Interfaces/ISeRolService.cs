using LabCamaronWeb.Dto.Configuracion.Rol;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Configuracion.Rol.RolVm;

namespace LabCamaronWeb.Servicios.Configuracion.Interfaces
{
    public interface ISeRolService
    {
        Task<RespuestaConsultaGenericaVm<RolVm>> ConsultarPorId(ConsultarRol consultar);

        Task<RespuestaConsultasGenericaVm<RolVm>> ConsultarTodos(ConsultarTodosRol consultar);

        Task<RespuestaGenericaVm> Crear(CrearRol crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarRol actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarRol eliminar);

        Task<RespuestaConsultaGenericaVm<PermisoRolVm>> ConsultarPermisos(PermisoRolVm.Consultar consultar);

        Task<RespuestaGenericaVm> ActualizarPermisos(PermisoRolVm.Actualizar actualizar);
    }
}