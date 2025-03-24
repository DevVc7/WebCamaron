using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Produccion.Siembra
{
    public class SiembraVm
    {
        public long Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int Corrida { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = string.Empty;
        public string Sala { get; set; } = string.Empty;
        public DateTime FechaPlanificacion { get; set; }
        public decimal Densidad { get; set; }
        public int CantidadFacturada { get; set; }
        public int CantidadBruta { get; set; }
        public bool Activo { get; set; }

        public class ConsultarSiembra
        {
            public long? Id { get; set; }
            public string? Codigo { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosSiembra
        {
            public long? Id { get; set; }
            public string? Codigo { get; set; }
            public string? Nombre { get; set; }
            public long? IdLaboratorio { get; set; }
            public long? IdModulo { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearSiembra
        {
            [Required(ErrorMessage = "Codigo es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long IdLaboratorio { get; set; }

            public string NombreLaboratorio { get; set; } = string.Empty;

            [Required(ErrorMessage = "Modulo es obligatorio")]
            public long IdModulo { get; set; }

            public string NombreModulo { get; set; } = string.Empty;
            public string Sala { get; set; } = string.Empty;

            [Required(ErrorMessage = "Fecha planificación es obligatorio")]
            public DateTime FechaPlanificacion { get; set; }

            public decimal Densidad { get; set; }
            public int CantidadFacturada { get; set; }
            public int CantidadBruta { get; set; }
            public bool Activo { get; set; }
        }

        public class ActualizarSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long IdLaboratorio { get; set; }

            public string NombreLaboratorio { get; set; } = string.Empty;

            [Required(ErrorMessage = "Modulo es obligatorio")]
            public long IdModulo { get; set; }

            public string NombreModulo { get; set; } = string.Empty;
            public string Sala { get; set; } = string.Empty;

            [Required(ErrorMessage = "Fecha planificación es obligatorio")]
            public DateTime FechaPlanificacion { get; set; }

            public decimal Densidad { get; set; }
            public int CantidadFacturada { get; set; }
            public int CantidadBruta { get; set; }
            public bool Activo { get; set; }
        }
    }
}