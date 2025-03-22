namespace LabCamaronWeb.Dto.Comercial.AsignacionCliente
{
    public class AsignacionClienteVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = string.Empty;
        public string IdPlanificacion { get; set; } = string.Empty;
        public string DescripcionPlanificacion { get; set; } = string.Empty;
        public int IdEnteVendedor { get; set; }
        public string NombreEnteVendedor { get; set; } = string.Empty;
        public int IdEnteCliente { get; set; }
        public string NombreEnteCliente { get; set; } = string.Empty;
        public decimal TotalKilogramos { get; set; }

        public class Detallado : AsignacionClienteVm
        {
            public List<AsignacionClienteLoteVm> DetallesLote { get; set; } = [];
        }

        public class ConsultarTodosAsignacionCliente
        {
            public bool SoloActivos { get; set; }
        }
    }
}