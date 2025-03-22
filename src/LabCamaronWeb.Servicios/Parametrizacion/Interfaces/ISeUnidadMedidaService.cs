using LabCamaronWeb.Dto.Parametrizacion.UnidadMedida;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.UnidadMedida.UnidadMedidaVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeUnidadMedidaService
    {
        Task<RespuestaConsultaGenericaVm<UnidadMedidaVm>> ConsultarPorId(ConsultarUnidadMedida consultar);

        Task<RespuestaConsultasGenericaVm<UnidadMedidaVm>> ConsultarTodos(ConsultarTodosUnidadMedida consultar);

        Task<RespuestaConsultasGenericaVm<TipoUnidadMedidaVm>> ConsultarTiposUnidadMedida();

        Task<RespuestaGenericaVm> Crear(CrearUnidadMedida crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarUnidadMedida actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarUnidadMedida eliminar);
    }
}