using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Dto.Configuracion.Rol
{
    public class PermisoRolVm
    {
        public string CodigoRol { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;
        public List<Modulo> Modulos { get; set; } = [];

        public class Consultar
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }
        }

        public class Actualizar
        {
            [Required(ErrorMessage = "Código es obligatorio")]
            public string? Codigo { get; set; }

            [Required(ErrorMessage = "Detalle de permisos es obligatorio")]
            public List<DetallePermiso>? DetallePermisos { get; set; }
        }

        public class DetallePermiso
        {
            public long IdMenuAccion { get; set; }
            public bool TienePermiso { get; set; }
        }

        public class MenuAccion
        {
            public long IdMenuAccion { get; set; }
            public int OrdenAccion { get; set; }
            public string CodigoAccion { get; set; } = string.Empty;
            public string NombreAccion { get; set; } = string.Empty;
            public string DescripcionAccion { get; set; } = string.Empty;
            public bool TienePermiso { get; set; }
        }

        public class Menu
        {
            public int OrdenMenu { get; set; }
            public string CodigoMenu { get; set; } = string.Empty;
            public string NombreMenu { get; set; } = string.Empty;
            public List<MenuAccion> MenuAcciones { get; set; } = [];
        }

        public class Modulo
        {
            public int OrdenModulo { get; set; }
            public string NombreModulo { get; set; } = string.Empty;
            public List<Menu> Menus { get; set; } = [];
        }
    }
}