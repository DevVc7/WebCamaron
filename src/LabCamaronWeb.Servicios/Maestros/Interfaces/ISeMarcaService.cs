using LabCamaronWeb.Dto.Maestros.Marca;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Marca.MarcaVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeMarcaService
    {
        Task<RespuestaConsultaGenericaVm<MarcaVm>> ConsultarPorId(ConsultarMarca consultar);

        Task<RespuestaConsultasGenericaVm<MarcaVm>> ConsultarTodos(ConsultarTodosMarca consultar);

        Task<RespuestaGenericaVm> Crear(CrearMarca crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarMarca actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarMarca eliminar);
    }
}