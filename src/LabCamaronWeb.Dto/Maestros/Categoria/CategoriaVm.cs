using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Categoria
{
    public class CategoriaVm
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarCategoria
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosCategoria
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarCategoria
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarCategoria
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearCategoria
        {
            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }

        public class ActualizarCategoria
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }
    }
}