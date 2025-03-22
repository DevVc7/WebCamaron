using LabCamaronWeb.Dto.Maestros.Cargo;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeCargoService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeCargoService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(CargoVm.ActualizarCargo actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CargoVm.ActualizarCargo, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarCargo"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<CargoVm>> ConsultarPorId(CargoVm.ConsultarCargo consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CargoVm.ConsultarCargo, RespuestaConsultaGenericaVm<CargoVm>>(
                        _configuration["Microservicios:ConsultarCargoCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<CargoVm>> ConsultarTodos(CargoVm.ConsultarTodosCargo consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CargoVm.ConsultarTodosCargo, RespuestaConsultasGenericaVm<CargoVm>>(
                        _configuration["Microservicios:ConsultarCargo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CargoVm.CrearCargo crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CargoVm.CrearCargo, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearCargo"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(CargoVm.EliminarCargo eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CargoVm.EliminarCargo, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarCargo"]!, eliminar);

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