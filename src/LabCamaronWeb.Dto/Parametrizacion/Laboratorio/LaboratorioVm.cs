using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Parametrizacion.Laboratorio
{
    public class LaboratorioVm
    {
        public long Id { get; set; }
        public long IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string RutaIcono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }
        public long IdCiudad { get; set; }
        public string NombreCiudad { get; set; } = string.Empty;
        public long IdProvincia { get; set; }
        public string NombreProvincia { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarLaboratorio
        {
            [Required(ErrorMessage = "IdEmpresa es obligatorio")]
            public long? IdEmpresa { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosLaboratorio
        {
            public long? IdEmpresa { get; set; }
            public bool SoloActivos { get; set; }
        }

        public class EliminarLaboratorio
        {
            [Required(ErrorMessage = "IdEmpresa es obligatorio")]
            public long? IdEmpresa { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class CrearLaboratorio
        {
            [Required(ErrorMessage = "IdEmpresa es obligatorio")]
            public long? IdEmpresa { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "IdCiudad es obligatorio")]
            public long? IdCiudad { get; set; }

            public string? RutaIcono { get; set; }
            public string? Descripcion { get; set; }
            public string? Direccion { get; set; }
            public double? Longitud { get; set; }
            public double? Latitud { get; set; }
        }

        public class ActualizarLaboratorio
        {
            [Required(ErrorMessage = "IdEmpresa es obligatorio")]
            public long? IdEmpresa { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Orden es obligatorio")]
            public int? Orden { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "IdCiudad es obligatorio")]
            public long? IdCiudad { get; set; }

            public string? RutaIcono { get; set; }
            public string? Descripcion { get; set; }
            public string? Direccion { get; set; }
            public double? Longitud { get; set; }
            public double? Latitud { get; set; }
        }
    }
}