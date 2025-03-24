using LabCamaronWeb.Infraestructura.Modelo;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class RespuestaLoginVm
    {
        public RespuestaGenericaVm Respuesta { get; set; } = null!;
        public string IdentificadorSesion { get; set; } = string.Empty;
        public PermisoUsuarioVm Permisos { get; set; } = null!;
        public JwtModelVm Jwt { get; set; } = null!;

        public RespuestaLoginVm()
        { }

        public RespuestaLoginVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Respuesta = new RespuestaGenericaVm(codigo, mensaje, metodoInvoca);
        }
    }
}