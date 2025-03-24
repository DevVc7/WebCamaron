using LabCamaron.Web.Autorizadores;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LabCamaron.Web.Controllers
{
    [TypeFilter(typeof(AutorizadorAttribute))]
    public class BaseController : Controller
    {
        #region Métodos de Procesamiento de Error

        public IActionResult ProcesarError()
        {
            return RedirectToAction("ErrorComun", "Home");
        }

        public IActionResult ProcesarError(RespuestaGenericaVm respuesta)
        {
            return respuesta.Codigo switch
            {
                Servidor.CodigoMetodoNoAutorizado => RedirectToAction("ErrorAutorizacion", "Home"),
                Servidor.CodigoServicioNoDisponible => RedirectToAction("ErrorMantenimiento", "Home"),
                _ => RedirectToAction("ErrorComun", "Home")
            };
        }

        #endregion Métodos de Procesamiento de Error

        #region Métodos de Procesamientos de Viewbag

        public void AsignarViewBagMensajeError(ModelStateDictionary model)
        {
            var textos = model.SelectMany(e => e.Value!.Errors.Select(x => x.ErrorMessage));
            this.ViewBag.MensajeError = string.Join(",", textos);
        }

        public void AsignarViewBagMensajeError(string mensaje)
        {
            this.ViewBag.MensajeError = mensaje;
        }

        public void AsignarViewBagMensajeError(RespuestaGenericaVm respuesta)
        {
            this.ViewBag.MensajeError = respuesta.EsExitosa ? string.Empty : respuesta.Mensaje;
        }

        public void AsignarViewBagMensajeExito(string mensaje)
        {
            this.ViewBag.MensajeExito = mensaje;
        }

        public void AsignarViewBagMensajeExito(RespuestaGenericaVm respuesta)
        {
            this.ViewBag.MensajeExito = respuesta.EsExitosa ? respuesta.Mensaje : string.Empty;
        }

        #endregion Métodos de Procesamientos de Viewbag
    }
}