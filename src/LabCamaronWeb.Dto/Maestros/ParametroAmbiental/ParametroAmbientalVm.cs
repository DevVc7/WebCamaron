using LabCamaronWeb.Infraestructura.Atributos;
using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.ParametroAmbiental
{
    public class ParametroAmbientalVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public float? ValorNormal { get; set; }
        public float? ValorMinimo { get; set; }
        public float? ValorMaximo { get; set; }
        public bool Activo { get; set; }

        public class ConsultarParametroAmbiental
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosParametroAmbiental
        {
            public long? IdLaboratorio { get; set; }
            public bool SoloActivos { get; set; }
        }

        public class EliminarParametroAmbiental
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearParametroAmbiental
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "Valor Mínimo es obligatorio")]
            [RangoEntre(nameof(ValorMinimo), nameof(ValorMaximo), ErrorMessage = "El valor debe estar dentro del rango indicado.")]
            public float? ValorNormal { get; set; }

            [Required(ErrorMessage = "Valor Mínimo es obligatorio")]
            public float? ValorMinimo { get; set; }

            [Required(ErrorMessage = "Valor Máximo es obligatorio")]
            [MinimoMenorQueMaximo(nameof(ValorMinimo), ErrorMessage = "El valor mínimo debe ser menor que el valor máximo.")]
            public float? ValorMaximo { get; set; }
        }

        public class ActualizarParametroAmbiental
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "Valor Mínimo es obligatorio")]
            [RangoEntre(nameof(ValorMinimo), nameof(ValorMaximo), ErrorMessage = "El valor debe estar dentro del rango indicado.")]
            public float? ValorNormal { get; set; }

            [Required(ErrorMessage = "Valor Mínimo es obligatorio")]
            public float? ValorMinimo { get; set; }

            [Required(ErrorMessage = "Valor Máximo es obligatorio")]
            [MinimoMenorQueMaximo(nameof(ValorMinimo), ErrorMessage = "El valor mínimo debe ser menor que el valor máximo.")]
            public float? ValorMaximo { get; set; }
        }
    }
}