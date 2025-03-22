using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.EnteVendedor.EnteVendedorVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeEnteVendedorService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEnteVendedorService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> CrearActualizar(CrearActualizarEnteVendedor crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearActualizarEnteVendedor, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearActualizarEnteVendedor"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarEnteVendedor eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarEnteVendedor, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEnteVendedor"]!, eliminar);

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