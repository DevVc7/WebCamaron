using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Usuario es obligatorio")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es obligatorio")]
        public string? Contrasenia { get; set; }
    }
}