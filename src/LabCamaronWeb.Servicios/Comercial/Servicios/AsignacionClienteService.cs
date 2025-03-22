using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Servicios.Comercial.Interfaces;

namespace LabCamaronWeb.Servicios.Comercial.Servicios
{
    public class AsignacionClienteService : IAsignacionClienteService
    {
        public async Task<RespuestaConsultasGenericaVm<AsignacionClienteVm>> ConsultarTodos(AsignacionClienteVm.ConsultarTodosAsignacionCliente consultar)
        {
            return new()
            {
                Respuesta = RespuestaGenericaVm.ExitoComun(),
                Resultados = [],
            };
        }
    }
}