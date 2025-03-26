using LabCamaronWeb.Dto.Comercial.PlanificacionSiembra;
using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Comercial.PlanificacionSiembra
{
    public class PlanificacionSiembraVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public DateTime FechaPlanificacion { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public PlanificacionSiembraDetalleVm[] PlanificacionSiembraDetalle { get; set; } = [];



        public class ConsultarPlanificacionSiembraId
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }
        public class ConsultarTodosPlanificacionSiembra
        {
            public bool SoloActivos { get; set; }
        }

        public class ConsultarTodosRangoFecha
        {
            public DateTime FechaPlanificacionInicio { get; set; }
            public DateTime FechaPlanificacionFin { get; set; }
        }

        public class EliminarPlanificacionSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }
        public class ActivarPlanificacionSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearPlanificacionSiembra
        {
            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            public string Codigo { get; set; } = string.Empty;
            [Required(ErrorMessage = "Fecha planificación es obligatorio")]
            public DateTime FechaPlanificacion { get; set; }

            [Required(ErrorMessage = "Detalles de planificación es obligatorio")]
            public bool Activo { get; set; }
            public PlanificacionSiembraDetalleVm.ActualizarPlanificacionSiembraDetalle[] PlanificacionSiembraDetalle { get; set; } = [];
        }

        public class ActualizarPlanificacionSiembra
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            public string Codigo { get; set; } = string.Empty;
            [Required(ErrorMessage = "Fecha planificación es obligatorio")]
            public DateTime FechaPlanificacion { get; set; }

            [Required(ErrorMessage = "Detalles de planificación es obligatorio")]
            public bool Activo { get; set; }
            public PlanificacionSiembraDetalleVm.ActualizarPlanificacionSiembraDetalle[] PlanificacionSiembraDetalle { get; set; } = [];
        }
    }
}