using LabCamaronWeb.Dto.Maestros.EnteCliente;
using LabCamaronWeb.Dto.Maestros.EntePersonal;
using LabCamaronWeb.Dto.Maestros.EnteTecnico;
using LabCamaronWeb.Dto.Maestros.EnteVendedor;
using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Ente
{
    public class EnteVm
    {
        public long Id { get; set; }
        public string TipoEntidad { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombresCompletos => $"{Nombres} {Apellidos}";
        public string RazonComercial { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Correos { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class Detallado : EnteVm
        {
            public EnteVendedorVm Vendedor { get; set; } = new();
            public EnteClienteVm Cliente { get; set; } = new();
            public EnteTecnicoVm Tecnico { get; set; } = new();
            public EntePersonalVm Personal { get; set; } = new();
        }

        public class ConsultarEnte
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
        }

        public class ConsultarTodosEnte
        {
            public bool SoloActivos { get; set; }
        }

        public class ConsultarTodosEnteSinRol
        {
            public string RolExcluir { get; set; } = string.Empty;
            public bool SoloActivos { get; set; }
        }

        public class EliminarEnte
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarEnte
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearEnte
        {
            [Required(ErrorMessage = "Tipo Entidad es obligatorio")]
            public string? TipoEntidad { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }

            public string? Nombres { get; set; }
            public string? Apellidos { get; set; }
            public string? RazonComercial { get; set; }
            public string? RazonSocial { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
        }

        public class ActualizarEnte
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }

            public string? Nombres { get; set; }
            public string? Apellidos { get; set; }
            public string? RazonComercial { get; set; }
            public string? RazonSocial { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
        }
    }
}