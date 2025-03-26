using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Comercial.AsignacionCliente.AsignacionClienteVm;
using static LabCamaronWeb.Dto.Maestros.CupoVendedor.CupoVendedorVm;

namespace LabCamaronWeb.Servicios.Comercial.Interfaces
{
    public interface ISeModuloCesionService
    {
        Task<RespuestaConsultasGenericaVm<AsignacionClienteVm>> ConsultarTodos(ConsultarTodosAsignacionCliente consultar);

        //Task<RespuestaConsultasGenericaVm<CupoVendedorVm>> ConsultarTodos(ConsultarCupoVendedor consultar);

        //Task<RespuestaGenericaVm> Crear(CrearActualizarCupoVendedor crear);

        //Task<RespuestaGenericaVm> Actualizar(CrearActualizarCupoVendedor actualizar);

        //Task<RespuestaGenericaVm> Eliminar(EliminarCupoVendedor eliminar);
        //Task <RespuestaConsultasGenericaVm<>>
    }
}