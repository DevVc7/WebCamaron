using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Cliente
{
    public class ClienteVm
    {
        public long Id { get; set; }
        public string TipoIdentificacion { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string RazonComercial { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Correos { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class Detallado : ClienteVm
        {
            public Contacto[] Contactos { get; set; } = [];
        }

        public class Contacto
        {
            public string NombreContacto { get; set; } = string.Empty;
        }

        public class ConsultarCliente
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
        }

        public class ConsultarTodosCliente
        {
            public bool SoloActivos { get; set; }
        }

        public class EliminarCliente
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarCliente
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearCliente
        {
            [Required(ErrorMessage = "Tipo Identificación es obligatorio")]
            public string? TipoIdentificacion { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
            public string? RazonComercial { get; set; }
            public string? RazonSocial { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
            public string? Contactos { get; set; }
        }

        public class ActualizarCliente
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
            public string? RazonComercial { get; set; }
            public string? RazonSocial { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
            public string? Contactos { get; set; }
        }
    }
}