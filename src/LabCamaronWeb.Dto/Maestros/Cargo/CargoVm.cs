using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Cargo
{
    public class CargoVm
    {
        public long Id { get; set; }
        public long IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public int Orden { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarCargo
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosCargo
        {
            public long? Id { get; set; }
            public long? IdEmpresa { get; set; }
            public string? Nombre { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarCargo
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarCargo
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearCargo
        {
            [Required(ErrorMessage = "Id empresa es obligatorio")]
            public long? IdEmpresa { get; set; }

            public int Orden { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }

        public class ActualizarCargo
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Id empresa es obligatorio")]
            public long IdEmpresa { get; set; }

            public int Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            public string? Descripcion { get; set; }
        }
    }
}