using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio
{
    public class ModuloLaboratorioVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarModuloLaboratorio
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosModuloLaboratorio
        {
            public long? IdLaboratorio { get; set; }
            public bool SoloActivos { get; set; }
        }

        public class EliminarModuloLaboratorio
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearModuloLaboratorio
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
        }

        public class ActualizarModuloLaboratorio
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
        }
    }
}