using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Comercial.AprobarDespacho
{
    public class AprobarDespachoVm
    {
        public long Id { get; set; }
        public long IdLaboratorio { get; set; }
        public string NombreLaboratorio { get; set; } = string.Empty;
        public long IdModulo { get; set; }
        public string NombreModulo { get; set; } = string.Empty;
        public string NumeroLote { get; set; } = string.Empty;
        public string NombrePrecrias { get; set; } = string.Empty;
        public long IdEnteVendedor { get; set; }
        public string NombreEnteVendedor { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaAprobacion {  get; set; }

        public class ConsultarAprobarDespacho
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }


        public class ConsultarTodosAprobarDespacho
        {
            public bool SoloActivos { get; set; }
        }
        public class CrearAprobarDespacho
        {

        }
    }
}
