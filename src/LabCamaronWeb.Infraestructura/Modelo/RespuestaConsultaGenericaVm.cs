using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Modelo
{
    public record RespuestaConsultaGenericaVm<T>
    {
        public RespuestaGenericaVm Respuesta { get; set; } = null!;
        public T? Resultado { get; set; }

        public RespuestaConsultaGenericaVm()
        {
        }

        public RespuestaConsultaGenericaVm(RespuestaGenericaVm respuesta)
        {
            Respuesta = respuesta;
        }

        public RespuestaConsultaGenericaVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Respuesta = new RespuestaGenericaVm(codigo, mensaje, metodoInvoca);
        }
    }
}