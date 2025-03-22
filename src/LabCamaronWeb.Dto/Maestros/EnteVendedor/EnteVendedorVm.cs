using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.EnteVendedor
{
    public class EnteVendedorVm
    {
        public long IdEnte { get; set; }
        public string TipoEntidad { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombresCompletos => $"{Nombres} {Apellidos}";
        public string RazonComercial { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; } = string.Empty;
        public DateTime FechaModificacion { get; set; }

        public class CrearActualizarEnteVendedor
        {
            [Required(ErrorMessage = "Identificacion es obligatorio")]
            public string? Identificacion { get; set; }

            [Required(ErrorMessage = "IdEnte es obligatorio")]
            public long? IdEnte { get; set; }

            public string? Codigo { get; set; }
            public bool Activo { get; set; }
        }

        public class EliminarEnteVendedor
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }
    }
}