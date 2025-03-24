using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.CupoVendedor
{
    public class CupoVendedorVm
    {
        public long IdLaboratorio { get; set; }
        public long IdModuloLaboratorio { get; set; }
        public string CodigoModuloLaboratorio { get; set; } = string.Empty;
        public string NombreLaboratorio { get; set; } = string.Empty;
        public string NombreModuloLaboratorio { get; set; } = string.Empty;
        public List<DetalleCupoVendedorDto> DetallesCupoVendedor { get; set; } = [];

        public class ConsultarCupoVendedor
        {
            [Required(ErrorMessage = "El IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "El código del módulo es obligatorio")]
            public string? CodigoModuloLaboratorio { get; set; }
        }

        public class CrearActualizarCupoVendedor
        {
            [Required(ErrorMessage = "El IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "El Código del módulo de laboratorio es obligatorio")]
            public string? CodigoModuloLaboratorio { get; set; }

            [Required(ErrorMessage = "El IdModuloLaboratorio es obligatorio")]
            public long? IdModuloLaboratorio { get; set; }

            [Required(ErrorMessage = "El detalle de vendedores es obligatorio")]
            public List<DetalleCupoVendedorDto.Actualizar>? DetalleCupoVendedores { get; set; }
        }

        public class EliminarCupoVendedor
        {
            [Required(ErrorMessage = "El IdModuloLaboratorio es obligatorio")]
            public long? IdModuloLaboratorio { get; set; }
        }

        public class DetalleCupoVendedorDto
        {
            public long IdEnteVendedor { get; set; }
            public string TipoEntidad { get; set; } = string.Empty;
            public string IdentificacionEnteVendedor { get; set; } = string.Empty;
            public string NombresEnteVendedor { get; set; } = string.Empty;
            public string ApellidosEnteVendedor { get; set; } = string.Empty;
            public string NombresCompletosEnteVendedor => $"{NombresEnteVendedor} {ApellidosEnteVendedor}";
            public string RazonSocialEnteVendedor { get; set; } = string.Empty;
            public long IdColor { get; set; }
            public string NombreColor { get; set; } = string.Empty;
            public string CodigoHexadecimal { get; set; } = string.Empty;
            public decimal PorcentajeCupo { get; set; }

            public class Actualizar
            {
                [Required(ErrorMessage = "Id del vendedor es obligatorio")]
                public long? IdEnteVendedor { get; set; }

                [Required(ErrorMessage = "Id del color es obligatorio")]
                public long? IdColor { get; set; }

                [Required(ErrorMessage = "Id del vendedor es obligatorio")]
                [Range(0, 100, ErrorMessage = "El valor del porcentaje debe ser menor o igual que 100")]
                public decimal? PorcentajeCupo { get; set; }

                public bool Activo { get; set; }
            }
        }
    }
}