using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Extensiones;
using LabCamaronWeb.Servicios.Configuracion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers;

public class LoginController(ISeLoginService seUsuario) : Controller
{
    private readonly ISeLoginService seUsuario = seUsuario;

    public IActionResult Index()
    {
        // Limpiamos la sesión y la cookie de la sesión
        ProcesamientoMensajes();

        LimpiarSesion();

        return View("Login");
    }

    public async Task<IActionResult> InicioSesion([FromForm] LoginVm login)
    {
        var respuesta = await seUsuario.Login(login);

        if (respuesta.Respuesta.EsExitosa)
        {
            // Creamos las opciones de la cookie para que sea HttpOnly y Secure
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    // La cookie no podrá ser accedida por JavaScript
                Secure = true,      // La cookie solo se enviará sobre HTTPS
                Expires = DateTime.Now.AddMinutes(30)  // La cookie expirará en 30 minutos
            };

            // Guardamos el token JWT en la cookie
            Response.Cookies.Append(SesionConstantes.JwtToken, respuesta.Jwt.Token, cookieOptions);

            HttpContext.Session.Agregar(SesionConstantes.IdentificadorSesion, respuesta.IdentificadorSesion);
            HttpContext.Session.Agregar(SesionConstantes.JwtToken, respuesta.Jwt);
            HttpContext.Session.Agregar(SesionConstantes.CodigoUsuario, respuesta.Permisos.Usuario.Codigo);
            HttpContext.Session.Agregar(SesionConstantes.Usuario, respuesta.Permisos.Usuario);
            HttpContext.Session.Agregar(SesionConstantes.Modulos, respuesta.Permisos.Modulos);
            HttpContext.Session.Agregar(SesionConstantes.Permisos, respuesta.Permisos.Detalles);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            this.ViewBag.MensajeError = respuesta.Respuesta.Mensaje;

            return View("Login");
        }
    }

    public IActionResult CerrarSesion()
    {
        LimpiarSesion();

        return RedirectToAction("Index", "Login");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    public IActionResult UsuarioNoAutorizado()
    {
        return View("w3crm/Error401");
    }

    private void LimpiarSesion()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete(SesionConstantes.JwtToken);
    }

    private void ProcesamientoMensajes()
    {
        var tipo = (string)TempData[LoginConstantes.TipoMensajeLogin]!;
        if (tipo == LoginConstantes.TipoCambioClave)
        {
            this.ViewBag.MensajeExito = "Contraseña cambiada con éxito";
        }
    }
}