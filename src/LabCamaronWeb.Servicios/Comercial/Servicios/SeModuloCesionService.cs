using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Dto.Maestros.CupoVendedor;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Comercial.Servicios
{
    internal class SeModuloCesionService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeModuloCesionService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaConsultasGenericaVm<AsignacionClienteVm>> ConsultarTodos(AsignacionClienteVm.ConsultarTodosAsignacionCliente consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AsignacionClienteVm.ConsultarTodosAsignacionCliente, RespuestaConsultasGenericaVm<AsignacionClienteVm>>(
                        _configuration["Microservicios:ConsultarAsignacionClientes"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        //public async Task<RespuestaGenericaVm> Actualizar(CupoVendedorVm.CrearActualizarCupoVendedor actualizar)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<CupoVendedorVm.CrearActualizarCupoVendedor, RespuestaGenericaVm>(
        //                _configuration["Microservicios:CrearCupoVendedor"]!, actualizar);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, actualizar);
        //        return RespuestaGenericaVm.Excepcion();
        //    }
        //}

        //public async Task<RespuestaConsultaGenericaVm<CupoVendedorVm>> ConsultarPorId(CupoVendedorVm.ConsultarCupoVendedor consultar)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<CupoVendedorVm.ConsultarCupoVendedor, RespuestaConsultaGenericaVm<CupoVendedorVm>>(
        //                _configuration["Microservicios:ConsultarCupoVendedor"]!, consultar);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, consultar);
        //        return new(RespuestaGenericaVm.Excepcion());
        //    }
        //}

        //public async Task<RespuestaConsultasGenericaVm<CupoVendedorVm>> ConsultarTodos(CupoVendedorVm.ConsultarCupoVendedor consultar)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<CupoVendedorVm.ConsultarCupoVendedor, RespuestaConsultasGenericaVm<CupoVendedorVm>>(
        //                _configuration["Microservicios:ConsultarCupoVendedor"]!, consultar);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, consultar);
        //        return new(RespuestaGenericaVm.Excepcion());
        //    }
        //}

        //public async Task<RespuestaGenericaVm> Crear(CupoVendedorVm.CrearActualizarCupoVendedor crear)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<CupoVendedorVm.CrearActualizarCupoVendedor, RespuestaGenericaVm>(
        //                _configuration["Microservicios:CrearCupoVendedor"]!, crear);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, crear);
        //        return RespuestaGenericaVm.Excepcion();
        //    }
        //}

        //public async Task<RespuestaGenericaVm> Eliminar(CupoVendedorVm.EliminarCupoVendedor eliminar)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<CupoVendedorVm.EliminarCupoVendedor, RespuestaGenericaVm>(
        //                _configuration["Microservicios:EliminarCupoVendedor"]!, eliminar);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, eliminar);
        //        return RespuestaGenericaVm.Excepcion();
        //    }
        //}
    }
}