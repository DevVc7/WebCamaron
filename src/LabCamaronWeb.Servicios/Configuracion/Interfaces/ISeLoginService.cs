using LabCamaronWeb.Dto.Configuracion.Login;

namespace LabCamaronWeb.Servicios.Configuracion.Interfaces
{
    public interface ISeLoginService
    {
        Task<RespuestaLoginVm> Login(LoginVm login);

        Task<RespuestaAutorizacionAccionVm> AutorizarAccion(DetallePermisoVm.AutorizarAccion autorizar);

        Task<RespuestaPermisosVm> ObtenerPermisosSesion(DetallePermisoVm.ConsultarPermisos consultar);
    }
}