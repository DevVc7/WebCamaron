using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Insumos
{
    public class InsumosVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string NombreInsumo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public long IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public long IdMarca { get; set; }
        public string NombreMarca { get; set; } = string.Empty;
        public float Presentacion { get; set; }
        public long IdUnidadMedida { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarInsumos
        {
            public long Id { get; set; }
        }

        public class ConsultarTodosInsumos
        {
            public long? IdLaboratorio { get; set; }
            public string? Codigo { get; set; }
            public string? Nombre { get; set; }
            public string? Sku { get; set; }
            public long? IdCategoria { get; set; }
            public long? IdMarca { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarInsumos
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long Id { get; set; }
        }

        public class CrearInsumos
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Sku es obligatorio")]
            public string Sku { get; set; } = string.Empty;

            [Required(ErrorMessage = "Categoria es obligatorio")]
            public long IdCategoria { get; set; }

            [Required(ErrorMessage = "Marca es obligatorio")]
            public long IdMarca { get; set; }

            [Required(ErrorMessage = "Presentación es obligatorio")]
            public decimal Presentacion { get; set; }

            [Required(ErrorMessage = "UnidadMedida es obligatorio")]
            public string UnidadMedida { get; set; } = string.Empty;
        }

        public class ActualizarInsumos
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long Id { get; set; }

            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "Sku es obligatorio")]
            public string Sku { get; set; } = string.Empty;

            [Required(ErrorMessage = "Categoria es obligatorio")]
            public long IdCategoria { get; set; }

            [Required(ErrorMessage = "Marca es obligatorio")]
            public long IdMarca { get; set; }

            [Required(ErrorMessage = "Presentación es obligatorio")]
            public decimal Presentacion { get; set; }

            [Required(ErrorMessage = "UnidadMedida es obligatorio")]
            public string UnidadMedida { get; set; } = string.Empty;
        }
    }
}