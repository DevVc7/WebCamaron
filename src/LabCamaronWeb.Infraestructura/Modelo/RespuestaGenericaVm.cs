using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Modelo
{
    public record RespuestaGenericaVm
    {
        public string Codigo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public bool TieneErrorServicio => Servidor.CodigosErrorServicio.Contains(Codigo);
        public bool EsExitosa => Codigo == Servidor.ExitoComun;
        public bool ExisteExcepcion => Codigo == Servidor.Excepcion;
        public RespuestaGenericaVm() { }

        public RespuestaGenericaVm(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Codigo = codigo;
            Mensaje = mensaje;

            //Escribimos en el log de errores cuando exista un mensaje controlado
            if (!Servidor.CodigosNoLog.Contains(codigo))
            {
                LogUtils.LogWarning(mensaje, metodoInvoca);
            }
        }

        public static RespuestaGenericaVm ExitoComun(string mensaje = "Operación realizada con exito")
        {
            return new RespuestaGenericaVm
            {
                Codigo = Servidor.ExitoComun,
                Mensaje = mensaje
            };
        }

        public static RespuestaGenericaVm ErrorComun(string mensaje = "Ha ocurrido un error", [CallerMemberName] string metodoInvoca = "")
        {
            // Escribimos en el log de errores cuando exista un mensaje controlado
            LogUtils.LogWarning(mensaje, metodoInvoca);

            return new RespuestaGenericaVm
            {
                Codigo = Servidor.ErrorComun,
                Mensaje = mensaje
            };
        }

        public static RespuestaGenericaVm Excepcion(string mensaje = "Ha ocurrido una excepción")
        {
            return new RespuestaGenericaVm
            {
                Codigo = Servidor.Excepcion,
                Mensaje = mensaje
            };
        }

        public static RespuestaGenericaVm RespuestaHttpVacia([CallerMemberName] string metodoInvoca = "")
        {
            // Escribimos en el log de errores cuando exista un mensaje controlado
            LogUtils.LogWarning("Respuesta Http Vacía", metodoInvoca);

            return new RespuestaGenericaVm
            {
                Codigo = Servidor.CodigoRespuestaVacia,
                Mensaje = Servidor.MensajeRespuestaVacia
            };
        }

        public static RespuestaGenericaVm RespuestaHttpError([CallerMemberName] string metodoInvoca = "")
        {
            // Escribimos en el log de errores cuando exista un mensaje controlado
            LogUtils.LogWarning("Respuesta Http con Error", metodoInvoca);

            return new RespuestaGenericaVm
            {
                Codigo = Servidor.CodigoRespuestaVacia,
                Mensaje = Servidor.MensajeRespuestaVacia
            };
        }
    }
}