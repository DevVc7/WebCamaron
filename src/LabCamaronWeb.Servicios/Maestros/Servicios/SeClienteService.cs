using LabCamaronWeb.Dto.Maestros.Cliente;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.Cliente.ClienteVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeClienteService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeClienteService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(ActualizarCliente actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActualizarCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarCliente"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<Detallado>> ConsultarPorId(ConsultarCliente consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarCliente, RespuestaConsultaGenericaVm<Detallado>>(
                        _configuration["Microservicios:ConsultarClienteCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<ClienteVm>> ConsultarTodos(ConsultarTodosCliente consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosCliente, RespuestaConsultasGenericaVm<ClienteVm>>(
                        _configuration["Microservicios:ConsultarClientes"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CrearCliente crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearCliente"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarCliente eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarCliente"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Activar(ActivarCliente activar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActivarCliente, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActivarCliente"]!, activar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, activar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}