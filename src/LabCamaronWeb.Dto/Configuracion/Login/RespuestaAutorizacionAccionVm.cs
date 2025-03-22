using LabCamaronWeb.Infraestructura.Modelo;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class RespuestaAutorizacionAccionVm
    {
        public RespuestaGenericaVm Respuesta { get; set; } = null!;
        public bool TieneAutorizacion { get; set; }

        public RespuestaAutorizacionAccionVm()
        { }

        public RespuestaAutorizacionAccionVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Respuesta = new RespuestaGenericaVm(codigo, mensaje, metodoInvoca);
        }
    }
}