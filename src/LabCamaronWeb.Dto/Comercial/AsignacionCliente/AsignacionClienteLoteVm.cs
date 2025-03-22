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
    }
}