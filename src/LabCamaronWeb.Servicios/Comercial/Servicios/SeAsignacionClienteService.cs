using LabCamaronWeb.Dto.Comercial.AsignacionCliente;
using LabCamaronWeb.Dto.Maestros.Dieta;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Comercial.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Comercial.Servicios
{
    public class SeAsignacionClienteService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeAsignacionClienteService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(AsignacionClienteVm.ActualizarAsignacionCliente actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AsignacionClienteVm.ActualizarAsignacionCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarAsignacionCliente"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<AsignacionClienteVm.Detallado>> ConsultarPorId(AsignacionClienteVm.ConsultarAsignacionCliente consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AsignacionClienteVm.ConsultarAsignacionCliente, RespuestaConsultaGenericaVm<AsignacionClienteVm.Detallado>>(
                        _configuration["Microservicios:ConsultarAsignacionCliente"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

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

        public async Task<RespuestaGenericaVm> Crear(AsignacionClienteVm.CrearAsignacionCliente crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AsignacionClienteVm.CrearAsignacionCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearAsignacionCliente"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(AsignacionClienteVm.EliminarAsignacionCliente eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<AsignacionClienteVm.EliminarAsignacionCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarAsignacionCliente"]!, eliminar);

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