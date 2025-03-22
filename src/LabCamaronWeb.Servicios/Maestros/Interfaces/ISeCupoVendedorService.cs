using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.CupoVendedor.CupoVendedorVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeCupoVendedorService
    {
        Task<RespuestaConsultaGenericaVm<CupoVendedorVm>> ConsultarPorId(ConsultarCupoVendedor consultar);

        Task<RespuestaConsultasGenericaVm<CupoVendedorVm>> ConsultarTodos(ConsultarCupoVendedor consultar);

        Task<RespuestaGenericaVm> Crear(CrearActualizarCupoVendedor crear);

        Task<RespuestaGenericaVm> Actualizar(CrearActualizarCupoVendedor actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarCupoVendedor eliminar);
    }
}