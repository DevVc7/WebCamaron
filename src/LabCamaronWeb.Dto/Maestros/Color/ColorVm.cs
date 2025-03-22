using LabCamaronWeb.Dto.Maestros.Enums;
using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Color
{
    public class ColorVm
    {
        public long Id { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CodigoHexadecimal { get; set; } = string.Empty;
        public TipoColor TipoColor { get; set; }
        public bool Activo { get; set; }

        public class ConsultarColor
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosColor
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public TipoColor? TipoColor { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarColor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarColor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearColor
        {
            public int Orden { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Código hexadecimal es obligatorio")]
            public string CodigoHexadecimal { get; set; } = string.Empty;

            public TipoColor TipoColor { get; set; }
        }

        public class ActualizarColor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            public int Orden { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Código hexadecimal es obligatorio")]
            public string CodigoHexadecimal { get; set; } = string.Empty;

            public TipoColor TipoColor { get; set; }
        }
    }
}