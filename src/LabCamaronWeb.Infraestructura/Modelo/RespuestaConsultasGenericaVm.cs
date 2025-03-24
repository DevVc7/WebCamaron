using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Modelo
{
    public record RespuestaConsultasGenericaVm<T>
    {
        public RespuestaGenericaVm Respuesta { get; set; } = null!;
        public IEnumerable<T>? Resultados { get; set; }
        public RespuestaConsultasGenericaVm()
        {
        }

        public RespuestaConsultasGenericaVm(RespuestaGenericaVm respuesta)
        {
            Respuesta = respuesta;
        }
        public RespuestaConsultasGenericaVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Respuesta = new RespuestaGenericaVm(codigo, mensaje, metodoInvoca);
        }
    }
}