using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Configuracion.Usuario
{
    public class UsuarioVm
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime? FechaValidezContrasenia { get; set; }
        public bool RequiereNuevaContraseña => FechaValidezContrasenia is null || DateTime.Now > FechaValidezContrasenia;
        public bool Activo { get; set; }
        public string NombresRol => string.Join(", ", Roles.Where(x => x.TienePermiso).Select(x => x.Nombre));
        public List<RolPermitido> Roles { get; set; } = [];
        public List<ModuloLaboratorioPermitido.Empresa> ModulosLaboratorio { get; set; } = [];

        public class ConsultarTodos
        {
            public bool SoloActivos { get; set; }
        }

        public class ConsultarUsuario
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;
        }

        public class CrearUsuario
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Contraseña es obligatorio")]
            public string Contrasenia => Codigo;

            [Required(ErrorMessage = "Descripción es obligatorio")]
            public string Descripcion { get; set; } = string.Empty;

            public bool ActualizarContrasenia { get; set; }
        }

        public class ActualizarUsuario
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Descripción es obligatorio")]
            public string Descripcion { get; set; } = string.Empty;
        }

        public class ActualizarClaveUsuario
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Contraseña actual es obligatorio")]
            public string ContraseniaActual { get; set; } = string.Empty;

            [Required(ErrorMessage = "Contraseña nueva es obligatorio")]
            public string ContraseniaNueva { get; set; } = string.Empty;
        }

        public class ReestablecerContrasenia
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;
        }

        public class ActualizarRol
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Roles es obligatorio")]
            public List<RolPermitido.Actualizar>? Roles { get; set; }
        }

        public class ActualizarModuloLaboratorio
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Modulos es obligatorio")]
            public List<ModuloLaboratorioPermitido.Actualizar>? ModulosLaboratorio { get; set; }
        }

        public class EliminarUsuario
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string Codigo { get; set; } = string.Empty;
        }

        public class RolPermitido
        {
            public long Id { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public bool TienePermiso { get; set; }

            public class Actualizar
            {
                public long IdRol { get; set; }
                public bool TienePermiso { get; set; }
            }
        }

        public class ModuloLaboratorioPermitido
        {
            public List<Empresa> Empresas { get; set; } = [];

            public class Empresa
            {
                public int Orden { get; set; }
                public string Codigo { get; set; } = string.Empty;
                public string Nombre { get; set; } = string.Empty;
                public List<Laboratorio> Laboratorios { get; set; } = [];
            }

            public class Laboratorio
            {
                public int Orden { get; set; }
                public string Codigo { get; set; } = string.Empty;
                public string Nombre { get; set; } = string.Empty;
                public List<Modulo> Modulos { get; set; } = [];
            }

            public class Modulo
            {
                public long Id { get; set; }
                public int Orden { get; set; }
                public string Codigo { get; set; } = string.Empty;
                public string Nombre { get; set; } = string.Empty;
                public string Descripcion { get; set; } = string.Empty;
                public bool TienePermiso { get; set; }
            }

            public class Actualizar
            {
                public long IdModulo { get; set; }
                public bool TienePermiso { get; set; }
            }
        }
    }
}