using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Maestros.Personal
{
    public class PersonalVm
    {
        public long Id { get; set; }
        public string TipoIdentificacion { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Correos { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string CodigoCargo { get; set; } = string.Empty;
        public string NombreCargo { get; set; } = string.Empty;

        public long? IdLaboratorio { get; set; }
        public string? NombreLaboratorio { get; set; }

        public string? UsuarioSistema { get; set; }
        public string? IdsModuloLaboratorio { get; set; }
        public string? NombresModuloLaboratorio { get; set; }
        public string? IdsFunciones { get; set; }
        public string? NombresFunciones { get; set; }

        public bool Activo { get; set; }
        

        public class ConsultarPersonal
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
        }

        public class ConsultarTodosPersonal
        {
            public string CodigoCargo { get; set; } = string.Empty;
            public bool SoloActivos { get; set; }
            public long? IdModulo { get; set; }
            public string? UsuarioSistema { get; set; }
        }

        public class EliminarPersonal
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class ActivarPersonal
        {
            [Required(ErrorMessage = "Identificación es obligatorio")]
            public long? Id { get; set; }
        }

        public class CrearPersonal
        {
            [Required(ErrorMessage = "Tipo Identificación es obligatorio")]
            public string? TipoIdentificacion { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
            public string? Nombres { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
            public string? CodigoCargo { get; set; }
            public string? UsuarioSistema { get; set; }
            public long? IdLaboratorio { get; set; }
            public string[]? IdsModuloLaboratorioArray { get; set; }
            public string? IdsModuloLaboratorio => string.Join(",", IdsModuloLaboratorioArray ?? []);
            public string[]? IdsFuncionesArray { get; set; }
            public string? IdsFunciones => string.Join(",", IdsFuncionesArray ?? []);
        }

        public class ActualizarPersonal
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Identificación es obligatorio")]
            public string? Identificacion { get; set; }
            public string? Nombres { get; set; }
            public string? Correos { get; set; }
            public string? Telefono { get; set; }
            public string? Celular { get; set; }
            public string? CodigoCargo { get; set; }
            public string? UsuarioSistema { get; set; }
            public long? IdLaboratorio { get; set; }
            public string[]? IdsModuloLaboratorioArray { get; set; }
            public string? IdsModuloLaboratorio => string.Join(",", IdsModuloLaboratorioArray ?? []);
            public string[]? IdsFuncionesArray { get; set; }
            public string? IdsFunciones => string.Join(",", IdsFuncionesArray ?? []);
        }
    }
}