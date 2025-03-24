using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.EnteTecnico.EnteTecnicoVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEnteTecnicoService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEnteTecnicoService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteTecnico crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearActualizarEnteTecnico, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearActualizarEnteTecnico"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarEnteTecnico eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarEnteTecnico, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEnteTecnico"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}