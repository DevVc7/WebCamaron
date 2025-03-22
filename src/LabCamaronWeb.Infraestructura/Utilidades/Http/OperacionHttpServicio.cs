using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Extensiones;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Json;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace LabCamaronWeb.Infraestructura.Utilidades.Http
{
    public class OperacionHttpServicio(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContext) : IOperacionHttpServicio
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(InformacionGateway.Nombre);
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<TSalida> EjecutarServicio<TEntrada, TSalida>(string metodo, TEntrada entrada, [CallerMemberName] string metodoInvoca = "")
        {
            return await ProcesarSolicitudHttp<TEntrada, TSalida>(metodo, entrada, false, metodoInvoca);
        }

        public async Task<TSalida> EjecutarServicioAutenticado<TSalida>(string metodo, [CallerMemberName] string metodoInvoca = "")
        {
            return await ProcesarSolicitudHttp<object?, TSalida>(metodo, null, true, metodoInvoca);
        }

        public async Task<TSalida> EjecutarServicioAutenticado<TEntrada, TSalida>(string metodo, TEntrada entrada, [CallerMemberName] string metodoInvoca = "")
        {
            return await ProcesarSolicitudHttp<TEntrada, TSalida>(metodo, entrada, true, metodoInvoca);
        }

        private static TSalida CrearInstancia<TSalida>(string codigo, string mensaje, [CallerMemberName] string metodoInvoca = "")
        {
            return (TSalida)(Activator.CreateInstance(typeof(TSalida), codigo, mensaje, metodoInvoca))!;
        }

        private async Task<TSalida> ProcesarSolicitudHttp<TEntrada, TSalida>(string metodo, TEntrada? entrada, bool incluirJwt,
            [CallerMemberName] string metodoInvoca = "")
        {
            try
            {
                if (incluirJwt)
                {
                    var tokenSesion = _httpContext.HttpContext.Session.Obtener<JwtModelVm>(SesionConstantes.JwtToken);
                    if (tokenSesion is null)
                    {
                        LogUtils.LogError(Servidor.MensajeTokenNoEncontrado + " " + metodo, entrada, metodoInvoca);
                        return CrearInstancia<TSalida>(Servidor.CodigoRespuestaTokenNoEncontrado, Servidor.MensajeTokenNoEncontrado, metodoInvoca);
                    }

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSesion.Token);
                }

                HttpResponseMessage? response;
                if (entrada is null)
                {
                    response = await _httpClient.PostAsync(metodo, null);
                }
                else
                {
                    var jsonContent = JsonUtil.Serializar(entrada);
                    response = await _httpClient.PostAsync(metodo, jsonContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    // Devuelve la respuesta como un string
                    var respuesta = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(respuesta))
                    {
                        // Usar Activator para crear una instancia con parámetros
                        return CrearInstancia<TSalida>(Servidor.CodigoRespuestaVacia, Servidor.MensajeRespuestaVacia, metodoInvoca);
                    }

                    // Deserializa la respuesta
                    var respuestaDeserializada = JsonUtil.Deserializar<TSalida>(respuesta)!;
                    return respuestaDeserializada;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    LogUtils.LogError(Servidor.MensajeRespuestaMicroservicio + " " + metodo, entrada, metodoInvoca);
                    return CrearInstancia<TSalida>(Servidor.CodigoMetodoNoAutorizado, Servidor.MensajeMetodoNoAutorizado, metodoInvoca);
                }
                else if (response.StatusCode == HttpStatusCode.BadGateway)
                {
                    LogUtils.LogError(Servidor.MensajeRespuestaMicroservicio + " " + metodo, entrada, metodoInvoca);
                    return CrearInstancia<TSalida>(Servidor.CodigoServicioNoDisponible, Servidor.MensajeServicioNoDisponible, metodoInvoca);
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    LogUtils.LogError(Servidor.MensajeRespuestaMicroservicio + " " + metodo, entrada, metodoInvoca);
                    return CrearInstancia<TSalida>(Servidor.CodigoExcepcionServicio, Servidor.MensajeExcepcionServicio, metodoInvoca);
                }
                else
                {
                    LogUtils.LogError(Servidor.MensajeRespuestaMicroservicio + " " + metodo, entrada, metodoInvoca);
                    return CrearInstancia<TSalida>(Servidor.CodigoRespuestaMicroservicio, Servidor.MensajeRespuestaMicroservicio, metodoInvoca);
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, entrada, metodoInvoca);
                return CrearInstancia<TSalida>(Servidor.Excepcion, Servidor.MensajeExcepcion, metodoInvoca);
            }
        }
    }
}