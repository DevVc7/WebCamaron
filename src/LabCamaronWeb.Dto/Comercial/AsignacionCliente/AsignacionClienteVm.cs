using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Comercial.AsignacionCliente
{
    public class AsignacionClienteVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = string.Empty;
        public long IdPlanificacion { get; set; }
        public string DescripcionPlanificacion { get; set; } = string.Empty;
        public long IdEnteVendedor { get; set; }
        public string NombreEnteVendedor { get; set; } = string.Empty;
        public long IdEnteCliente { get; set; }
        public string NombreEnteCliente { get; set; } = string.Empty;
        public decimal TotalKilogramos { get; set; }

        public class Detallado : AsignacionClienteVm
        {
            public List<AsignacionClienteLoteVm> DetalleLotes { get; set; } = [];
        }

        public class ConsultarAsignacionCliente
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class EliminarAsignacionCliente
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearAsignacionCliente
        {
            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdModulo { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdPlanificacion { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdEnteVendedor { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdEnteCliente { get; set; }


            [Required(ErrorMessage = "Detalles de Lote es obligatorio")]
            public List<AsignacionClienteLoteVm.Actualizacion> DetallesLote { get; set; } = [];
        }

        public class ActualizarAsignacionCliente
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdModulo { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdPlanificacion { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdEnteVendedor { get; set; }


            [Required(ErrorMessage = "Laboratorio es obligatorio")]
            public long? IdEnteCliente { get; set; }


            [Required(ErrorMessage = "Detalles de Lote es obligatorio")]
            public List<AsignacionClienteLoteVm.Actualizacion> DetallesLote { get; set; } = [];
        }

        public class ConsultarTodosAsignacionCliente
        {
            public bool SoloActivos { get; set; }
        }
    }
}