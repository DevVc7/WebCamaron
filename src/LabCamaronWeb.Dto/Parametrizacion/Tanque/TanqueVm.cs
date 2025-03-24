using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Parametrizacion.Tanque
{
    public class TanqueVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = null!;
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = null!;
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int NumeroTanque { get; set; }
        public double CapacidadLitros { get; set; }
        public bool Activo { get; set; }

        public class ConsultarTanque
        {
            [Required(ErrorMessage = "IdModulo es obligatorio")]
            public long? IdModulo { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosTanque
        {
            public long? IdModulo { get; set; }
            public bool SoloActivos { get; set; }
        }

        public class EliminarTanque
        {
            [Required(ErrorMessage = "IdModulo es obligatorio")]
            public long? IdModulo { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearTanque
        {
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "IdModulo es obligatorio")]
            public long? IdModulo { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "Número de Tanque es obligatorio")]
            public int? NumeroTanque { get; set; }

            [Required(ErrorMessage = "Capacidad en Litros es obligatorio")]
            public double? CapacidadLitros { get; set; }
        }

        public class ActualizarTanque
        {
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "IdModulo es obligatorio")]
            public long? IdModulo { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "Número de Tanque es obligatorio")]
            public int? NumeroTanque { get; set; }

            [Required(ErrorMessage = "Capacidad en Litros es obligatorio")]
            public double? CapacidadLitros { get; set; }
        }
    }
}