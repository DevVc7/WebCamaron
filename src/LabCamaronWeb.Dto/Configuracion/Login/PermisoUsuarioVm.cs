namespace LabCamaronWeb.Dto.Configuracion.Login
{
    public class PermisoUsuarioVm
    {
        public UsuarioVm Usuario { get; set; } = new UsuarioVm();
        public List<ModuloVm> Modulos { get; set; } = [];
        public List<DetallePermisoVm> Detalles => ProcesarDetallesPermisos(Modulos);

        public List<RolVm> Roles { get; set; } = [];

        public class UsuarioVm
        {
            public string Codigo { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public DateTime? FechaValidezContrasenia { get; set; }
            public bool RequiereNuevaContraseña { get; set; }
        }

        public class ModuloVm
        {
            public long Id { get; set; }
            public int Orden { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string IconoModulo { get; set; } = string.Empty;
            public List<MenuVm> Menus { get; set; } = [];
        }

        public class MenuVm
        {
            public long Id { get; set; }
            public int Orden { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string Controlador { get; set; } = string.Empty;
            public string Accion { get; set; } = string.Empty;
            public string IconoMenu { get; set; } = string.Empty;
            public bool Mostrar { get; set; }
            public List<PermisoVm> Permisos { get; set; } = [];
        }

        public class PermisoVm
        {
            public long Id { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
        }

        public class RolVm
        {
            public long Id { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
        }

        private static List<DetallePermisoVm> ProcesarDetallesPermisos(List<ModuloVm> modulos)
        {
            return modulos
                .SelectMany(mod => mod.Menus.SelectMany(men => men.Permisos.Select(per => new DetallePermisoVm()
                {
                    CodigoMenu = men.Codigo,
                    CodigoPermiso = per.Codigo
                })))
                .ToList();
        }
    }
}