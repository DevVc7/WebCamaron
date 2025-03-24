using LabCamaronWeb.Dto.Configuracion.Usuario;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Configuracion.Usuario.UsuarioVm;

namespace LabCamaronWeb.Servicios.Configuracion.Interfaces
{
    public interface ISeUsuarioService
    {
        Task<RespuestaConsultaGenericaVm<UsuarioVm>> ConsultarUsuario(ConsultarUsuario consultar);

        Task<RespuestaConsultasGenericaVm<UsuarioVm>> ConsultarUsuarios(ConsultarTodos consultar);

        Task<RespuestaGenericaVm> CrearUsuario(CrearUsuario crear);

        Task<RespuestaGenericaVm> EliminarUsuario(EliminarUsuario eliminar);

        Task<RespuestaGenericaVm> ActualizarUsuario(ActualizarUsuario actualizar);

        Task<RespuestaGenericaVm> ActualizarClaveUsuario(ActualizarClaveUsuario actualizar);

        Task<RespuestaGenericaVm> ReestablecerClaveUsuario(ReestablecerContrasenia actualizar);

        Task<RespuestaGenericaVm> ActualizarRolesUsuario(ActualizarRol actualizar);

        Task<RespuestaGenericaVm> ActualizarModulosLaboratorioUsuario(ActualizarModuloLaboratorio actualizar);
    }
}