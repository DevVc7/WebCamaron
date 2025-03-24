using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Cargo
{
    public class CargoVm
    {
        public long Id { get; set; }
        public int Orden { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public class ConsultarCargo
        {
            public long? Id { get; set; }
        }

        public class ConsultarTodosCargo
        {
            public bool SoloActivo { get; set; }
        }
        
        public class ActualizarCargo
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
            public int Orden { get; set; }

            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;
            public string? Descripcion { get; set; }
        }
    }
}