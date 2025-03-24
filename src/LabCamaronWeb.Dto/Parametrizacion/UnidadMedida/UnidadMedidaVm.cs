using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Parametrizacion.UnidadMedida
{
    public class UnidadMedidaVm
    {
        public long Id { get; set; }
        public int Orden { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarUnidadMedida
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosUnidadMedida
        {
            public bool SoloActivos { get; set; }
        }

        public class EliminarUnidadMedida
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearUnidadMedida
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "Tipo de Unidad de Medida es obligatorio")]
            public string? Tipo { get; set; }
        }

        public class ActualizarUnidadMedida
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "Tipo de Unidad de Medida es obligatorio")]
            public string? Tipo { get; set; }
        }
    }
}