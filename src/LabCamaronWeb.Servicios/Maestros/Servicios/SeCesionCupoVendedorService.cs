using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Dto.Maestros.CesionCupoVendedor;
using LabCamaronWeb.Dto.Maestros.Color;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeCesionCupoVendedorService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeCesionCupoVendedorService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public Task<RespuestaGenericaVm> Actualizar(CesionCupoVendedorVm.ActualizarCesionCupoVendedor actualizar)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaConsultaGenericaVm<CesionCupoVendedorVm>> ConsultarPorId(CesionCupoVendedorVm.ConsultarCesionCupoVendedor consultar)
        {
            throw new NotImplementedException();
        }

        //public Task<RespuestaConsultasGenericaVm<CesionCupoVendedorVm>> ConsultarTodos(CesionCupoVendedorVm.ConsultarTodosCesionCupoVendedor consultar)
        //{
        //    try
        //    {
        //        var respuesta = await _operacionHttp
        //            .EjecutarServicioAutenticado<ColorVm.ConsultarTodosColor, RespuestaConsultasGenericaVm<ColorVm>>(
        //                _configuration["Microservicios:ConsultarColor"]!, consultar);

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtils.LogError(ex, consultar);
        //        return new(RespuestaGenericaVm.Excepcion());
        //    }

        //}
        public async Task<RespuestaConsultasGenericaVm<ColorVm>> ConsultarTodos(ColorVm.ConsultarTodosColor consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ColorVm.ConsultarTodosColor, RespuestaConsultasGenericaVm<ColorVm>>(
                        _configuration["Microservicios:ConsultarColor"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }



        public Task<RespuestaGenericaVm> Crear(CesionCupoVendedorVm.CrearCesionCupoVendedor crear)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaGenericaVm> Eliminar(CesionCupoVendedorVm.EliminarCesionCupoVendedor eliminar)
        {
            throw new NotImplementedException();
        }
    }
}