using LabCamaronWeb.Infraestructura.Modelo;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class RespuestaPermisosVm
    {
        public RespuestaGenericaVm Respuesta { get; set; } = null!;
        public PermisoUsuarioVm? PermisoUsuario { get; set; }

        public RespuestaPermisosVm()
        {
        }

        public RespuestaPermisosVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Respuesta = new RespuestaGenericaVm(codigo, mensaje, metodoInvoca);
        }
    }
}