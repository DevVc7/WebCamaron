using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Marca
{
    public class MarcaVm
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarMarca
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosMarca
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarMarca
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarMarca
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearMarca
        {
            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }

        public class ActualizarMarca
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }
    }
}