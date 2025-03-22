using LabCamaronWeb.Dto.Configuracion.Login;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Infraestructura.Extensiones;

namespace LabCamaron.Web.Extensions
{
    public static class RazorHttpContextExtensiones
    {
        public static bool TienePermiso(this HttpContext context, string codigoMenu, string codigoPermiso)
        {
            var permisos = context.Session.Obtener<List<DetallePermisoVm>>(SesionConstantes.Permisos) ?? [];
            return permisos.Any(e => e.CodigoMenu == codigoMenu && e.CodigoPermiso == codigoPermiso);
        }

        public static bool TienePermiso(this HttpContext context, string codigoMenu, ICollection<string> codigosPermiso)
        {
            var permisos = context.Session.Obtener<List<DetallePermisoVm>>(SesionConstantes.Permisos) ?? [];

            foreach (var codigoPermiso in codigosPermiso)
            {
                if (!permisos.Any(e => e.CodigoMenu == codigoMenu && e.CodigoPermiso == codigoPermiso)) return false;
            }

            return true;
        }

        public static string ObtenerNombreModulo(this HttpContext context, string codigoMenu)
        {
            var modulos = context.Session.Obtener<List<PermisoUsuarioVm.ModuloVm>>(SesionConstantes.Modulos) ?? [];
            return modulos.SelectMany(e => e.Menus.Select(x => new
            {
                NombreModulo = e.Nombre,
                CodigoMenu = x.Codigo
            }))
            .FirstOrDefault(e => e.CodigoMenu == codigoMenu)?
            .NombreModulo ?? string.Empty;
        }
    }
}