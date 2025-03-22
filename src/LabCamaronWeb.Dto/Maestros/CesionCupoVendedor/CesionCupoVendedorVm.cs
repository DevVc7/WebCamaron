using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.CesionCupoVendedor
{
    public class CesionCupoVendedorVm
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarCesionCupoVendedor
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        public class ConsultarTodosCesionCupoVendedor
        {
            public long? Id { get; set; }
            public string? Nombre { get; set; }
            public bool? Activo { get; set; }
        }

        public class EliminarCesionCupoVendedor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarCesionCupoVendedor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearCesionCupoVendedor
        {
            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }

        public class ActualizarCesionCupoVendedor
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Nombres es obligatorio")]
            public string? Nombre { get; set; }

            public string? Descripcion { get; set; }
        }
    }
}