using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Configuracion.Rol
{
    public class RolVm
    {
        public long Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarRol
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosRol
        {
            public bool SoloActivos { get; set; }
        }

        public class EliminarRol
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearRol
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }

        public class ActualizarRol
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }
    }
}