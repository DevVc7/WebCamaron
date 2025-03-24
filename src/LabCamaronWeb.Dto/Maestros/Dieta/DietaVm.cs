using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Dieta
{
    public class DietaVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public long IdEstadio { get; set; }
        public string NombreEstadio { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaModificacion { get; set; }

        public class Detallado : DietaVm
        {
            //public List<DietaInsumoDto> DetallesInsumo { get; set; } = [];
        }

        public class ConsultarDietas
        {
            public bool SoloActivos { get; set; }
        }

        public class ConsultarDieta
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class EliminarDieta
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearDieta
        {
            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "IdEstadio es obligatorio")]
            public long? IdEstadio { get; set; }
        }

        public class ActualizarDieta
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "IdLaboratorio es obligatorio")]
            public long? IdLaboratorio { get; set; }

            [Required(ErrorMessage = "IdEstadio es obligatorio")]
            public long? IdEstadio { get; set; }
        }
    }
}