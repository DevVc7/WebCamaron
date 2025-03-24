using LabCamaronWeb.Infraestructura.Atributos;
using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.EstadioLarva
{
    public class EstadioLarvaVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int DiaDesde { get; set; }
        public int DiaHasta { get; set; }
        public bool Activo { get; set; }

        public class ConsultarEstadioLarva
        {
            public long? Id { get; set; }
        }

        public class ConsultarTodosEstadioLarva
        {
            public bool SoloActivo { get; set; }
        }

        public class EliminarEstadioLarva
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarEstadioLarva
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearEstadioLarva
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Día Desde es obligatorio")]
            public int? DiaDesde { get; set; }

            [Required(ErrorMessage = "Día Hasta es obligatorio")]
            [MinimoMenorQueMaximo(nameof(DiaDesde), ErrorMessage = "El valor hasta debe ser menor que el valor desde.")]
            public int? DiaHasta { get; set; }
        }

        public class ActualizarEstadioLarva
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Día Desde es obligatorio")]
            public int? DiaDesde { get; set; }

            [Required(ErrorMessage = "Día Hasta es obligatorio")]
            [MinimoMenorQueMaximo(nameof(DiaDesde), ErrorMessage = "El valor hasta debe ser menor que el valor desde.")]
            public int? DiaHasta { get; set; }
        }
    }
}