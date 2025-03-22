using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class DetallePermisoVm
    {
        public string CodigoMenu { get; set; } = string.Empty;
        public string CodigoPermiso { get; set; } = string.Empty;

        public class ConsultarPermisos
        {
            [Required(ErrorMessage = "El identificador de la sesión es obligatorio")]
            public string? IdentificadorSesion { get; set; }
        }

        public class AutorizarAccion
        {
            [Required(ErrorMessage = "El identificador de la sesión es obligatorio")]
            public string IdentificadorSesion { get; set; } = string.Empty;

            [Required(ErrorMessage = "El código del menú es obligatorio")]
            public string? CodigoMenu { get; set; }

            [Required(ErrorMessage = "El código del permiso es obligatorio")]
            public string? CodigoPermiso { get; set; }
        }
    }
}