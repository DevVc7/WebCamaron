using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.EntePersonal.EntePersonalVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEntePersonalService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEntePersonalService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEntePersonal crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearActualizarEntePersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearActualizarEntePersonal"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarEntePersonal eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarEntePersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEntePersonal"]!, eliminar);

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