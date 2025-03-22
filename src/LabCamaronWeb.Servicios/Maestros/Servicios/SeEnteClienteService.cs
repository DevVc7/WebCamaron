using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.EnteCliente.EnteClienteVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEnteClienteService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEnteClienteService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteCliente crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearActualizarEnteCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearActualizarEnteCliente"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarEnteCliente eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarEnteCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEnteCliente"]!, eliminar);

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