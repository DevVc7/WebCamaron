using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Comercial.PlanificacionSiembra
{
    public class PlanificacionSiembraDetalleVm
    {
        public long Id { get; set; }
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = string.Empty;
        public long IdEnteTecnico { get; set; }
        public string NombreEnteTecnico { get; set; } = string.Empty;
        public DateTime FechaSiembra { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int CantidadFacturada { get; set; }
        public bool Activo { get; set; }



        public class ActualizarPlanificacionSiembraDetalle
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long Id { get; set; }
            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long IdModulo { get; set; }

            [Required(ErrorMessage = "Tecnico es obligatorio")]
            public long IdEnteTecnico { get; set; }

            [Required(ErrorMessage = "Fecha Siembra es obligatorio")]
            public DateTime FechaSiembra { get; set; }
            [Required(ErrorMessage = "Fecha Cosecha es obligatorio")]
            public DateTime FechaCosecha { get; set; }
            public int CantidadFacturada { get; set; }
            public bool Activo { get; set; }
        }
    }
}