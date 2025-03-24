using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Horas
{
    public class HorasVm
    {
        public long Id { get; set; }
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;

        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        public bool Activo { get; set; }

        public class ConsultarHoras
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosHoras
        {
            public string? Codigo { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarHoras
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearHoras
        {
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Hora es obligatorio")]
            [DataType(DataType.Time)]
            public TimeSpan Hora { get; set; }
        }

        public class ActualizarHoras
        {
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Hora es obligatorio")]
            [DataType(DataType.Time)]
            public TimeSpan Hora { get; set; }
        }
    }
}