using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Parametrizacion.Ciudad
{
    public class CiudadVm
    {
        public long Id { get; set; }
        public long IdProvincia { get; set; }
        public string NombreProvincia { get; set; } = string.Empty;
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarCiudad
        {
            [Required(ErrorMessage = "IdProvincia es obligatorio")]
            public long? IdProvincia { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class ConsultarTodosCiudad
        {
            public long? IdProvincia { get; set; }
            public bool SoloActivos { get; set; }
        }
    }
}