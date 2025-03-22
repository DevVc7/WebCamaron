using LabCamaronWeb.Dto.Parametrizacion.Empresa;
using LabCamaronWeb.Infraestructura.Modelo;
using static LabCamaronWeb.Dto.Parametrizacion.Empresa.EmpresaVm;

namespace LabCamaronWeb.Servicios.Parametrizacion.Interfaces
{
    public interface ISeEmpresaService
    {
        Task<RespuestaConsultaGenericaVm<EmpresaVm>> ConsultarPorId(ConsultarEmpresa consultar);

        Task<RespuestaConsultasGenericaVm<EmpresaVm>> ConsultarTodos(ConsultarTodosEmpresa consultar);

        Task<RespuestaGenericaVm> Crear(CrearEmpresa crear);

        Task<RespuestaGenericaVm> Actualizar(ActualizarEmpresa actualizar);

        Task<RespuestaGenericaVm> Eliminar(EliminarEmpresa eliminar);
    }
}