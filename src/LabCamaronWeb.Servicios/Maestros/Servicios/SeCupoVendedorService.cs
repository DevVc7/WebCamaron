using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeCupoVendedorService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeCupoVendedorService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(CupoVendedorVm.CrearActualizarCupoVendedor actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CupoVendedorVm.CrearActualizarCupoVendedor, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearCupoVendedor"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<CupoVendedorVm>> ConsultarPorId(CupoVendedorVm.ConsultarCupoVendedor consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CupoVendedorVm.ConsultarCupoVendedor, RespuestaConsultaGenericaVm<CupoVendedorVm>>(
                        _configuration["Microservicios:ConsultarCupoVendedor"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<CupoVendedorVm>> ConsultarTodos(CupoVendedorVm.ConsultarCupoVendedor consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CupoVendedorVm.ConsultarCupoVendedor, RespuestaConsultasGenericaVm<CupoVendedorVm>>(
                        _configuration["Microservicios:ConsultarCupoVendedor"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CupoVendedorVm.CrearActualizarCupoVendedor crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CupoVendedorVm.CrearActualizarCupoVendedor, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearCupoVendedor"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(CupoVendedorVm.EliminarCupoVendedor eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CupoVendedorVm.EliminarCupoVendedor, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarCupoVendedor"]!, eliminar);

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