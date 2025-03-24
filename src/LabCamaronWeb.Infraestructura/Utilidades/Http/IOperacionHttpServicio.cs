using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Utilidades.Http
{
    public interface IOperacionHttpServicio
    {
        Task<TSalida> EjecutarServicio<TEntrada, TSalida>(string metodo, TEntrada entrada, [CallerMemberName] string metodoInvoca = "");

        Task<TSalida> EjecutarServicioAutenticado<TEntrada, TSalida>(string metodo, TEntrada entrada, [CallerMemberName] string metodoInvoca = "");

        Task<TSalida> EjecutarServicioAutenticado<TSalida>(string metodo, [CallerMemberName] string metodoInvoca = "");
    }
}