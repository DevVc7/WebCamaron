using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Maestros.Categoria.CategoriaVm;

namespace LabCamaronWeb.Servicios.Maestros.Interfaces
{
    public interface ISeCategoriaService
    {
        Task<RespuestaConsultaGenericaVm<CategoriaVm>> ConsultarPorId(ConsultarCategoria consultar);

        Task<RespuestaConsultasGenericaVm<CategoriaVm>> ConsultarTodos(ConsultarTodosCategoria consultar);

        Task<RespuestaGenericaVm> Crear(CrearCategoria crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarCategoria actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarCategoria eliminar);
    }
}