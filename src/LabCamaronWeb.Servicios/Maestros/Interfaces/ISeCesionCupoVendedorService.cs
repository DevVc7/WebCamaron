using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Dto.Maestros.CesionCupoVendedor;
using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.CesionCupoVendedor.CesionCupoVendedorVm;
using static LabCamaronWeb.Dto.Maestros.Color.ColorVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeCesionCupoVendedorService
    {
        Task<RespuestaConsultaGenericaVm<CesionCupoVendedorVm>> ConsultarPorId(ConsultarCesionCupoVendedor consultar);

        Task<RespuestaConsultasGenericaVm<ColorVm>> ConsultarTodos(ConsultarTodosColor consultar);

        Task<RespuestaGenericaVm> Crear(CrearCesionCupoVendedor crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarCesionCupoVendedor actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarCesionCupoVendedor eliminar);
    }
}