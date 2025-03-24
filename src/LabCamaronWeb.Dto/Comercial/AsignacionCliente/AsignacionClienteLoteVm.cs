using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Comercial.AsignacionCliente
{
    public class AsignacionClienteLoteVm
    {
        public long IdAsignacionClienteLote { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public string NombreSector { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string NombreContacto { get; set; } = string.Empty;
        public string NombrePrecria { get; set; } = string.Empty;
        public string IdsTanque { get; set; } = string.Empty;
        public string NombresTanques { get; set; } = string.Empty;
        public decimal PlGramoRequerido { get; set; }
        public decimal Salinidad { get; set; }
        public decimal Temperatura { get; set; }
        public int NumeroCamiones { get; set; }
        public int NumeroTinas { get; set; }
        public int NumeroChequeadores { get; set; }
        public decimal ValorKilogramos { get; set; }

        public class Actualizacion
        {
            [Required(ErrorMessage = "Lote es obligatorio")]
            public string? NumeroLote { get; set; }

            [Required(ErrorMessage = "Sector es obligatorio")]
            public string? NombreSector { get; set; }

            [Required(ErrorMessage = "Latitud es obligatorio")]
            public double? Latitud { get; set; }

            [Required(ErrorMessage = "Longitud es obligatorio")]
            public double? Longitud { get; set; }

            [Required(ErrorMessage = "Contacto es obligatorio")]
            public string? NombreContacto { get; set; }

            [Required(ErrorMessage = "Precria es obligatorio")]
            public string? NombrePrecria { get; set; }

            [Required(ErrorMessage = "Tanques es obligatorio")]
            public string? IdsTanque { get; set; }

            [Required(ErrorMessage = "Pl/Gramos es obligatorio")]
            public decimal? PlGramoRequerido { get; set; }

            [Required(ErrorMessage = "Salinidad es obligatorio")]
            public decimal? Salinidad { get; set; }

            [Required(ErrorMessage = "Temperatura es obligatorio")]
            public decimal? Temperatura { get; set; }

            [Required(ErrorMessage = "N° Camiones es obligatorio")]
            public int? NumeroCamiones { get; set; }

            [Required(ErrorMessage = "N° Tinas es obligatorio")]
            public int? NumeroTinas { get; set; }

            [Required(ErrorMessage = "Chequeadores es obligatorio")]
            public int? NumeroChequeadores { get; set; }

            [Required(ErrorMessage = "Valor Kilogramos es obligatorio")]
            public decimal? ValorKilogramos { get; set; }

        }
    }
}