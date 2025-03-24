using LabCamaronWeb.Infraestructura.Constantes;
using Newtonsoft.Json;
using Serilog;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Utilidades.Logger
{
    public static class LogUtils
    {
        public static void LogWarning(string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            Log.Warning("{0} - Error - método: {1}. Detalle: {2}.",
                InformacionApi.Nombre,
                metodoInvoca,
                mensaje);
        }

        public static void LogError(string mensaje, object? parametros = null, [CallerMemberName] string metodoInvoca = "")
        {
            var paramSerializados = JsonConvert.SerializeObject(parametros, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            Log.Error(new Exception(mensaje), "{0} - Excepcion - método: {1}. Parámetros entrada: {2}.",
                InformacionApi.Nombre,
                metodoInvoca,
                paramSerializados);
        }

        public static void LogError(Exception ex, object? parametros = null, [CallerMemberName] string metodoInvoca = "")
        {
            var paramSerializados = JsonConvert.SerializeObject(parametros, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            Log.Error(ex, "{0} - Excepcion - método: {1}. Parámetros entrada: {2}.",
                InformacionApi.Nombre,
                metodoInvoca,
                paramSerializados);
        }

        public static void LogInformation(string mensaje, object? parametros = null, [CallerMemberName] string metodoInvoca = "")
        {
            var paramSerializados = JsonConvert.SerializeObject(parametros, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            Log.Information(mensaje,
                metodoInvoca,
                paramSerializados);
        }
    }
}